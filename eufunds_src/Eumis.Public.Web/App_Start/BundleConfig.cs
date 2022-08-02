using System;
using System.Web.Optimization;

namespace Eumis.Public.Web
{
    public static class BundleConfig
    {
        public const string PubJs = "~/bundles/pubjs";
        public const string PubActionJs = "~/bundles/pubactionjs";
        public const string PubCss = "~/bundles/pubcss";
        public const string Highmaps = "~/bundles/highmaps";
        public const string Highcharts = "~/bundles/highcharts";
        public const string PageHomeDefault = "~/bundles/pages/home-default";
        public const string BgMapJs = "~/bundles/bgMapJs";

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException(nameof(bundles));
            }

            bundles.IgnoreList.Clear();

            RegisterCommon(bundles); // libraries

            RegisterIndividual(bundles); // singular modules

            RegisterPages(bundles); // page-specific code

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }

        public static void RegisterCommon(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException(nameof(bundles));
            }

            // Libraries
            bundles.Add(new ScriptBundle(PubJs).Include(
                    "~/Scripts/jquery-*",
                    "~/Scripts/jquery.easing.1.3.js",
                    "~/Scripts/jquery.ba-resize.min.js",
                    "~/Scripts/autogrow.min.js",
                    "~/Scripts/select2.min.js",
                    "~/Scripts/select2_locale_bg.js",
                    "~/Scripts/select2_locale_en.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/bootstrap-switch.min.js",
                    "~/Scripts/bootstrap-tabdrop.js",
                    "~/Scripts/bootstrap-datepicker.js",
                    "~/Scripts/bootstrap-datepicker.bg.js",
                    "~/Scripts/bootstrap-datepicker.en.js",
                    "~/Scripts/bootstrap-confirmation.min.js",
                    "~/Scripts/jquery.lightbox.min.js",
                    "~/Scripts/lodash-*",
                    "~/Scripts/responsive-calendar.min.js"));

            // Action scripts
            bundles.Add(new ScriptBundle(PubActionJs).Include(
                "~/Scripts/scripts.js",
                "~/Scripts/scripts-custom.js"));

            bundles.Add(new ScriptBundle(BgMapJs).Include(
                        "~/Scripts/map/Map.js",
                        "~/Scripts/map/MapData.js"));

            // Styles bundle
            bundles.Add(new StyleBundle(PubCss)
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-switch.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/datepicker.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/select2/select2.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/tabdrop.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/custom-style.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/lightbox/jquery.lightbox.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/custom-public.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/responsive-calendar.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/tree/jquery.treetable.css", new CssRewriteUrlTransform()));
        }

        public static void RegisterIndividual(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException(nameof(bundles));
            }

            bundles.Add(new ScriptBundle(Highmaps)
                .Include(
                    "~/Scripts/highmaps-*",
                    "~/Scripts/tree/jquery.treetable.js"));

            bundles.Add(new ScriptBundle(Highcharts)
                .Include("~/Scripts/highcharts-*"));
        }

        public static void RegisterPages(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException(nameof(bundles));
            }

            bundles.Add(new ScriptBundle(PageHomeDefault).Include("~/Scripts/Pages/HomeDefault.js"));
        }
    }
}