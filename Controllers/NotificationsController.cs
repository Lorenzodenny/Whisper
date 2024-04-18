using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class NotificationsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: post / comment / like
        public ActionResult Index()
        {
            CleanupReadNotifications();
            var userId = int.Parse(User.Identity.Name);
            var userNotifications = db.Notifications
                                      .Include(n => n.Users1)
                                      .Where(n => n.UserID == userId && (n.LikeID.HasValue || n.CommentID.HasValue || n.PostID.HasValue))
                                      .OrderByDescending(n => n.NotificationDate)
                                      .ToList();

            return View(userNotifications);
        }


        // GET : Amicizie

        public ActionResult Friendships()
        {
            CleanupReadNotifications();
            var userId = int.Parse(User.Identity.Name);
            var userNotifications = db.Notifications
                                      .Include(n => n.Users1)
                                      .Where(n => n.UserID == userId && n.FriendshipID.HasValue)
                                      .OrderByDescending(n => n.NotificationDate)
                                      .ToList();

            return View(userNotifications);
        }

        // GET : Messaggi

        public ActionResult Messages()
        {
            CleanupReadNotifications();
            var userId = int.Parse(User.Identity.Name);
            var userNotifications = db.Notifications
                                      .Include(n => n.Users1)
                                      .Where(n => n.UserID == userId && n.ConversationID.HasValue)
                                      .OrderByDescending(n => n.NotificationDate)
                                      .ToList();

            return View(userNotifications);
        }

        // Metodo per aggiornare lo stato di una notifica a "letta"
        [HttpPost]
        public ActionResult MarkNotificationAsRead(int notificationId)
        {
            var notification = db.Notifications.Find(notificationId);
            if (notification != null && notification.UserID.ToString() == User.Identity.Name)
            {
                notification.ReadStatus = true;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Notifica non trovata o utente non autorizzato." });
        }

        // Pulizia delle notifiche lette più vecchie di 24 ore
        private void CleanupReadNotifications()
        {
            var rangeDiControllo = DateTime.UtcNow.AddDays(-1);
            var oldNotifications = db.Notifications
                                     .Where(n => n.ReadStatus == true && n.NotificationDate <= rangeDiControllo)
                                     .ToList();

            db.Notifications.RemoveRange(oldNotifications);
            db.SaveChanges();
        }

    }
}