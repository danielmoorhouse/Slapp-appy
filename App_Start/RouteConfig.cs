using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SlappMain
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new
             {
                 controller = "Home",
                 action = "Index",
                 id = UrlParameter.Optional
             }
             );
            routes.MapRoute(
           name: "More Details",
           url: "Apps/Details/{id}",
           defaults: new
           {
               controller = "Apps",
               action = "Details",
               id = UrlParameter.Optional
           },
             constraints: new
             {
                 //Route only applies with this subname
                 subName = "Apps"
             }
           );

            routes.MapRoute(
                name: "Add Company (App)",
                url: "Develops/Create/{subName}/{AppId}",
                defaults: new
                {
                    controller = "Develops",
                    action = "Create",
                    AppId = UrlParameter.Optional
                },
                constraints: new
                {
                    //Route only applies with this subname
                    subName = "App"
                }
            );
            routes.MapRoute(
          name: "Add Company (Company)",
          url: "Develops/Create/{subName}/{CompId}",
          defaults: new
          {
              controller = "Develops",
              action = "Create",
              AppId = UrlParameter.Optional
          },
          constraints: new
          {
                    //Route only applies with this subname
                    subName = "Company"
          }
      );

        }
    }
}
