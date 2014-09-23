﻿using System;
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

            routes.MapRoute(
                name: "RegisterPerson",
                url: "cadastro/pessoa/{id}/{personType}",
                defaults: new
                {
                    controller = "RegisterPerson",
                    action = "Index",
                    id = UrlParameter.Optional,
                    personType = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Post_RegisterPerson",
                url: "salvar/pessoa/{id}",
                defaults: new { controller = "RegisterPerson", action = "Post" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
