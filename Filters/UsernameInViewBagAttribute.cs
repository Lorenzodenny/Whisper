using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisper.Models;

namespace Whisper.Filters
{
    public class UsernameInViewBagAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.HttpContext.User.Identity.Name;
            if (int.TryParse(userId, out var uid))
            {
                using (var db = new DBContext())
                {
                    var user = db.Users.Find(uid);
                    if (user != null)
                    {
                        filterContext.Controller.ViewBag.Username = user.Username;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}