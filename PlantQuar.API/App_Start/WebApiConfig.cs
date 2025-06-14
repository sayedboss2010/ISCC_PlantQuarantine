using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API
{
    public static class WebApiConfig
    {
        //test
        public static void Register(HttpConfiguration config)
        {


            // To enable CORS for all Web API controllers in your application

            // var cors = new EnableCorsAttribute("www.example.com", "*", "*");
            // config.EnableCors(cors);

            // Web API configuration and services

            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;



            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultRpc",
                routeTemplate: "Rpc/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
