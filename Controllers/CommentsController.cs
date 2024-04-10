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
    public class CommentsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Likes).Include(c => c.Users);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

      

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Contents")] Comments comment, int postId)
        {
            
            if (ModelState.IsValid)
            {
                comment.PostedAt = DateTime.Now;
                var userId = int.Parse(User.Identity.Name);
                comment.UserId = userId;
                comment.PostId = postId;


                db.Comments.Add(comment);
                db.SaveChanges();
                TempData["success"] = "Commento pubblicato con successo";
                return RedirectToAction("Index", "Posts");
            }
            TempData["errore"] = "Problemi con la pubblicazione del commento";
                return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            
            return View(comments);
        }

        // POST: Comments/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Contents")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                var comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }

                comment.Contents = comments.Contents;
                comment.PostedAt = DateTime.Now;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Commento modificato con successo";
                return RedirectToAction("Index", "Posts");
            }

            TempData["error"] = "C'è stato un problema con la modifica del commento";
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
            db.SaveChanges();
            return RedirectToAction("Index", "Posts");
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