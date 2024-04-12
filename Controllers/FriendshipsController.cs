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
    public class FriendshipsController : Controller
    {
        private DBContext db = new DBContext();

        // Metodo per seguire un utente
        [HttpPost]
        public ActionResult Follow(int userId)
        {
            var followerId = int.Parse(User.Identity.Name);
            var existingFriendship = db.Friendships.Any(f => f.UserMittenteId == followerId && f.UserRiceventeId == userId);

            if (!existingFriendship)
            {
                var newFriendship = new Friendships { UserMittenteId = followerId, UserRiceventeId = userId };
                db.Friendships.Add(newFriendship);
                db.SaveChanges();
                TempData["success"] = "Amicizia inviata con successo";
            }

            return RedirectToAction("Details","Users" ,new { id = userId });
        }

        // Metodo per smettere di seguire un utente
        [HttpPost]
        public ActionResult Unfollow(int userId)
        {
            var followerId = int.Parse(User.Identity.Name);
            var friendship = db.Friendships.FirstOrDefault(f => f.UserMittenteId == followerId && f.UserRiceventeId == userId);

            if (friendship != null)
            {
                db.Friendships.Remove(friendship);
                db.SaveChanges();
                TempData["success"] = "Amicizia eliminata con successo";
            }

            return RedirectToAction("Details", "Users", new { id = userId });
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
