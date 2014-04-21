using System.Web.Optimization;

namespace Burgerama.Web.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/vendor/angularjs/angular.js",
                "~/Scripts/vendor/angularjs/angular-resource.js",
                "~/Scripts/vendor/angularjs/angular-route.js",
                "~/Scripts/vendor/angularjs/angular-animate.js",
                "~/Scripts/vendor/angularjs/angular-cookies.js",

                "~/Scripts/vendor/auth0/auth0-angular.js",
                "~/Scripts/vendor/auth0/auth0-widget.js",

                "~/Scripts/vendor/angular-local-storage/angular-local-storage.js",
                "~/Scripts/vendor/angular-ui-bootstrap/ui-bootstrap-tpls.js",
                "~/Scripts/vendor/angular-toaster/angular-toaster.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/burgerama")
                .IncludeDirectory("~/Scripts/app/", "*.js", true));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/bootstrap-theme.css",
                "~/Scripts/vendor/angular-toaster/angular-toaster.css",
                "~/Content/css/default.css"
            ));

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}