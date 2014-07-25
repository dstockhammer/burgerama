using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace Burgerama.Web.Maintenance
{
    public sealed class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            Contract.Requires<ArgumentException>(routes != null);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
