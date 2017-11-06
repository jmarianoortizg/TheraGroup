using GrupoThera.WebUI.Controllers;
using GrupoThera.WebUI.Controllers.api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Runtime.Serialization.Formatters;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace GrupoThera.WebUI.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var settings =  config.Formatters.JsonFormatter.SerializerSettings;
                            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
                            settings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffZ";
                            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                            settings.DateParseHandling = DateParseHandling.DateTimeOffset;
                            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                            settings.NullValueHandling = NullValueHandling.Ignore;
                            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                            settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
                            settings.TypeNameHandling = TypeNameHandling.None;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "rs/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                name: "DefaultQuery",
                routeTemplate: "rs/{controller}/q/{query}",
                defaults: new { query = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
            .Add(new RequestHeaderMapping("Accept",
                                          "text/html",
                                          StringComparison.InvariantCultureIgnoreCase,
                                          true,
                                          "application/json"));
            
        }
    }
}