using System.Web.Mvc;
using System.Web.Routing;

namespace UserManagment.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Empty",
                "",
                new { controller = "User", action = "Index", id = UrlParameter.Optional }
                ).DataTokens = new RouteValueDictionary(new { area = "UserControlArea" });

            routes.MapRoute(
               "UserDelete",
               "UserControlArea/User/{action}/{id}",
               new { controller = "User", action = "Delete", id = UrlParameter.Optional },
               namespaces: new[] { "UserManagment.Web.Areas.UserControlArea.Controllers" })
               .DataTokens = new RouteValueDictionary(new { area = "UserControlArea" });

            routes.MapRoute(
               "UserUpdate",
               "UserControlArea/User/{action}/{id}",
               new { controller = "User", action = "Update", id = UrlParameter.Optional },
               namespaces: new[] { "UserManagment.Web.Areas.UserControlArea.Controllers" })
               .DataTokens = new RouteValueDictionary(new { area = "UserControlArea" }); 

            routes.MapRoute(
               "UserControl",
               "UserControlArea/{controller}/{action}/{id}",
               new { controller = "User", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "UserManagment.Web.Areas.UserControlArea.Controllers" }); 

            routes.MapRoute(
                name: "Default",
                url: "{area}/{controller}/{action}/{id}",
                defaults: new { area = "UserControlArea", controller = "User", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}
