using System.Web;
using System.Web.Http;
using Burgerama.Services.Users.Api;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Burgerama.Services.Users.Api
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
