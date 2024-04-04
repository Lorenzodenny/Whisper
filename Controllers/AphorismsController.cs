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
        public ActionResult Index()
        {
            return View(db.Aphorisms.ToList());
        }

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
