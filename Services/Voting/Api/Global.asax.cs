using System.Web;
using System.Web.Http;
using Burgerama.Services.Voting.Core.DI;

namespace Burgerama.Services.Voting.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.DependencyResolver = new HttpDependencyResolver();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
