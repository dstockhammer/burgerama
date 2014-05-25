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
                "~/Scripts/vendor/angularjs/angular-animate.js",

                "~/Scripts/vendor/auth0/auth0-angular.js",
                "~/Scripts/vendor/auth0/auth0-widget.js",

                "~/Scripts/vendor/angular-ui-bootstrap/ui-bootstrap-tpls.js",
                "~/Scripts/vendor/angular-ui-utils/ui-utils.js",
                "~/Scripts/vendor/angular-ui-map/ui-map.js",

                "~/Scripts/vendor/angular-local-storage/angular-local-storage.js",
                "~/Scripts/vendor/angular-toaster/angular-toaster.js",
                "~/Scripts/vendor/angular-truncate/truncate.js"

            ));

            bundles.Add(new ScriptBundle("~/bundles/burgerama")
                .IncludeDirectory("~/Scripts/app/", "*.js", true));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Scripts/vendor/angular-toaster/angular-toaster.css",
                "~/Content/less/main.css"
            ));

//#if !DEBUG
//            BundleTable.EnableOptimizations = true;
//#endif
        }
    }
}