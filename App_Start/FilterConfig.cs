using System.Web;
using System.Web.Mvc;
using Whisper.Filters;

namespace Whisper
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CheckBanStatusFilter());
            filters.Add(new UsernameInViewBagAttribute());
            filters.Add(new NotificationsCountFilter());
        }
    }
}
