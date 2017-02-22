using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KamikazeTrungThanh.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "About Index",
                url: "about-index.html",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional},
                 namespaces: new string[] { "KamikazeTrungThanh.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Portfolio Index",
                url: "portfolio-index.html",
                defaults: new { controller = "Portfolio", action = "Layouts", alias = "", id = 0 },
                 namespaces: new string[] { "KamikazeTrungThanh.Web.Controllers" }
            );


            routes.MapRoute(
                name: "Portfolio Layouts",
                url: "portfolio-{alias}-{id}.html",
                defaults: new { controller = "Portfolio", action = "Layouts", id = UrlParameter.Optional },
                 namespaces: new string[] { "KamikazeTrungThanh.Web.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "KamikazeTrungThanh.Web.Controllers" }
            );
        }
    }
}
