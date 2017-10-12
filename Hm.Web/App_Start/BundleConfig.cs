using System.Web;
using System.Web.Optimization;

namespace Hm.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js",
                         "~/Scripts/jquery-ui.min.js",
                            "~/Scripts/select2.full.js",
                         "~/Scripts/jquery-ui-timepicker-addon.js",
                         "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate*",
                       "~/Scripts/globalize/globalize.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/validatedate").Include(
                       "~/Scripts/jquery-validate-date.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/signaturepad").Include(
                    "~/Scripts/jquery.signaturepad.min.js",
                    "~/Scripts/json2.min.js"
                    ));

            bundles.Add(new ScriptBundle("~/ui/tinymce").Include(
               "~/Scripts/tinymce/tinymce.min.js"           
               ));

            bundles.Add(new ScriptBundle("~/bundles/BootstrapScript").Include(
                 "~/Scripts/bootstrap/bootstrap.min.js",
                   "~/Scripts/bootstrap/sidebar_menu.js"
                 ));


            bundles.Add(new StyleBundle("~/Content/BasicCss").Include(
                      "~/Content/HmCommon.css",
                      "~/Content/jquery-ui.css",
                       "~/Content/jquery-ui.theme.min.css",
                      "~/Content/tabcontrol_styles.css",
                           "~/Content/select2.min.css",
                      "~/Content/jquery.signaturepad.css"));

            bundles.Add(new StyleBundle("~/bundles/Bootstrap").Include(
                  "~/Content/Bootstrap/bootstrap.min.css",
                 "~/Content/Bootstrap/font-awesome.min.css",
                   "~/Content/Bootstrap/simple-sidebar.css"));
        }
         
    }
}
