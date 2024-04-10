using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Whisper.Models;

public class CheckBanStatusFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var user = filterContext.HttpContext.User;
        if (user.Identity.IsAuthenticated)
        {
            using (var db = new DBContext()) 
            {
                var userId = int.Parse(user.Identity.Name);
                var userEntity = db.Users.Find(userId);
                if (userEntity != null && userEntity.Stato == "Bannato")
                {
                    FormsAuthentication.SignOut();

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                        {"controller", "Auth"},
                        {"action", "Login"}
                        }
                    );
                }
            }
        }
    }
}
