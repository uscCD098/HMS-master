using System.Web;
using System.Web.Optimization;

namespace StdHMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/LoginJS.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/loginStyle.css",
                      "~/Content/site.css"));

            //AdminMenu js
            bundles.Add(new ScriptBundle("~/bundles/adminmenujs").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/popper.js",
                      "~/Scripts/adminmenumain.js"));


            //AdminMenu CS
            bundles.Add(new StyleBundle("~/Content/adminmenucss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/adminMenuStyle.css",
                      "~/Content/site.css"));

        }
    }
}
