using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class MessagesController : Controller
    {

        private DBContext db = new DBContext();
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(int conversationId, string messageText)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = int.Parse(User.Identity.Name);
                var conversation = db.Conversations.Find(conversationId);

                // Controlla se l'altra parte della conversazione ha eseguito il soft delete
                var otherUserId = conversation.User1Id == currentUserId ? conversation.User2Id : conversation.User1Id;
                var otherUser = db.Users.Find(otherUserId);
                if (otherUser.IsDeleted)
                {
                    TempData["userUnable"] = "Non è possibile inviare messaggi perché l'utente è temporaneamente inattivo.";
                    return RedirectToAction("Details", "Conversations", new { id = conversationId });
                }

                var message = new Messages
                {
                    ConversationId = conversationId,
                    UserId = currentUserId,
                    Testo = messageText,
                    Orario = DateTime.Now,
                    ReadStatus = false // Imposta il messaggio come non letto
                };
                db.Messages.Add(message);
                db.SaveChanges();

               
                if (conversation != null)
                {
                    otherUserId = conversation.User1Id == currentUserId ? conversation.User2Id : conversation.User1Id;

                    if (conversation.User1Id == currentUserId && conversation.User2Deleted)
                    {
                        conversation.User2Deleted = false;
                    }
                    else if (conversation.User2Id == currentUserId && conversation.User1Deleted)
                    {
                        conversation.User1Deleted = false;
                    }

                    db.Entry(conversation).State = EntityState.Modified;

                    // Gestisci la notifica
                    var notification = db.Notifications.FirstOrDefault(n =>
                        n.ConversationID == conversationId && n.UserID == otherUserId);

                    if (notification == null)
                    {
                        // Crea una nuova notifica se non esiste
                        notification = new Notifications
                        {
                            UserID = otherUserId,
                            TriggeredByUserID = currentUserId,
                            ConversationID = conversationId,
                            NotificationType = "Message",
                            ReadStatus = false,
                            NotificationDate = DateTime.Now
                        };
                        db.Notifications.Add(notification);
                    }
                    else if (notification.ReadStatus.HasValue && notification.ReadStatus.Value)
                    {
                        // Aggiorna la notifica esistente se è stata già letta
                        notification.ReadStatus = false;
                        notification.NotificationDate = DateTime.Now;
                        db.Entry(notification).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Details", "Conversations", new { id = conversationId });
            }

            return RedirectToAction("Details", "Conversations", new { id = conversationId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int messageId, string newText)
        {
            // Trova il messaggio nel database
            var message = db.Messages.Find(messageId);
            if (message == null)
            {
                return HttpNotFound();
            }

            // Verifica se l'utente corrente ha il permesso di modificare il messaggio
            if (message.UserId != int.Parse(User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Aggiorna il testo del messaggio
            message.Testo = newText;
            db.SaveChanges();

            // Reindirizza alla conversazione o ritorna un risultato appropriato
            return RedirectToAction("Details", "Conversations", new { id = message.ConversationId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int messageId)
        {
            int currentUserId = int.Parse(User.Identity.Name);
            var message = db.Messages.Find(messageId);
            if (message == null)
            {
                return HttpNotFound();
            }
            if (message.UserId != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            var remainingMessages = db.Messages.Any(m => m.ConversationId == message.ConversationId);
            if (!remainingMessages) // Se non ci sono altri messaggi nella conversazione
            {
                var notifications = db.Notifications.Where(n => n.ConversationID == message.ConversationId && n.UserID != currentUserId);
                db.Notifications.RemoveRange(notifications);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Conversations", new { id = message.ConversationId });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(int messageId, string descrizioneReport)
        {
            var report = new Reports
            {
                MessageId = messageId,
                ReportedByUserId = int.Parse(User.Identity.Name),
                DescrizioneReport = descrizioneReport,
                ReportDate = DateTime.Now
            };

            db.Reports.Add(report);
            db.SaveChanges();

            // Reindirizza alla conversazione o ritorna un risultato appropriato
            return RedirectToAction("Details", "Conversations", new { id = db.Messages.Find(messageId)?.ConversationId });
        }



    }
}