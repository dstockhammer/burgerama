using System.Web;
using System.Web.Http;
using Burgerama.Common.Authentication.Owin;
using Burgerama.Services.Voting.Core.DI;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(InitializeAuthentication))]

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
