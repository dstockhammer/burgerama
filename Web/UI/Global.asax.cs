using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Burgerama.Web.UI
{
    public class MvcApplication : HttpApplication
    {
        private readonly bool isHttpsRedirectionEnabled;

        public MvcApplication()
        {
            var securityRedirectionConfig = ConfigurationManager.AppSettings["security:isHttpsRedirectionEnabled"];
            isHttpsRedirectionEnabled = securityRedirectionConfig != null && securityRedirectionConfig.ToLower() == "true";
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            if (isHttpsRedirectionEnabled && !Context.Request.IsSecureConnection)
                Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
        }
    }
}
