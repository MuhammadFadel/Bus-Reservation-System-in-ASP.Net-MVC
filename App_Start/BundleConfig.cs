using System.Web;
using System.Web.Optimization;

namespace OnlineBusReservationV6
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/respond.js",
                      //For Toastr Message Plugin
                      "~/Scripts/toastr.js",
                      //For Datatables JQuery Plugin
                      "~/Scripts/DataTables/jquery.datatables.js",
                      "~/Scripts/DataTables/datatables.bootstrap.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Template").Include(
                      "~/Scripts/Template/js/jquery.easing.min.js",
                      "~/Scripts/Template/js/jqBootstrapValidation.js",
                      "~/Scripts/Template/js/contact_me.js",
                      "~/Scripts/Template/js/agency.js"));

            bundles.Add(new StyleBundle("~/Content/Template").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      //For Toastr Message Plugin
                      "~/Content/toastr.css",
                      //For Datatables JQuery Plugin
                      "~/Content/DataTables/css/datatables.bootstrap.css",                      
                      "~/Content/Template/css/agency.css"));
            
        }
    }
}
