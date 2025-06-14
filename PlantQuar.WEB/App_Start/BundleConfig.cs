using System.Web.Optimization;

namespace PlantQuar.WEB.App_Start
{
    /// <summary>
    /// /////////mai
    /// </summary>
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval-ar").Include(
                        "~/scripts/jquery.validate.js",
                        "~/scripts/jquery.validationEngine.js",
                        "~/Scripts/jquery.validationEngine-ar.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval-en").Include(
                        "~/scripts/jquery.validate.js",
                        "~/scripts/jquery.validationEngine.js",
                        "~/Scripts/jquery.validationEngine-en.js"));

            bundles.Add(new ScriptBundle("~/bundles/jtable-ar").Include(
                       "~/Scripts/jtable/jtable.2.4.0/jtable.2.4.0/jquery.jtable.js",
                       "~/Scripts/jtable/jtable.2.4.0/jtable.2.4.0/jquery.jtable.min.js"
                       ));


            bundles.Add(new ScriptBundle("~/bundles/jtable-en").Include(
                    "~/Scripts/jtable/jtable.2.4.0/jtable.2.4.0/jquery.jtable.js",
                       "~/Scripts/jtable/jtable.2.4.0/jtable.2.4.0/jquery.jtable.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
                       "~/Scripts/alertify/alertify.js",
                        "~/Scripts/alertify/alertify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //     "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/TabStyle.css",
                       //  "~/Content/FileUploadStyle.css",
                       "~/Content/JtableCustomStyle.css",
                      "~/Content/alertify/alertify.bootstrap.css",
                      "~/Content/alertify/alertify.core.css",
                      "~/Content/alertify/alertify.default.css",
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Scripts/jtable/themes/metro/lightgray/jtable.css"));
        }
    }
}