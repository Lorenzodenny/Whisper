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
            var posts = db.Posts.Include(p => p.Likes).Include(p => p.Users);
            return View(posts.ToList());
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
        // Per proteggerti da attacchi di overposting, abilita solo le proprietà che vuoi essere legate.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contents")] Posts post)
        {
            if (ModelState.IsValid)
            {
                post.PostedAt = DateTime.Now; // Imposta la data e l'ora corrente del post

                post.UserId = int.Parse(User.Identity.Name); // Converto l'ID utente da stringa a int

                db.Posts.Add(post);
                db.SaveChanges();
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

            TempData["error"] = "C'è stato qualche problema con la diffusione del Bisbiglio";
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
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
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
