using System;
using System.Diagnostics.Contracts;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Burgerama.Services.Users.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Contract.Requires<ArgumentException>(config != null);

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
