using System.Web;
using System.Web.Optimization;

namespace BexMediaCom
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqeryvaladditional").Include(
                         "~/Scripts/additional-methods.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                       "~/Content/plugins/datatables/dataTables.min.js",
                       "~/Content/plugins/datatables/dataTables.bootstrap.min.js",
                       "~/Content/plugins/datatables/js/dataTables.buttons.min.js",
                       "~/Content/plugins/datatables/js/buttons.flash.min.js",
                       "~/Content/plugins/datatables/js/jszip.min.js",
                       "~/Content/plugins/datatables/js/pdfmake.min.js",
                       "~/Content/plugins/datatables/js/vfs_fonts.js",
                       "~/Content/plugins/datatables/js/buttons.html5.min.js",
                       "~/Content/plugins/datatables/js/buttons.print.min.js"));

            bundles.Add(new StyleBundle("~/Content/dataTables").Include(
                     "~/Content/plugins/datatables/dataTables.min.css",
                     "~/Content/plugins/datatables/css/buttons.dataTables.min.css"));




            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/bootstrap/js/bootstrap.min.js",
                      "~/Content/jquery-ui-1.12.1/jquery-ui.min.js",
                      "~/Content/plugins/sparkline/jquery.sparkline.min.js",
                      "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      "~/Content/plugins/knob/jquery.knob.js",
                      "~/Content/moment.min.js",
                      "~/Content/plugins/daterangepicker/daterangepicker.js",
                      "~/Content/plugins/datepicker/bootstrap-datepicker.js",
                      "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/plugins/fastclick/fastclick.js",
                      "~/Content/dist/js/app.min.js",
                      "~/Content/dist/js/demo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/dist/css/skins/_all-skins.min.css",
                      "~/Content/plugins/iCheck/flat/blue.css",
                      "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                      "~/Content/plugins/datepicker/datepicker3.css",
                      "~/Content/plugins/daterangepicker/daterangepicker-bs3.css",
                      "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/Content/ionicons-2.0.1/css/ionicons.min.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                      "~/Content/plugins/Datetimepicker/themes/classic.css",
                      "~/Content/plugins/Datetimepicker/themes/classic.date.css"));


            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/Content/plugins/Datetimepicker/compressed/picker.js",
                      "~/Content/plugins/Datetimepicker/compressed/picker.date.js"));

            // Use this section for pop up alert ( sweet alert 2)
            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                      "~/Content/plugins/PopUp/sweetalert.css"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                     "~/Content/plugins/PopUp/sweetalert.min.js"));

        }
    }
}
