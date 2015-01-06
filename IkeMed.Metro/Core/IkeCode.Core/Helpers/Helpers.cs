using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IkeCode
{
    public static class Helpers
    {
        public static UrlHelper UrlHelper { get { return GetUrlHelper(); } }

        public static UrlHelper GetUrlHelper()
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            return new UrlHelper(new RequestContext(httpContext, CurrentRoute(httpContext)));
        }

        public static RouteData CurrentRoute(HttpContextWrapper httpContext)
        {
            return RouteTable.Routes.GetRouteData(httpContext);
        }
    }
}
