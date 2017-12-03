using System.Web;
using System.Web.Optimization;

namespace GrupoThera.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/assets/js/jquery").Include(
                        "~/Content/assets/js/core/jquery.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/Content/assets/js/bootstrap").Include(
                      "~/Content/assets/js/core/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/assets/css/GrupoTheraTemplateCss").Include(
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/grupoTheraUI.css",
                      "~/Content/assets/css/themes/modern.min.css",
                      "~/Content/plugins/gridforms/gridforms/gridforms.css",
                      "~/Content/assets/js/plugins/sweetalert2/sweetalert2.min.css",
                      "~/Content/assets/css/grupoTheraCustom.css"
            )); 

            bundles.Add(new StyleBundle("~/Content/assets/js/GrupoTheraTemplateJs").Include(
                    "~/Content/assets/js/core/jquery.slimscroll.min.js",
                    "~/Content/assets/js/core/jquery.scrollLock.min.js",
                    "~/Content/assets/js/core/jquery.appear.min.js",
                    "~/Content/assets/js/core/jquery.countTo.min.js",
                    "~/Content/assets/js/core/jquery.placeholder.min.js",
                    "~/Content/assets/js/core/js.cookie.min.js",
                    "~/Content/assets/js/app.js",
                    "~/Content/assets/js/plugins/bootstrap-notify/bootstrap-notify.min.js",
                    "~/Content/plugins/gridforms/gridforms/gridforms.js",
                    "~/Content/assets/js/plugins/loadSpinner/jquery.babypaunch.spinner.js",
                    "~/Scripts/General/StdJavascript.js"
            ));
        }
    }
}