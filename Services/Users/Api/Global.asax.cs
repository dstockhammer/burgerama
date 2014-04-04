using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Burgerama.Services.Users.Api
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
