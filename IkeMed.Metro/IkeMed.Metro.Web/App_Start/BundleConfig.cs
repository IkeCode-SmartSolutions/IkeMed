using System.Web;
using System.Web.Optimization;

namespace IkeMed.Metro.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/js/jquery.mousewheel.js",
                    "~/js/jquery.widget.min.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/jquery.ext.js",
                    "~/Scripts/jquery-ui-1.9.2.min.js",
                    "~/Scripts/jtable/jquery.jtable.min.js"));

            var ikeMedBundle = new ScriptBundle("~/bundles/ikemed");
            ikeMedBundle.Include("~/Scripts/common.js");
            ikeMedBundle.Include("~/Scripts/knockout-3.2.0.js");
            ikeMedBundle.Include("~/Scripts/ko-notify.js");
            bundles.Add(ikeMedBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
                    "~/js/metro.min.js",
                    "~/js/metro/metro-*",
                    "~/js/ext-plugins/metro-alert.js",
                    "~/js/prettify.js",
                    "~/js/holder.js",
                    "~/js/metro.docs.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/css/iconFont.min.css",
                    "~/css/metro-bootstrap.css",
                    "~/css/metro-bootstrap-responsive.css",
                    "~/css/metro-alert.css",
                    "~/css/metro-alert-theme-*",
                    "~/css/metros-docs.css",
                    "~/css/common.css",
                    "~/Content/themes/base/jquery-ui.css",
                    "~/Scripts/jtable/themes/metro/lightgray/jtable.min.css",
                    "~/Scripts/jtable/themes/metro/jtable_metro_base.min.css"));
        }
    }
}
