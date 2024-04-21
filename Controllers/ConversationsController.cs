using System;
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
                // Resetta il flag di eliminazione se la conversazione esisteva già
                if ((existingConversation.User1Id == user1Id && existingConversation.User1Deleted) ||
                    (existingConversation.User2Id == user1Id && existingConversation.User2Deleted))
                {
                    existingConversation.User1Deleted = existingConversation.User1Id == user1Id ? false : existingConversation.User1Deleted;
                    existingConversation.User2Deleted = existingConversation.User2Id == user1Id ? false : existingConversation.User2Deleted;
                    db.Entry(existingConversation).State = EntityState.Modified;
                    db.SaveChanges();
                }
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






        // GET: Conversations/Delete/5
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
            int userId = int.Parse(User.Identity.Name);
            if (conversation.User1Id != userId && conversation.User2Id != userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

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
