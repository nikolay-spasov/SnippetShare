namespace SnippetShare
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Short",
                url: "Snippet/{id}",
                defaults: new { controller = "Home", action = "Show" });

            routes.MapRoute(
                name: "embedded",
                url: "Embedded/{id}/{follow}",
                defaults: new { controller = "Home", action = "Embedded", follow = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "CreateSnippet", id = UrlParameter.Optional });
        }
    }
}