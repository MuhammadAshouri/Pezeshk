using System.Web.Optimization;

namespace PezeshkGit
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.min.js",
                        "~/Scripts/MyScripts/bootbox.js",
                        "~/Scripts/MyScripts/splide.min.js",
                        "~/Scripts/MyScripts/DataTableLangFa.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include("~/Scripts/MyScripts/ckeditor/ckeditor.js"));

            // JPList
            bundles.Add(new ScriptBundle("~/bundles/jplist").Include("~/Scripts/MyScripts/jplist.min.js"));
            bundles.Add(new StyleBundle("~/Content/jplist").Include("~/Content/jplist.styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/Regex").Include(
                      "~/Scripts/MyScripts/RegEx.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/sign.css",
                      "~/Content/ModalBox.css",
                      "~/Content/bootstrap.css",
                      "~/Content/splide.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      "~/Content/custom-scrollbar.css",
                      "~/Content/Public/Header.css",
                      "~/Content/Public/AboutSection.css",
                      "~/Content/Public/FastMenu.css",
                      "~/Content/Public/Content.css",
                      "~/Content/Public/Feedback.css",
                      "~/Content/Public/Footer.css",
                      "~/Content/Public/slider.css",
                      "~/Content/Public/responsive.css"));

            bundles.Add(new StyleBundle("~/Content/DashboardCSS").Include(
                      "~/Content/Dashboard/right-panel.css",
                      "~/Content/Dashboard/left-panel.css",
                      "~/Content/Dashboard/responsive.css"));
        }
    }
}
