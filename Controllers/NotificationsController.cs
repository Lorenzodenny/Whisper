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

        [HttpPost]
        public ActionResult MarkAllPostsLikesCommentsAsRead()
        {
            var userId = int.Parse(User.Identity.Name);
            var notificationsToMarkRead = db.Notifications
                                             .Where(n => n.UserID == userId
                                                         && (n.PostID.HasValue || n.LikeID.HasValue || n.CommentID.HasValue)
                                                         && (!n.ReadStatus.HasValue || !n.ReadStatus.Value))
                                             .ToList();

            if (!notificationsToMarkRead.Any())
            {
                return Json(new { success = false, message = "Nessuna notifica da aggiornare." });
            }

            foreach (var notification in notificationsToMarkRead)
            {
                notification.ReadStatus = true;
            }

            db.SaveChanges();

            return Json(new { success = true, message = "Tutte le notifiche di post, like e commenti sono state segnate come lette." });
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

        public ActionResult MarkAllFriendshipAsRead()
        {
            var userId = int.Parse(User.Identity.Name);
            var friendshipNotifications = db.Notifications
                         .Where(n => n.UserID == userId
                                     && n.FriendshipID.HasValue // Solo le notifiche di amicizia
                                     && (!n.ReadStatus.HasValue || !n.ReadStatus.Value)) // Non lette
                         .ToList();

            if (!friendshipNotifications.Any())
            {
                return Json(new { success = false, message = "Nessuna notifica di amicizia da aggiornare." });
            }

            foreach (var notification in friendshipNotifications)
            {
                notification.ReadStatus = true;
            }

            db.SaveChanges();

            return Json(new { success = true, message = "Tutte le notifiche di amicizia sono state segnate come lette." });
        }


        // GET : Messaggi

        public ActionResult Conversations()
        {
            CleanupReadNotifications();
            int userId = int.Parse(User.Identity.Name);
            ViewBag.CurrentUserId = userId;

            // Ottieni solo le conversazioni che non sono state cancellate dall'utente corrente
            var userConversations = db.Conversations
                                      .Include(c => c.Notifications)
                                      .Where(c => (c.User1Id == userId && !c.User1Deleted) ||
                                                  (c.User2Id == userId && !c.User2Deleted))
                                      .ToList();

            var users = db.Users.ToDictionary(u => u.UserId, u => u);
            ViewBag.Users = users;

            return View(userConversations);
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