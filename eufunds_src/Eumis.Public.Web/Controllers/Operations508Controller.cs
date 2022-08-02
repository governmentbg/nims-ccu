using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Operations508;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eumis.Public.Web.Controllers
{
    public partial class Operations508Controller : BaseWithExportController
    {
        private readonly IUmisRepository umisRepository;

        public Operations508Controller(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IUmisRepository umisRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        [HttpGet]
        [DecryptParameters(IdsParamName =
            new[]
            {
                "programmeId",
                "dateFrom",
                "dateTo",
                "page",
            })]
        public virtual ActionResult Index(
            string programmeId = "",
            string dateFrom = "",
            string dateTo = "",
            bool showRes = false,
            string page = "")
        {
            this.ModelState.Clear();

            SearchVM vm = new SearchVM()
            {
                ProgrammeId = programmeId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                ShowRes = showRes,
            };

            DateTime? from = null;
            DateTime? to = null;

            DateTime temp;
            if (DateTime.TryParse(dateFrom, out temp))
            {
                from = temp;
            }

            if (DateTime.TryParse(dateTo, out temp))
            {
                to = temp;
            }

            this.FillModelValues(vm);

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var contracts = this.umisRepository.GetOperations508Report(
                    startDateFrom: from,
                    completionDateTo: to,
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<Operations508ReportVO>(contracts.Results, innerPage, Configuration.MaxItemsPerPage, contracts.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(SearchVM vm)
        {
            DateTime from;
            DateTime to;

            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (DateTime.TryParse(vm.DateFrom, out from) && DateTime.TryParse(vm.DateTo, out to) && from > to)
            {
                this.ModelState.AddModelError("DateFrom", string.Format(Texts.Implementation_Index_ValidationDates, Texts.Operations508_Index_DateFrom, Texts.Operations508_Index_DateTo));
            }

            if (!this.ModelState.IsValid)
            {
                this.FillModelValues(vm);

                vm.ShowRes = false;

                return this.View(vm);
            }

            vm.ShowRes = true;

            SearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            int? programmeId = null;
            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeId"]))
            {
                programmeId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeId"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["dateFrom"]))
            {
                from = DateTime.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["dateFrom"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["dateTo"]))
            {
                to = DateTime.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["dateTo"]));
            }

            var firstRow = new ExportRow();

            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_BeneficiaryName, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_VesselIdentifiers, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_OperationName, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_OperationSummary, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_OperationStartDate, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_OperationEndDate, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_TotalEligibleExpenditure, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_FinancingEU, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_PostCode, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_Country, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_ProgrammePriority, true));
            firstRow.Cells.Add(new ExportCell(Texts.Operations508_Index_LastUpdate, true));

            var table = new ExportTable(Texts.Operations508_Index_OperationsList);
            table.Rows.Add(firstRow);

            var results = this.umisRepository.GetOperations508Report(from, to, programmeId).Results;

            foreach (var item in results)
            {
                var row = new ExportRow();

                row.Cells.Add(item.TransCompanyFullName.ToExportCell());
                row.Cells.Add(string.Join(", ", item.VesselIdentifiers).ToExportCell());
                row.Cells.Add(item.TransName.ToExportCell());
                row.Cells.Add(item.TransDescription.ToExportCell());
                row.Cells.Add(item.StartDate.ToExportCell());
                row.Cells.Add(item.CompletionDate.ToExportCell());
                row.Cells.Add(item.ContractedTotalAmount.ToExportCell());
                row.Cells.Add(item.ContractedEuAmount.ToExportCell());
                row.Cells.Add(item.PostCode.ToExportCell());
                row.Cells.Add(item.TransCountryName.ToExportCell());
                row.Cells.Add(item.TransProgrammePriorityName.ToExportCell());
                row.Cells.Add(item.ModifyDate.ToExportCell());

                table.Rows.Add(row);
            }

            var template = new ExportTemplate("508_2014");
            template.PageOrientation = PageOrientation.Landscape;
            template.Sheet.Name = "508_2014";
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
                { 11, 200 },
                { 12, 200 },
            };
            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private void FillModelValues(SearchVM vm)
        {
            if (vm == null)
            {
                vm = new SearchVM();
            }

            var ops = this.InfrastructureRepository.GetEfmdrProgrammes().OrderBy(e => e.PortalOrderNum).Select(e => new SelectListItem() { Value = e.MapNodeId.ToString(), Text = e.TransName });
            vm.Programs = ops;
        }
    }
}
