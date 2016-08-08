using System.Web.Optimization;

namespace UserManagment.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/avatar").Include(
                        "~/Scripts/avatars/jquery.min.js",
                        "~/Scripts/avatars/resample.js",
                        "~/Scripts/avatars/avatar.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/tables/jquery.min.js",
                        "~/Scripts/tables/jquery.dataTables.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/tables/jquery.dataTables.min.css",
                      "~/Content/Site.css"));
        }
    }
}
