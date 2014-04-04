using System.Web.Optimization;

namespace Burgerama.Web.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                // Angular Js
                "~/Scripts/vendor/angular/angular.js",
                "~/Scripts/vendor/angular/angular-resource.js",

                // UI-Bootstrap
                "~/Scripts/vendor/angular-ui/ui-bootstrap-tpls.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/burgerama")
                .IncludeDirectory("~/Scripts/app/", "*.js", true));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/main.css"));

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}