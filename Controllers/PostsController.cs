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
    public class PostsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Posts
        public ActionResult Index()
        {
            var userId = int.Parse(User.Identity.Name);
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);
            var username = user != null ? user.Username : "Utente non trovato";

            var posts = db.Posts
                .Include(p => p.Likes)
                .Include(p => p.Users)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.PostedAt)
                .Select(p => new PostViewModel
                {
                    Post = p,
                    LikedByUser = p.Likes.Any(l => l.UserId == userId),
                    TotalLikes = p.Likes.Count(),
                    Comments = p.Comments.OrderByDescending(c => c.PostedAt).ToList()
                }).ToList();

            ViewBag.Username = username;
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

      
        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contents")] Posts post)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.Identity.Name); // ID dell'utente attuale
                post.PostedAt = DateTime.Now;
                post.UserId = userId;

                db.Posts.Add(post);
                db.SaveChanges();

                // Ottieni tutti gli ID degli utenti che seguono l'utente che ha fatto il post
                var followerIds = db.Friendships
                    .Where(f => f.UserRiceventeId == userId) // solo coloro che hanno l'utente come seguito
                    .Select(f => f.UserMittenteId) // prendi gli ID degli utenti che seguono l'utente loggato
                    .Distinct()
                    .ToList();

                // Crea una notifica per ciascun follower
                foreach (var followerId in followerIds)
                {
                    var notification = new Notifications
                    {
                        UserID = followerId,
                        TriggeredByUserID = userId,
                        PostID = post.PostId, // l'ID del nuovo post
                        NotificationType = "Post",
                        ReadStatus = false,
                        NotificationDate = DateTime.Now
                    };
                    db.Notifications.Add(notification);
                }

                db.SaveChanges(); // Salva tutte le notifiche in una volta sola
                TempData["success"] = "Bisbiglio diffuso con successo";
                return RedirectToAction("Index");
            }

            TempData["error"] = "C'è stato qualche problema con la diffusione del Bisbiglio";
            return View(post);
        }






        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }

            return View(posts);
        }

        // POST: Posts/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Contents")] Posts postForm)
        {
            if (ModelState.IsValid)
            {
                var post = db.Posts.Find(id); 
                if (post == null)
                {
                    return HttpNotFound();
                }

                post.Contents = postForm.Contents;
                post.PostedAt = DateTime.Now; 
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Bisbiglio modificato con successo";
                return RedirectToAction("Index");
            }

            TempData["error"] = "C'è stato qualche problema con la modifica del Bisbiglio";
            return View(postForm);
        }


        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Trova tutte le notifiche collegate al post che stai eliminando
            var notifications = db.Notifications.Where(n => n.PostID == id).ToList();
            foreach (var notification in notifications)
            {
                db.Notifications.Remove(notification);
            }

            // Ora puoi eliminare il post
            Posts post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
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
