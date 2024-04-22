using System;
using System.Collections.Generic;
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
            // Assicurati che l'ID della conversazione e il testo del messaggio siano validi
            if (ModelState.IsValid)
            {
                // Crea e salva il nuovo messaggio nel database
                var message = new Messages
                {
                    ConversationId = conversationId,
                    UserId = int.Parse(User.Identity.Name),
                    Testo = messageText,
                    Orario = DateTime.Now
                };

                db.Messages.Add(message);
                db.SaveChanges();

                // Reindirizza alla vista dei dettagli per mostrare il nuovo messaggio
                return RedirectToAction("Details", "Conversations", new { id = conversationId });
            }

            // Se ci sono problemi con il modello, ritorna alla vista dei dettagli
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
            // Trova il messaggio nel database
            var message = db.Messages.Find(messageId);
            if (message == null)
            {
                return HttpNotFound();
            }

            // Verifica se l'utente corrente ha il permesso di cancellare il messaggio
            if (message.UserId != int.Parse(User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Rimuovi il messaggio dal database
            db.Messages.Remove(message);
            db.SaveChanges();

            // Reindirizza alla conversazione o ritorna un risultato appropriato
            return RedirectToAction("Details", "Conversations", new { id = message.ConversationId });
        }


    }
}