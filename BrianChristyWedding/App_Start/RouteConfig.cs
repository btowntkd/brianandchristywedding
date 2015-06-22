using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BrianChristyWedding
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "InvitationCodePrompt",
                url: "rsvp/",
                defaults: new { controller = "Rsvp", action = "Index" }
            );

            routes.MapRoute(
                name: "RSVP",
                url: "rsvp/{shortcode}",
                defaults: new { controller = "Rsvp", action = "Rsvp" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
