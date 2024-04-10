using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Users

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Assumi che ci sia una relazione tra Users e Posts nel tuo database
            var posts = db.Posts.Where(p => p.UserId == id)
                    .OrderByDescending(p => p.PostedAt)
                    .ToList();

            // Crea un nuovo ViewModel se non ne hai già uno che includa sia i dati dell'utente che i post
            var viewModel = new UserProfileViewModel
            {
                Users = user,
                Posts = posts,
                LoggedInUserId = User.Identity.Name
            };

            return View(viewModel); // Assicurati di avere un ViewModel che possa gestire sia l'utente che i post
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Users/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Username,Password,Email,Nome,Cognome,Role,Stato,CodiceFiscale")] Users users, string confermaPassword)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            // Aggiungi qui il controllo della corrispondenza delle password
            if (users.Password != confermaPassword)
            {
                TempData["error"] = "Le password non combaciano";
                return View(users);
            }

            if (ModelState.IsValid)
            {

                var existingUserByEmail = db.Users.FirstOrDefault(u => u.Email == users.Email);
                if (existingUserByEmail != null)
                {
                    TempData["error"] = "L'email esiste già";
                    return View(users);
                }

                var existingUserByCodiceFiscale = db.Users.FirstOrDefault(u => u.CodiceFiscale == users.CodiceFiscale);
                if (existingUserByCodiceFiscale != null)
                {
                    TempData["error"] = "Il codice fiscale esiste già";
                    return View(users);
                }

                var existingUserByUsername = db.Users.FirstOrDefault(u => u.Username == users.Username);
                if (existingUserByUsername != null)
                {
                    TempData["error"] = "Lo username esiste già";
                    return View(users);
                }

                var usernameEqualToName = users.Username.Equals(users.Nome, StringComparison.OrdinalIgnoreCase);
                if (usernameEqualToName)
                {
                    TempData["error"] = "Lo username non può essere uguale al nome";
                    return View(users);
                }

                db.Users.Add(users);
                db.SaveChanges();
                TempData["success"] = "Account creato con successo";
                return RedirectToAction("Index");
            }

            
            return View(users);
        }


        // GET: Users/Edit/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Password,Email,Nome,Cognome,Role,Stato,CodiceFiscale")] Users users, string OldPassword, string confermaPassword)
        {
            var userInDb = db.Users.Find(users.UserId);

            if (userInDb == null)
            {
                return HttpNotFound();
            }

            // Aggiungi qui il controllo della corrispondenza delle password
            if (users.Password != confermaPassword)
            {
                TempData["error"] = "Le password non combaciano";
                return View(users);
            }

            if (userInDb.Password != OldPassword)
            {
                TempData["error"] = "la Password non è corretta";
                return View(users);
            }

            if (ModelState.IsValid)
            {
                db.Entry(userInDb).CurrentValues.SetValues(users);
                db.SaveChanges();
                TempData["success"] = "Modifiche avvenute con successo";
                return RedirectToAction("Index");
            }

            return View(users);
        }


        // GET: Users/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string ConfirmPassword)
        {
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.Password != ConfirmPassword)
            {
                TempData["error"] = "La Password non è corretta.";
                return RedirectToAction("Delete", new { id = id });
            }

            db.Users.Remove(user);
            db.SaveChanges();

            // Se l'utente che viene eliminato è quello attualmente loggato, effettuare il logout
            if (User.Identity.Name == id.ToString())
            {
                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public ActionResult BanUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null && user.Stato != "Bannato")
            {
                user.Stato = "Bannato";
                db.SaveChanges();
                TempData["success"] = "Utente bannato con successo.";
            }
            else
            {
                TempData["error"] = "Non è possibile bannare l'utente( utente bannato in precedenza )";
            }

            return RedirectToAction("Index", "Reports");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UnbanUser(int id)
        {
            var userToUnban = db.Users.Find(id);
            if (userToUnban != null && userToUnban.Stato == "Bannato")
            {
                userToUnban.Stato = "Attivo";
                db.SaveChanges();
                TempData["success"] = "Ban dell'utente sospeso con successo.";
            }
            else
            {
                TempData["error"] = "Non è possibile sospendere il ban dell'utente.";
            }

            return RedirectToAction("Index", "Reports");
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
