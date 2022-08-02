using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.Models.OperationalProgram;

namespace Eumis.Public.Web.Controllers
{
    public partial class ImplementationController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public ImplementationController(
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
                    "dateFrom",
                    "dateTo",
            })]
        public virtual ActionResult Index(
                string dateFrom = "",
                string dateTo = "")
        {
            this.ModelState.Clear();

            OpSearchVM vm = new OpSearchVM()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
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

            var programs = this.umisRepository.GetProgrammeBudgetDetailed(from, to);

            vm.OperationalPrograms = new OperationalProgramsVO(programs);

            return this.View(vm);
        }

        [HttpPost]
        public virtual ActionResult Index(OpSearchVM vm)
        {
            DateTime from;
            DateTime to;

            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            if (DateTime.TryParse(vm.DateFrom, out from) && DateTime.TryParse(vm.DateTo, out to) && from > to)
            {
                this.ModelState.AddModelError("DateFrom", string.Format(Texts.Implementation_Index_ValidationDates, Texts.Implementation_Index_DateFrom, Texts.Implementation_Index_DateTo));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(vm);
            }

            OpSearchVM.EncryptProperties(vm);

            return this.RedirectToAction(this.ActionNames.Index, vm);
        }

        public override ExportTemplate RenderTemplate()
        {
            DateTime? from = DateTime.MinValue;
            DateTime? to = DateTime.Now;

            if (!string.IsNullOrEmpty(this.Request.QueryString["dateFrom"]))
            {
                from = DateTime.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["dateFrom"]));
            }

            if (!string.IsNullOrEmpty(this.Request.QueryString["dateTo"]))
            {
                to = DateTime.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["dateTo"]));
            }

            var programs = this.umisRepository.GetProgrammeBudgetDetailed(from, to);

            var results = new OperationalProgramsVO(programs);

            var template = new ExportTemplate("implementation");
            template.Sheet.Name = "implementation";

            string headerDate = to.HasValue ? Helper.DateToBgFormat(to.Value) : Helper.DateToBgFormat(DateTime.Now);

            var table = new ExportTable(Texts.Implementation_Index_OpExecution);
            var firstRow = new ExportRow();

            for (int i = 0; i < 5; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Global_OperationalProgram;
            firstRow.Cells[0].RowSpan = 2;

            firstRow.Cells[1].Value = Texts.Global_BudgetProgram;
            firstRow.Cells[1].ColSpan = 3;

            firstRow.Cells[2].Value = Texts.Global_Contracted;
            firstRow.Cells[2].ColSpan = 2;

            firstRow.Cells[3].Value = Texts.Global_Payed;
            firstRow.Cells[3].ColSpan = 2;

            firstRow.Cells[4].Value = Texts.Implementation_Index_ReceivedTranches;
            firstRow.Cells[4].ColSpan = 2;

            table.Rows.Add(firstRow);

            var secondRow = new ExportRow();

            for (int i = 0; i < 9; i++)
            {
                secondRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            secondRow.Cells[0].Value = Texts.Global_Total;
            secondRow.Cells[1].Value = Texts.Global_FinancingEU;
            secondRow.Cells[2].Value = Texts.Global_FinancingNF;
            secondRow.Cells[3].Value = string.Format(Texts.Implementation_Index_TotalTo + " " + headerDate);
            secondRow.Cells[4].Value = Texts.Global_PercentExecution;
            secondRow.Cells[5].Value = string.Format(Texts.Implementation_Index_TotalTo + " " + headerDate);
            secondRow.Cells[6].Value = Texts.Global_PercentExecution;
            secondRow.Cells[7].Value = string.Format(Texts.Implementation_Index_TotalTo + " " + headerDate);
            secondRow.Cells[8].Value = Texts.Global_PercentExecution;

            table.Rows.Add(secondRow);

            foreach (var operationalProgramGroup in results.OperationalProgramGroups)
            {
                if (operationalProgramGroup.OperationalPrograms.Count > 0)
                {
                    foreach (var program in operationalProgramGroup.OperationalPrograms)
                    {
                        var row = new ExportRow();

                        row.Cells.Add(program.TransName.ToExportCell());
                        row.Cells.Add(program.BudgetTotal.ToExportCell());
                        row.Cells.Add(program.BudgetEU.ToExportCell());
                        row.Cells.Add(program.BudgetNational.ToExportCell());
                        row.Cells.Add(program.ContractTotal.ToExportCell());
                        row.Cells.Add(program.ContractPercent.ToExportCell());
                        row.Cells.Add(program.PaidBFP.ToExportCell());
                        row.Cells.Add(program.PaidPercent.ToExportCell());
                        row.Cells.Add(program.ReceivedTotal.ToExportCell());
                        row.Cells.Add(program.ReceivedPercent.ToExportCell());

                        table.Rows.Add(row);
                    }

                    var subtotalsRow = new ExportRow();

                    subtotalsRow.Cells.Add($"{Texts.Global_Total} {operationalProgramGroup.GroupName}:".ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.BudgetTotalSum.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.BudgetEUSum.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.BudgetNationalSum.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractTotalSum.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractSumPercent.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidBFPSum.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidSumPercent.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidECSum.ToExportHeaderCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidECSumPercent.ToExportHeaderCell());

                    table.Rows.Add(subtotalsRow);
                }
            }

            if (results.OtherOperationalPrograms.Count > 0)
            {
                foreach (var program in results.OtherOperationalPrograms)
                {
                    var row = new ExportRow();

                    row.Cells.Add(program.TransName.ToExportCell());
                    row.Cells.Add(program.BudgetTotal.ToExportCell());
                    row.Cells.Add(program.BudgetEU.ToExportCell());
                    row.Cells.Add(program.BudgetNational.ToExportCell());
                    row.Cells.Add(program.ContractTotal.ToExportCell());
                    row.Cells.Add(program.ContractPercent.ToExportCell());
                    row.Cells.Add(program.PaidBFP.ToExportCell());
                    row.Cells.Add(program.PaidPercent.ToExportCell());
                    row.Cells.Add(program.ReceivedTotal.ToExportCell());
                    row.Cells.Add(program.ReceivedPercent.ToExportCell());

                    table.Rows.Add(row);
                }
            }

            var totalRow = new ExportRow();

            totalRow.Cells.Add($"{Texts.Global_Total.ToUpper()}:".ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.BudgetTotalSum.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.BudgetEUSum.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.BudgetNationalSum.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.ContractTotalSum.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.ContractSumPercent.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.PaidBFPSum.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.PaidSumPercent.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.PaidECSum.ToExportHeaderCell());
            totalRow.Cells.Add(results.GrandTotals.PaidECSumPercent.ToExportHeaderCell());

            table.Rows.Add(totalRow);

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 300 },
                { 2, 150 },
                { 3, 150 },
                { 4, 150 },
                { 5, 150 },
                { 6, 150 },
                { 7, 150 },
                { 8, 150 },
                { 9, 200 },
                { 10, 200 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }
    }
}