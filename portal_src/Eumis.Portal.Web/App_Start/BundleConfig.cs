using System.Web;
using System.Web.Optimization;
using R_10010;

namespace Eumis.Portal.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/upxjs").Include(
                    "~/Scripts/js/jquery-1.11.1.min.js",
                    "~/Scripts/js/jquery.easing.1.3.js",
                    "~/Scripts/js/jquery.ba-resize.min.js",
                    "~/Scripts/js/select2.min.js",
                    "~/Scripts/js/select2_locale_bg.js",
                    "~/Scripts/js/select2_locale_en.js",
                    "~/Scripts/js/bootstrap.min.js",
                    "~/Scripts/js/bootstrap-switch.min.js",
                    "~/Scripts/js/bootstrap-tabdrop.js",
                    "~/Scripts/js/bootstrap-datepicker.js",
                    "~/Scripts/js/bootstrap-datepicker.bg.js",
                    "~/Scripts/js/bootstrap-confirmation.min.js",
                    "~/Scripts/js/upload/jquery.ui.widget.js",
                    "~/Scripts/js/upload/jquery.iframe-transport.js",
                    "~/Scripts/js/upload/jquery.fileupload.js",
                    "~/Scripts/js/tree/src/easyTree.js",
                    "~/Scripts/js/jquery.lightbox.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/upxactionjs").Include(
                    "~/Scripts/js/time.js",
                    "~/Scripts/js/scripts.js",
                    "~/Scripts/js/project-scripts.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                    "~/Scripts/custom/jquery.custom-extensions.js",
                    "~/Scripts/custom/jquery.cascadingDropDown.js",
                    "~/Scripts/custom/html.extensions.js",
                    "~/Scripts/custom/jquery.autogrow-textarea.js",
                    "~/Scripts/custom/switchFunctions.js",
                    "~/Scripts/custom/js.find.extension.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularCore").Include(
                    "~/Scripts/angular/core/angular.js",
                    "~/Scripts/angular/core/angular-resource.js",
                    "~/Scripts/angular/angular-animate.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularDirectives").Include(
                // external
                    "~/Scripts/angular/directives/external/jq.js",
                    "~/Scripts/angular/directives/external/select2.js",
                    "~/Scripts/angular/directives/external/ui-bootstrap-tpls-0.10.0.js",
                // eumis
                    "~/Scripts/angular/directives/scaffolding.js",
                    "~/Scripts/angular/directives/autoGrow.js",
                    "~/Scripts/angular/directives/bootstrapSwitch.js",
                    "~/Scripts/angular/directives/datepicker.js",
                    "~/Scripts/angular/directives/disabled.js",
                    "~/Scripts/angular/directives/enter.js",
                    "~/Scripts/angular/directives/historyBtn.js",
                    "~/Scripts/angular/directives/infoIcon.js",
                    "~/Scripts/angular/directives/number.js",
                    "~/Scripts/angular/directives/money.js",
                    "~/Scripts/angular/directives/symbolsCount.js",
                    "~/Scripts/angular/directives/eumisAddress/eumisAddress.js",
                    "~/Scripts/angular/directives/nomenclature.js",
                    "~/Scripts/angular/directives/eumisCompany/eumisCompany.js",
                    "~/Scripts/angular/directives/confirmClick.js",
                    "~/Scripts/angular/directives/file/fileDirective.js",
                    "~/Scripts/angular/directives/booleanRadio/booleanRadio.js",
                    "~/Scripts/angular/directives/nutsAddress/nutsAddress.js",
                    "~/Scripts/angular/directives/privateNomRadio/privateNomRadio.js",
                    "~/Scripts/angular/directives/nomRadio/nomRadio.js",
                    "~/Scripts/angular/directives/validationPopover.js",
                    "~/Scripts/angular/directives/eumisCompanySearch/eumisCompanySearch.js",
                    "~/Scripts/angular/directives/fireValidationPopover.js",
                    "~/Scripts/angular/directives/nutsAddressAsync/nutsAddressAsync.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularModules").Include(

                    "~/Scripts/angular/modules/_moduleUtils.js",
                    "~/Scripts/angular/modules/moduleEumisNomenclature.js",
                    "~/Scripts/angular/modules/moduleTriggers.js",
                    "~/Scripts/angular/modules/modulePartners.js",
                    "~/Scripts/angular/modules/moduleAttachedDocuments.js",
                    "~/Scripts/angular/modules/moduleIndicators.js",
                    "~/Scripts/angular/modules/moduleContractTeamCollection.js",
                    "~/Scripts/angular/modules/moduleProgrammeContractActivities.js",
                    "~/Scripts/angular/modules/moduleProjectErrandCollection.js",
                    "~/Scripts/angular/modules/moduleDirectionsBudgetContract.js",
                    "~/Scripts/angular/modules/modulePaperAttachedDocuments.js",
                    "~/Scripts/angular/modules/moduleNutsAddress.js",
                    "~/Scripts/angular/modules/moduleCandidate.js",
                    "~/Scripts/angular/modules/moduleEvalTableGroups.js",
                    "~/Scripts/angular/modules/moduleEvalSheetGroups.js",
                    "~/Scripts/angular/modules/moduleProjectSpecFields.js",
                    "~/Scripts/angular/modules/moduleProjectBasicData.js",
                    "~/Scripts/angular/modules/moduleAttachedSignatures.js",
                    "~/Scripts/angular/modules/modulePreliminaryContract.js",
                    "~/Scripts/angular/modules/modulePreliminaryContractActivities.js",

                    "~/Scripts/angular/modules/moduleBFPContract.js",

                    "~/Scripts/angular/modules/moduleProcurements.js",

                    "~/Scripts/angular/modules/modulePaymentRequest.js",

                    "~/Scripts/angular/modules/moduleTechnicalReport.js",

                    "~/Scripts/angular/modules/moduleFinanceReport.js",

                    "~/Scripts/angular/modules/moduleSpendingPlan.js",

                    "~/Scripts/angular/modules/moduleOffer.js",

                    "~/Scripts/angular/modules/moduleCheckListGroups.js",

                    "~/Scripts/angular/modules/moduleCheckSheetGroups.js",

                    "~/Scripts/angular/modules/moduleProjectCommunications.js",

                    "~/Scripts/angular/modules/moduleElectronicDeclarations.js"

                ));

            var upxcss = new StyleBundle("~/bundles/upxcss")
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-switch.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/datepicker.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/select2/select2.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/tabdrop.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/custom-style.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/js/tree/css/easyTree.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/lightbox/jquery.lightbox.min.css", new CssRewriteUrlTransform());

            upxcss.Orderer = new AsDefinedBundleOrderer();

            bundles.Add(upxcss);

            var upxdarkcss = new StyleBundle("~/bundles/upxdarkcss")
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-switch.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/datepicker.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/select2/select2.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/tabdrop.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/dark-style.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/custom-style.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/js/tree/css/easyTree.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/lightbox/jquery.lightbox.min.css", new CssRewriteUrlTransform());

            upxdarkcss.Orderer = new AsDefinedBundleOrderer();

            bundles.Add(upxdarkcss);
        }

        public class AsDefinedBundleOrderer : IBundleOrderer
        {
            #region IBundleOrderer Members

            public System.Collections.Generic.IEnumerable<BundleFile> OrderFiles(BundleContext context, System.Collections.Generic.IEnumerable<BundleFile> files)
            {
                return files;
            }

            #endregion
        }
    }
}
