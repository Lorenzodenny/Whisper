using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class SponsorsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sponsors
        public ActionResult Index()
        {
            var sponsors = db.Sponsors
                .Include(s => s.Users)
                .OrderByDescending(s => s.SponsorId);

            return View(sponsors.ToList());
        }

        // GET: Sponsors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsors sponsors = db.Sponsors.Find(id);
            if (sponsors == null)
            {
                return HttpNotFound();
            }
            return View(sponsors);
        }

        // GET: Sponsors/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "SponsorId,Titolo,Description")] Sponsors sponsors, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                int userId;
                if (int.TryParse(User.Identity.Name, out userId))
                {
                    var currentUser = db.Users.FirstOrDefault(u => u.UserId == userId);
                    if (currentUser != null)
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                           
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                            var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                       
                            try
                            {
                                upload.SaveAs(path);
                                sponsors.Foto = "~/Uploads/" + fileName;
                            }
                            catch (Exception ex)
                            {
                              
                                ModelState.AddModelError("", "Errore durante il salvataggio del file: " + ex.Message);
                                return View(sponsors);
                            }
                        }

                        sponsors.UserId = userId;
                        db.Sponsors.Add(sponsors);
                        db.SaveChanges();
                        TempData["success"] = "Sponsor aggiunto correttamente";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Utente non trovato.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Errore nel parsing dell'UserID.");
                }
            }

            return View(sponsors);
        }



        // GET: Sponsors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsors sponsors = db.Sponsors.Find(id);
            if (sponsors == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", sponsors.UserId);
            return View(sponsors);
        }

        // POST: Sponsors/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SponsorId,Titolo,Description,Foto,UserId")] Sponsors sponsors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sponsors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", sponsors.UserId);
            return View(sponsors);
        }

        // GET: Sponsors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsors sponsors = db.Sponsors.Find(id);
            if (sponsors == null)
            {
                return HttpNotFound();
            }
            return View(sponsors);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sponsors sponsors = db.Sponsors.Find(id);
            db.Sponsors.Remove(sponsors);
            db.SaveChanges();
            TempData["success"] = "Sponsor eliminato correttamente";
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
