using System.Web;
using System.Web.Http;
using Burgerama.Services.Outings.Api;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Burgerama.Services.Outings.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
