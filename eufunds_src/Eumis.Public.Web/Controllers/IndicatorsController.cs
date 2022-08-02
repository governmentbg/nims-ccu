using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Indicators;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class IndicatorsController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public IndicatorsController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IUmisRepository umisRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                    "programmeId",
                    "page",
            })]
        public virtual ActionResult Index(
                string programmeId = "",
                bool showRes = false,
                string page = "")
        {
            this.ModelState.Clear();

            IndicatorsSearchVM vm = new IndicatorsSearchVM()
            {
                ProgrammeId = programmeId,
                ShowRes = showRes,
            };

            this.FillModelValues(ref vm);

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var indicators = this.umisRepository.GetStatisticIndicators(
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage,
                    isEn: SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English);

                vm.SearchResults = new StaticPagedList<StatisticIndicatorVO>(indicators.Results, innerPage, Configuration.MaxItemsPerPage, indicators.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(IndicatorsSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (!this.ModelState.IsValid)
            {
                this.FillModelValues(ref vm);

                vm.ShowRes = false;

                return this.View(vm);
            }

            vm.ShowRes = true;

            IndicatorsSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            int? programmeId = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeId"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeId"]));
            }

            var indicators = this.umisRepository
                .GetStatisticIndicators(programmeId, 0, null, isEn: SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                .Results.ToList();

            var template = new ExportTemplate("indicators");
            template.Sheet.Name = "indicators";

            var table = new ExportTable(Texts.Indicators_Index_IndicatorsInfo);
            var firstRow = new ExportRow();

            for (int i = 0; i < 10; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Global_OperationalProgram;
            firstRow.Cells[1].Value = Texts.Indicators_Index_Indicator;
            firstRow.Cells[2].Value = Texts.Indicators_Index_Category;
            firstRow.Cells[3].Value = Texts.Indicators_Index_Type;
            firstRow.Cells[4].Value = Texts.Indicators_Index_Trend;
            firstRow.Cells[5].Value = Texts.Indicators_Index_MeasureUnit;
            firstRow.Cells[6].Value = Texts.Indicators_Index_TargetValue;
            firstRow.Cells[7].Value = Texts.Indicators_Index_CumulativeTargetValue;
            firstRow.Cells[8].Value = Texts.Indicators_Index_AchievedValue;
            firstRow.Cells[9].Value = Texts.Indicators_Index_CumulativeAchievedValue;

            table.Rows.Add(firstRow);

            foreach (var indicator in indicators)
            {
                var row = new ExportRow();

                row.Cells.Add(indicator.TransProgrammeShortName.ToExportCell());
                row.Cells.Add(indicator.TransName.ToExportCell());
                row.Cells.Add(indicator.IndicatorType.ToExportCell());
                row.Cells.Add(indicator.IndicatorKind.ToExportCell());
                row.Cells.Add(indicator.IndicatorTrend.ToExportCell());
                row.Cells.Add(indicator.TransMeasuerName.ToExportCell());
                row.Cells.Add(indicator.TargetTotalValue.ToExportCell());
                row.Cells.Add(indicator.AggregatedTarget.ToExportCell());
                row.Cells.Add(indicator.ApprovedPeriodAmountTotal.ToExportCell());
                row.Cells.Add(indicator.AggregatedReport.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 200 },
                { 2, 200 },
                { 3, 200 },
                { 4, 200 },
                { 5, 200 },
                { 6, 200 },
                { 7, 200 },
                { 8, 300 },
                { 9, 200 },
                { 10, 200 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private void FillModelValues(ref IndicatorsSearchVM vm)
        {
            if (vm == null)
            {
                vm = new IndicatorsSearchVM();
            }

            var ops = this.InfrastructureRepository.GetAllOps().OrderBy(e => e.PortalOrderNum).Select(e => new SelectListItem() { Value = e.MapNodeId.ToString(), Text = e.TransName });
            vm.Programs = ops;
        }
    }
}