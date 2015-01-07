using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;
using IkeCode.Core.Mvc;

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
                    "~/Scripts/jtable/jquery.jtable.js",
                    "~/Scripts/jtable/localization/jquery.jtable.pt-BR.js"));

            var ikeMedBundle = new ScriptBundle("~/bundles/ikemed");
            ikeMedBundle.Orderer = new AsDefinedBundleOrderer();
            ikeMedBundle.Include("~/Scripts/common.js");
            ikeMedBundle.Include("~/Scripts/knockout-3.2.0.js");
            ikeMedBundle.Include("~/Scripts/ikeNotify.js");
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

            var styleBundle = new StyleBundle("~/Content/styles");
            styleBundle.Orderer = new AsDefinedBundleOrderer();
            styleBundle.Include("~/css/iconFont.min.css");
            styleBundle.Include("~/css/metro-bootstrap.css");
            styleBundle.Include("~/css/metro-bootstrap-responsive.css");
            styleBundle.Include("~/css/metro-alert.css");
            styleBundle.Include("~/css/metro-alert-theme-*");
            styleBundle.Include("~/css/metros-docs.css");
            styleBundle.Include("~/css/common.css");
            styleBundle.Include("~/Content/themes/base/jquery-ui.css");
            styleBundle.Include("~/Scripts/jtable/themes/metro/lightgray/jtable.css");
            bundles.Add(styleBundle);
        }
    }
}
