using System.Web;
using System.Web.Http;
using Burgerama.Services.Voting.Api;
using Burgerama.Services.Voting.Core.DI;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Burgerama.Services.Voting.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new HttpDependencyResolver();

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
