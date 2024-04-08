using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class ReportsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Reports
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Comments).Include(r => r.Comments1).Include(r => r.Messages).Include(r => r.Posts).Include(r => r.Users);
            return View(reports.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            return View(reports);
        }

        // GET: Reports/Create
        public ActionResult Create(int? postId, int? commentId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(User.Identity.Name);
            var userExists = db.Users.Any(u => u.UserId == userId);
            if (!userExists)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "L'utente non esiste.");
            }

            var report = new Reports
            {
                PostId = postId,
                CommentId = commentId,
                // ReportedByUserId = userId, // Non impostarlo qui, lo farai nel POST
                ReportDate = DateTime.Now
            };

            return View(report);
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportId,PostId,CommentId,DescrizioneReport")] Reports report)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            report.ReportedByUserId = int.Parse(User.Identity.Name); // Imposta l'ID utente autenticato qui
            report.ReportDate = report.ReportDate ?? DateTime.Now; // Assicurati che la data venga impostata

            if (ModelState.IsValid)
            {
                try
                {
                    db.Reports.Add(report);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException ex)
                {
                    // Aggiungi qui il tuo codice di gestione degli errori
                    // ...
                }
            }

            return View(report);
        }



        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommentId = new SelectList(db.Comments, "CommentId", "Contents", reports.CommentId);
            ViewBag.CommentId = new SelectList(db.Comments, "CommentId", "Contents", reports.CommentId);
            ViewBag.MessageId = new SelectList(db.Messages, "MessageId", "Testo", reports.MessageId);
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Contents", reports.PostId);
            ViewBag.ReportedByUserId = new SelectList(db.Users, "UserId", "Username", reports.ReportedByUserId);
            return View(reports);
        }

        // POST: Reports/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportId,PostId,CommentId,MessageId,ReportedByUserId,DescrizioneReport,ReportDate")] Reports reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommentId = new SelectList(db.Comments, "CommentId", "Contents", reports.CommentId);
            ViewBag.CommentId = new SelectList(db.Comments, "CommentId", "Contents", reports.CommentId);
            ViewBag.MessageId = new SelectList(db.Messages, "MessageId", "Testo", reports.MessageId);
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Contents", reports.PostId);
            ViewBag.ReportedByUserId = new SelectList(db.Users, "UserId", "Username", reports.ReportedByUserId);
            return View(reports);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            return View(reports);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reports reports = db.Reports.Find(id);
            db.Reports.Remove(reports);
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
