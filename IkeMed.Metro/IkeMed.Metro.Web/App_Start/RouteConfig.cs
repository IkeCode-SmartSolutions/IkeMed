using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IkeMed.Metro.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Helpers

            routes.MapRoute(
                name: "Helpers",
                url: "helpers/{action}",
                defaults: new { controller = "Helpers" }
            );

            #endregion Helpers

            #region Person

            routes.MapRoute(
                name: "Person",
                url: "cadastro/pessoa/{id}",
                defaults: new
                {
                    controller = "Person",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Post_Person",
                url: "salvar/pessoa/{id}",
                defaults: new { controller = "Person", action = "Post" }
            );

            #endregion Person

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
