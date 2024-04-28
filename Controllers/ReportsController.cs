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
            var reports = db.Reports
                            .Include(r => r.Comments)
                            .Include(r => r.Posts)
                            .Include(r => r.Users)
                            .OrderByDescending(r => r.ReportId) 
                            .ToList();
            return View(reports);
        }


        // GET: Reports/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "User")]
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
                ReportedByUserId = userId,
                ReportDate = DateTime.Now
            };

            return View(report);
        }


        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportId,PostId,CommentId,ReportedByUserId,DescrizioneReport")] Reports report)
        {
            if (ModelState.IsValid)
            {
               
                report.ReportedByUserId = int.Parse(User.Identity.Name);

                report.ReportDate = DateTime.Now;

                db.Reports.Add(report);
                db.SaveChanges();
                TempData["success"] = "Report effettuato con successo";
                return RedirectToAction("Index", "Posts");
            }

            //TempData["error"] = "Ci sono stati problemi con il tuo report";
            return View(report);
        }

        // GET: Reports/Delete/5
        [Authorize(Roles = "Admin")]
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
