﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class ConversationsController : Controller
    {
        private DBContext db = new DBContext();


        public ActionResult Index()
        {
            return View();
        }



        // POST: Conversations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int user2Id)
        {
            int user1Id = int.Parse(User.Identity.Name);
            var existingConversation = db.Conversations.FirstOrDefault(c =>
                (c.User1Id == user1Id && c.User2Id == user2Id) ||
                (c.User1Id == user2Id && c.User2Id == user1Id));

            if (existingConversation != null)
            {
                // Assicurati che la conversazione sia visibile per entrambi gli utenti
                existingConversation.User1Deleted = false;
                existingConversation.User2Deleted = false;
                db.Entry(existingConversation).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                // Crea una nuova conversazione
                existingConversation = new Conversations
                {
                    User1Id = user1Id,
                    User2Id = user2Id,
                    User1Deleted = false,
                    User2Deleted = false
                };
                db.Conversations.Add(existingConversation);
                db.SaveChanges();
            }

            // Cerca una notifica esistente e la resetta, oppure crea una nuova
            var notification = db.Notifications.FirstOrDefault(n =>
                n.ConversationID == existingConversation.ConversationId && n.UserID == user2Id);

            if (notification != null)
            {
                // Resetta la notifica esistente a "non letta"
                notification.ReadStatus = false;
                notification.NotificationDate = DateTime.Now; // Aggiorna la data della notifica
            }
            else
            {
                // Crea una nuova notifica
                notification = new Notifications
                {
                    UserID = user2Id,
                    TriggeredByUserID = user1Id,
                    ConversationID = existingConversation.ConversationId,
                    NotificationType = "Message",
                    ReadStatus = false,
                    NotificationDate = DateTime.Now
                };
                db.Notifications.Add(notification);
            }

            db.SaveChanges();

            return RedirectToAction("Conversations", "Notifications");
        }






        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Conversations conversation = db.Conversations.Find(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }

            // Verifica che l'utente corrente sia parte della conversazione
            int currentUserId = int.Parse(User.Identity.Name);
            if (conversation.User1Id != currentUserId && conversation.User2Id != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Determina l'ID dell'altro utente
            int otherUserId = conversation.User1Id == currentUserId ? conversation.User2Id : conversation.User1Id;

            // Passa l'altro utente alla view
            ViewBag.OtherUser = db.Users.Find(otherUserId);

            return View(conversation);
        }




        // POST: Conversations/Delete/5
        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conversations conversation = db.Conversations.Find(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            // Imposta il flag di eliminazione per l'utente corrente
            if (conversation.User1Id == userId)
            {
                conversation.User1Deleted = true;
            }
            else if (conversation.User2Id == userId)
            {
                conversation.User2Deleted = true;
            }

            // Elimina la conversazione se entrambi gli utenti hanno impostato il flag di eliminazione
            if (conversation.User1Deleted && conversation.User2Deleted)
            {
                // Prima elimina tutti i messaggi correlati a questa conversazione
                var messagesToDelete = db.Messages.Where(m => m.ConversationId == id).ToList();
                foreach (var message in messagesToDelete)
                {
                    db.Messages.Remove(message);
                }


                // Prima elimina tutte le notifiche correlate a questa conversazione
                var notificationsToDelete = db.Notifications.Where(n => n.ConversationID == id).ToList();
                foreach (var notification in notificationsToDelete)
                {
                    db.Notifications.Remove(notification);
                }

                // Poi elimina la conversazione
                db.Conversations.Remove(conversation);
            }
            else
            {
                db.Entry(conversation).State = EntityState.Modified;
            }

            db.SaveChanges();
            return RedirectToAction("Conversations", "Notifications");
        }



        // Conversazione specifica
        public ActionResult Details(int id)
        {
            int currentUserId = int.Parse(User.Identity.Name); // Assicurati che questo sia effettivamente l'ID dell'utente
            var conversation = db.Conversations
                .Include(c => c.Messages)
                .FirstOrDefault(c => c.ConversationId == id);

            if (conversation == null)
            {
                return HttpNotFound("Conversazione non trovata o non hai il permesso di visualizzarla.");
            }

            // Identificare l'altro utente
            int otherUserId = conversation.User1Id == currentUserId ? conversation.User2Id : conversation.User1Id;
            var otherUser = db.Users.Find(otherUserId);
            if (otherUser == null)
            {
                return HttpNotFound("L'altro utente nella conversazione non è stato trovato.");
            }

            var viewModel = new ConversationViewModel
            {
                Conversation = conversation,
                Messages = conversation.Messages.ToList(),
                OtherUser = otherUser 
            };

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
