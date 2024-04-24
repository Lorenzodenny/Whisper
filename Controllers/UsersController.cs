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

            // logica per le amicizie
            var loggedInUserId = int.Parse(User.Identity.Name);
            var isFriend = db.Friendships.Any(f =>
                (f.UserMittenteId == loggedInUserId && f.UserRiceventeId == id.Value));

            // recupera tutte le amicie dell'utente loggato

            var friendships = db.Friendships
            .Where(f => f.UserMittenteId == loggedInUserId)
            .ToList();

            // Assumi che ci sia una relazione tra Users e Posts nel tuo database
            var posts = db.Posts.Where(p => p.UserId == id)
                    .OrderByDescending(p => p.PostedAt)
                    .ToList();

            // Crea un dizionario degli usernames basato solo su queste amicizie
            var friendUsernames = friendships
                .Select(f => db.Users.Find(f.UserRiceventeId))
                .ToDictionary(u => u.UserId, u => u.Username);

            ViewBag.FriendUsernames = friendUsernames;

            // Crea un nuovo ViewModel se non ne hai già uno che includa sia i dati dell'utente che i post
            var viewModel = new UserProfileViewModel
            {
                Users = user,
                Posts = posts,
                LoggedInUserId = User.Identity.Name,
                IsFriend = isFriend,
                Friendships = friendships
            };

            return View(viewModel); 
        }


        // GET: Users/Create
        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.AvailableAvatars = db.Avatars.ToList();

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }



        // POST: Users/Create
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,Email,Nome,Cognome,Role,Stato,CodiceFiscale,AvatarId")] Users users, string confermaPassword)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.AvailableAvatars = db.Avatars.ToList();

            if (users.Password != confermaPassword)
            {
                TempData["error"] = "Le password non combaciano";
                return View(users);
            }

            if (ModelState.IsValid)
            {
                if (!users.AvatarId.HasValue)
                {
                    users.AvatarId = 20;
                }

                if (users.AvatarId.HasValue)
                {
                    users.Avatars = db.Avatars.Find(users.AvatarId.Value);
                }

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
                return RedirectToAction("Login", "Auth");
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
            Users user = db.Users.Include(u => u.Avatars) 
                                 .FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }

           
            var availableAvatars = db.Avatars.ToList();
            ViewBag.AvailableAvatars = availableAvatars; 

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Email,Nome,Cognome,Role,Stato,CodiceFiscale,Password,AvatarId")] Users users, string OldPassword, string confermaPassword)
        {

            // Ricarica l'avatar dall'ID se è presente
            if (users.AvatarId.HasValue)
            {
                users.Avatars = db.Avatars.Find(users.AvatarId.Value);
            }
            else
            {
                // Se l'AvatarId non è presente, ricarica l'utente con l'oggetto Avatars incluso
                users = db.Users.Include(u => u.Avatars).FirstOrDefault(u => u.UserId == users.UserId);
            }

            ViewBag.AvailableAvatars = db.Avatars.ToList();
            
            // Controlla prima se il modello è valido
            if (ModelState.IsValid)
            {
                Users userInDb = db.Users.Find(users.UserId);
                if (userInDb == null)
                {
                    return HttpNotFound();
                }

                // Controllo che l'email non sia già usata da un altro utente
                var existingUserByEmail = db.Users
                    .FirstOrDefault(u => u.Email == users.Email && u.UserId != users.UserId);
                if (existingUserByEmail != null)
                {
                    TempData["error"] = "L'email esiste già";
                }

                // Controllo che il codice fiscale non sia già usato da un altro utente
                var existingUserByCodiceFiscale = db.Users
                    .FirstOrDefault(u => u.CodiceFiscale == users.CodiceFiscale && u.UserId != users.UserId);
                if (existingUserByCodiceFiscale != null)
                {
                    TempData["error"] = "Il Codice Fiscale esiste già";
                }

                // Controllo che lo username non sia già usato da un altro utente
                var existingUserByUsername = db.Users
                    .FirstOrDefault(u => u.Username == users.Username && u.UserId != users.UserId);
                if (existingUserByUsername != null)
                {
                    TempData["error"] = "Lo username esiste già";
                }

                // Controlla se l'username è uguale al nome
                var usernameEqualToName = users.Username.Equals(users.Nome, StringComparison.OrdinalIgnoreCase);
                if (usernameEqualToName)
                {
                    TempData["error"] = "Lo username non può essere uguale al nome";
                }

                // Controllo della password
                if (!String.Equals(userInDb.Password, OldPassword))
                {
                    TempData["error"] = "La vecchia password non è corretta.";
                    // Ricarica l'avatar dall'ID se è presente
                    if (users.AvatarId.HasValue)
                    {
                        users.Avatars = db.Avatars.Find(users.AvatarId.Value);
                    }
                    else
                    {
                        // Se l'AvatarId non è presente, ricarica l'utente con l'oggetto Avatars incluso
                        users = db.Users.Include(u => u.Avatars).FirstOrDefault(u => u.UserId == users.UserId);
                    }

                    ViewBag.AvailableAvatars = db.Avatars.ToList();
                    return View(users);
                }
                else if (!string.IsNullOrWhiteSpace(users.Password))
                {
                    if (users.Password == confermaPassword)
                    {
                        // Aggiorna la password
                        userInDb.Password = users.Password;
                    }
                    else
                    {
                        TempData["error"] = "Le password non combaciano.";
                        // Ricarica l'avatar dall'ID se è presente
                        if (users.AvatarId.HasValue)
                        {
                            users.Avatars = db.Avatars.Find(users.AvatarId.Value);
                        }
                        else
                        {
                            // Se l'AvatarId non è presente, ricarica l'utente con l'oggetto Avatars incluso
                            users = db.Users.Include(u => u.Avatars).FirstOrDefault(u => u.UserId == users.UserId);
                        }

                        ViewBag.AvailableAvatars = db.Avatars.ToList();
                        return View(users);
                    }
                }

                // Se non ci sono errori relativi alla password
                if (!ModelState.Values.Any(x => x.Errors.Count > 0))
                {
                    // Aggiorna le altre proprietà dell'utente
                    userInDb.Username = users.Username;
                    userInDb.Email = users.Email;
                    userInDb.Nome = users.Nome;
                    userInDb.Cognome = users.Cognome;
                    userInDb.Role = users.Role;
                    userInDb.Stato = users.Stato;
                    userInDb.CodiceFiscale = users.CodiceFiscale;
                    userInDb.AvatarId = users.AvatarId.HasValue ? users.AvatarId : userInDb.AvatarId;

                    // Salva le modifiche nel database
                    db.Entry(userInDb).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["success"] = "Modifiche avvenute con successo.";
                    return RedirectToAction("Index", "Posts");
                }
            }
            if (!ModelState.IsValid)
            {
                // Ricarica l'avatar dall'ID se è presente
                if (users.AvatarId.HasValue)
                {
                    users.Avatars = db.Avatars.Find(users.AvatarId.Value);
                }
                else
                {
                    // Se l'AvatarId non è presente, ricarica l'utente con l'oggetto Avatars incluso
                    users = db.Users.Include(u => u.Avatars).FirstOrDefault(u => u.UserId == users.UserId);
                }

                ViewBag.AvailableAvatars = db.Avatars.ToList();
                return View(users);
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

            user.IsDeleted = true;
            db.Entry(user).State = EntityState.Modified;
            TempData["success"] = "Profilo eliminato correttamente. Arrivederci 💔";
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
