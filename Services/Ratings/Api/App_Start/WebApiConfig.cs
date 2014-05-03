using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics.Contracts;
using System.Web.Http;

namespace Burgerama.Services.Ratings.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Contract.Requires<ArgumentException>(config != null);

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Register Web API routes.
            config.MapHttpAttributeRoutes();
        }
    }
}
