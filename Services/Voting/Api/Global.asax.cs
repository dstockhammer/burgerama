using Burgerama.Services.Voting.Api;
using Microsoft.Owin;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace Burgerama.Services.Voting.Api
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
