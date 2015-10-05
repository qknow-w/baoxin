using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaoXin.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
         "Account",                                              // Route name
         "Account/{action}/{id}",                           // URL with parameters
         new { controller = "Account", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
      );
            routes.MapRoute(
           "Chat",                                              // Route name
           "Chat/{action}/{id}",                           // URL with parameters
           new { controller = "Chat", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
        );
            routes.MapRoute(
            "Speach",                                              // Route name
            "Speach/{action}/{id}",                           // URL with parameters
            new { controller = "Speach", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );
            routes.MapRoute(
            "Unity",                                              // Route name
            "Unity/{action}/{id}",                           // URL with parameters
            new { controller = "Unity", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );
            routes.MapRoute(
                "Home",                                              // Route name
                "{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );
           
          
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }

       
    }
}