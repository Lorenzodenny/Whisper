using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class AuthController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = db.Users.FirstOrDefault(u => u.UserId.ToString() == User.Identity.Name);
                if (currentUser != null && currentUser.Role == "Admin")
                {
                    return RedirectToAction("Index", "Reports");
                }
                else
                {
                    return RedirectToAction("Index", "Posts");
                }
            }

            var loggedUser = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (loggedUser == null || loggedUser.Stato == "Bannato")
            {
                if (loggedUser != null && loggedUser.Stato == "Bannato")
                {
                    TempData["error"] = "Questo account è stato bannato e non può accedere.";
                }
                else
                {
                    TempData["error"] = "Nessun utente corrispondente trovato con questa combinazione di email e password.";
                }

                return RedirectToAction("Login");
            }
            else if (loggedUser.IsDeleted)
            {
                TempData["restore"] = true;
                TempData["userId"] = loggedUser.UserId;
                return RedirectToAction("Login");
            }

            FormsAuthentication.SetAuthCookie(loggedUser.UserId.ToString(), true);

            if (loggedUser.Role == "Admin")
            {
                TempData["success"] = "Login effettuato con successo come Admin.";
                return RedirectToAction("Index", "Reports");
            }
            else
            {
                TempData["success"] = "Login effettuato con successo.";
                return RedirectToAction("Index", "Posts");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestoreAccount(int userId)
        {
            var user = db.Users.Find(userId);
            if (user == null)
            {
                TempData["error"] = "Utente non trovato.";
                return RedirectToAction("Login");
            }

            user.IsDeleted = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            TempData["success"] = "Account riattivato con successo.";
            FormsAuthentication.SetAuthCookie(userId.ToString(), true);
            return RedirectToAction("Index", "Posts"); 
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}