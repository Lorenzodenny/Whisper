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
    public class AvatarsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Avatars
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Avatars.ToList());
        }

        // GET: Avatars/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avatars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AvatarId")] Avatars avatars, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    upload.SaveAs(path);
                    avatars.Foto = "~/Uploads/" + fileName;
                }

                db.Avatars.Add(avatars);
                db.SaveChanges();
                TempData["success"] = "Avatar caricato con successo";
                return RedirectToAction("Index");
            }

            return View(avatars);
        }

        // GET: Avatars/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avatars avatars = db.Avatars.Find(id);
            if (avatars == null)
            {
                return HttpNotFound();
            }
            return View(avatars);
        }

        // POST: Avatars/Delete/5
        // POST: Avatars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avatars avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return HttpNotFound();
            }

            // Controlla se l'avatar è utilizzato da qualche utente
            var usersWithAvatar = db.Users.Where(u => u.AvatarId == id).ToList();
            if (usersWithAvatar.Any())
            {
                // Se l'avatar è utilizzato da almeno un utente
                foreach (var user in usersWithAvatar)
                {
                    user.AvatarId = 20; 
                }
            }

            db.Avatars.Remove(avatar);
            db.SaveChanges();
            TempData["success"] = "Avatar eliminato con successo";
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
