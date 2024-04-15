using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class LikesController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Likes
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Like(int postId)
        {
            var userId = int.Parse(User.Identity.Name); // Assicurati che questo sia l'ID dell'utente corretto

            var like = db.Likes.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);
            var postOwner = db.Posts.Where(p => p.PostId == postId).Select(p => p.UserId).FirstOrDefault();

            if (like == null)
            {
                // Aggiungi un nuovo like
                var newLike = new Likes { PostId = postId, UserId = userId, EmoticonId = 1 }; // Assumi che 1 sia l'ID per il pollice su
                db.Likes.Add(newLike);
                db.SaveChanges();

                // Invia notifica solo se il liker non è il proprietario del post
                if (userId != postOwner)
                {
                    var notification = new Notifications
                    {
                        UserID = postOwner,
                        TriggeredByUserID = userId,
                        PostID = postId,
                        LikeID = newLike.LikeId, // Assumi che LikeId sia l'ID auto-generato per il nuovo "like"
                        NotificationType = "Like",
                        ReadStatus = false,
                        NotificationDate = DateTime.Now
                    };
                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }

                if (Request.IsAjaxRequest()) // Controlla se la richiesta è una richiesta AJAX
                {
                    return Json(new { success = true, message = "Like aggiunto con successo!" }); // Risposta AJAX
                }
            }
            else
            {
                // Rimuovi il like esistente
                db.Likes.Remove(like);

                // Trova la notifica correlata a questo like, se esiste
                var notification = db.Notifications
                                    .Where(n => n.PostID == postId && n.TriggeredByUserID == userId && n.NotificationType == "Like")
                                    .FirstOrDefault();
                if (notification != null)
                {
                    // Rimuovi la notifica
                    db.Notifications.Remove(notification);
                }

                db.SaveChanges();

                // Se la richiesta è AJAX, restituisci un JSON
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true, message = "Like rimosso con successo!" });
                }
            }

            return RedirectToAction("Index", "Posts");
        }


        public ActionResult GetLikeCount(int postId)
        {
            // Calcola il conteggio dei like per il post specificato
            var likeCount = db.Likes.Count(l => l.PostId == postId);

            // Restituisce il conteggio dei like come risposta AJAX
            return Json(new { likeCount = likeCount }, JsonRequestBehavior.AllowGet);
        }

    }
}