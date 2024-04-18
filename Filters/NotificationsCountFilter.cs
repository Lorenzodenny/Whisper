using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;


namespace Whisper.Filters
{
    public class NotificationsCountFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(filterContext.HttpContext.User.Identity.Name);
                using (var db = new DBContext()) 
                {
                    filterContext.Controller.ViewBag.FriendshipNotificationsCount = db.Notifications
                        .Count(n => n.FriendshipID.HasValue && n.ReadStatus == false && n.UserID == userId);

                    filterContext.Controller.ViewBag.MessageNotificationsCount = db.Notifications
                        .Count(n => n.ConversationID.HasValue && n.ReadStatus == false && n.UserID == userId);

                    filterContext.Controller.ViewBag.GeneralNotificationsCount = db.Notifications
                        .Count(n => (n.LikeID.HasValue || n.CommentID.HasValue || n.PostID.HasValue) && n.ReadStatus == false && n.UserID == userId);
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
