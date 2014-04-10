using System.Web;
using System.Web.Http;

namespace Burgerama.Services.Venues.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(AutofacConfig.Register);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
