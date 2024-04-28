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
    public class AphorismsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Aphorisms
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var aphorisms = db.Aphorisms.OrderByDescending(a => a.AphorismId).ToList();
            return View(aphorisms);
        }

        [Authorize]
        public ActionResult GetRandomAphorism()
        {
            int count = db.Aphorisms.Count();
            // Assicurati che ci siano aforismi da restituire.
            if (count == 0)
            {
                return Json(new { frase = "Nessun aforisma disponibile." }, JsonRequestBehavior.AllowGet);
            }

            int index = new Random().Next(count);
            var aphorism = db.Aphorisms.OrderBy(a => a.AphorismId).Skip(index).FirstOrDefault();

            // Assicurati che l'aforisma non sia null.
            if (aphorism == null)
            {
                return Json(new { frase = "Errore durante il recupero dell'aforisma." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { frase = aphorism.Frase }, JsonRequestBehavior.AllowGet);
        }

        // GET: Aphorisms/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aphorisms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aphorisms aphorism)
        {
            if (ModelState.IsValid)
            {
                db.Aphorisms.Add(aphorism);
                db.SaveChanges();
                TempData["success"] = "Aforisma creato con successo";
                return RedirectToAction("Index");
            }

            return View(aphorism);
        }

        // GET: Aphorisms/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Aphorisms aphorism = db.Aphorisms.Find(id);
            if (aphorism == null)
            {
                return HttpNotFound();
            }
            return View(aphorism);
        }

        // POST: Aphorisms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Aphorisms aphorism)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aphorism).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Aforisma modifica con successo";
                return RedirectToAction("Index");
            }
            return View(aphorism);
        }

        // GET: Aphorisms/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Aphorisms aphorism = db.Aphorisms.Find(id);
            if (aphorism == null)
            {
                return HttpNotFound();
            }
            return View(aphorism);
        }

        // POST: Aphorisms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aphorisms aphorism = db.Aphorisms.Find(id);
            db.Aphorisms.Remove(aphorism);
            db.SaveChanges();
            TempData["success"] = "Aforisma cancellato con successo";
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
