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
            if (like == null)
            {
                // Aggiungi un nuovo like
                db.Likes.Add(new Likes { PostId = postId, UserId = userId, EmoticonId = 1 /* Assumi che 1 sia l'ID per il pollice su */ });
            }
            else
            {
                // Rimuovi il like esistente
                db.Likes.Remove(like);
            }
            db.SaveChanges();

            if (Request.IsAjaxRequest()) // Controlla se la richiesta è una richiesta AJAX
            {
                return Json(new { success = true, message = "Like aggiunto con successo!" }); // Risposta AJAX
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