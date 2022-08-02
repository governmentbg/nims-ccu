using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.Operations;
using PagedList;

namespace Eumis.Public.Web.Controllers
{
    public partial class OperationsController : BaseWithExportController
    {
        private readonly IUmisRepository umisRepository;

        public OperationsController(
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

            OperationsSearchVM vm = new OperationsSearchVM()
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

            this.FillModelValues(ref vm);

            if (vm.ShowRes)
            {
                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);
                int offset = (innerPage - 1) * Configuration.MaxItemsPerPage;

                var contracts = this.umisRepository.GetStatisticContracts(
                    startDateFrom: from,
                    completionDateTo: to,
                    programmeId: string.IsNullOrWhiteSpace(vm.ProgrammeId) ? (int?)null : int.Parse(vm.ProgrammeId),
                    offset: offset,
                    limit: Configuration.MaxItemsPerPage);

                vm.SearchResults = new StaticPagedList<StatisticContractVO>(contracts.Results, innerPage, Configuration.MaxItemsPerPage, contracts.Count);
            }

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(OperationsSearchVM vm)
        {
            DateTime from;
            DateTime to;

            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (DateTime.TryParse(vm.DateFrom, out from) && DateTime.TryParse(vm.DateTo, out to) && from > to)
            {
                this.ModelState.AddModelError("DateFrom", string.Format(Texts.Implementation_Index_ValidationDates, Texts.Operations_Index_DateFrom, Texts.Operations_Index_DateTo));
            }

            if (!this.ModelState.IsValid)
            {
                this.FillModelValues(ref vm);

                vm.ShowRes = false;

                return this.View(vm);
            }

            vm.ShowRes = true;

            OperationsSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            int? programmeId = null;
            DateTime? from = DateTime.MinValue;
            DateTime? to = DateTime.Now;

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

            var contracts = this.umisRepository.GetStatisticContracts(from, to, programmeId).Results.ToList();

            var template = new ExportTemplate("operations");
            template.Sheet.Name = "operations";

            var table = new ExportTable(Texts.Operations_Index_OperationsList);
            var firstRow = new ExportRow();

            for (int i = 0; i < 10; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Operations_Index_BeneficiaryName;
            firstRow.Cells[1].Value = Texts.Operations_Index_OperationName;
            firstRow.Cells[2].Value = Texts.Operations_Index_OperationSummary;
            firstRow.Cells[3].Value = Texts.Operations_Index_OperationStartDate;
            firstRow.Cells[4].Value = Texts.Operations_Index_OperationEndDate;
            firstRow.Cells[5].Value = Texts.Operations_Index_TotalEligibleExpenditure;
            firstRow.Cells[6].Value = Texts.Operations_Index_PercentFinancingEU;
            firstRow.Cells[7].Value = Texts.Operations_Index_Location;
            firstRow.Cells[8].Value = Texts.Operations_Index_InterventionCategoriesName;
            firstRow.Cells[9].Value = Texts.Operations_Index_LastUpdate;

            table.Rows.Add(firstRow);

            foreach (var contract in contracts)
            {
                var row = new ExportRow();

                row.Cells.Add(contract.TransCompanyFullName.ToExportCell());
                row.Cells.Add(contract.TransName.ToExportCell());
                row.Cells.Add(contract.TransDescription.ToExportCell());
                row.Cells.Add(contract.StartDate.ToExportCell());
                row.Cells.Add(contract.CompletionDate.ToExportCell());
                row.Cells.Add(contract.ContractedTotalAmount.ToExportCell());
                row.Cells.Add(contract.ContractedEuAmountPercentage.ToExportCell());
                row.Cells.Add(this.GetLocations(contract.TransNutsFullPathNames).ToExportCell());
                row.Cells.Add(this.GetInterventionCategories(contract.InterventionCategories).ToExportCell());
                row.Cells.Add(contract.ModifyDate.ToExportCell());

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

        private void FillModelValues(ref OperationsSearchVM vm)
        {
            if (vm == null)
            {
                vm = new OperationsSearchVM();
            }

            var ops = this.InfrastructureRepository.GetAllOps().OrderBy(e => e.PortalOrderNum).Select(e => new SelectListItem() { Value = e.MapNodeId.ToString(), Text = e.TransName });
            vm.Programs = ops;
        }

        private string GetLocations(IEnumerable<string> locations)
        {
            StringBuilder result = new StringBuilder();

            if (locations != null && locations.Any())
            {
                foreach (var location in locations)
                {
                    result.Append(location + ";" + Environment.NewLine);
                }
            }

            return result.ToString().Trim();
        }

        private string GetInterventionCategories(IEnumerable<InterventionCategory> interventionCategories)
        {
            StringBuilder result = new StringBuilder();

            if (interventionCategories != null && interventionCategories.Any())
            {
                foreach (var interventionCategory in interventionCategories)
                {
                    result.Append(interventionCategory.TransName + ";" + Environment.NewLine);
                }
            }

            return result.ToString().Trim();
        }
    }
}