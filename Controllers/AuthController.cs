using System;
using System.Collections.Generic;
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
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
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

           
            FormsAuthentication.SetAuthCookie(loggedUser.UserId.ToString(), true);
            TempData["success"] = "Login effettuato con successo.";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}