using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Mvc;

namespace PezeshkGit
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "PostsAPI",
                "api/{controller}/{action}/{id}",
                new { controller = "Post", id = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "MessagesAPI",
                "api/{controller}/{action}/{id}",
                new { controller = "Message", id = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "CommentsAPI",
                "api/{controller}/{action}/{id}",
                new { controller = "Comment", id = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "GalleriesAPI",
                "api/{controller}/{action}/{id}",
                new { controller = "Gallery", id = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
