using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.InfrastructureClasses;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;
using Eumis.Public.Web.Models.Charts;
using Eumis.Public.Web.Models.Pies;
using Eumis.Public.Web.Models.PriorityLines;

namespace Eumis.Public.Web.Controllers
{
    public partial class PriorityLinesController : BaseWithExportController
    {
        private IUmisRepository umisRepository;

        public PriorityLinesController(
            IMapsRepository mapsRepository,
            IUmisRepository umisRepository,
            IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        public virtual ActionResult Index()
        {
            var model = this.InitializeModel();

            return this.View(model);
        }

        public override ExportTemplate RenderTemplate()
        {
            var model = this.InitializeModel();

            var template = new ExportTemplate("priorityAxes");
            template.Sheet.Name = "priorityAxes";

            var table = new ExportTable(Texts.PriorityLines_Index_FinancingPriorityAxes);
            var firstRow = new ExportRow();

            for (int i = 0; i < 4; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.PriorityLines_Index_PriorityAxis;
            firstRow.Cells[0].RowSpan = 2;

            firstRow.Cells[1].Value = Texts.Global_Budget;
            firstRow.Cells[1].ColSpan = 3;

            firstRow.Cells[2].Value = Texts.Global_Contracted;
            firstRow.Cells[2].ColSpan = 5;

            firstRow.Cells[3].Value = Texts.Global_Payed;
            firstRow.Cells[3].ColSpan = 4;

            table.Rows.Add(firstRow);

            var secondRow = new ExportRow();

            for (int i = 0; i < 12; i++)
            {
                secondRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            secondRow.Cells[0].Value = Texts.Global_Total;
            secondRow.Cells[1].Value = Texts.Global_FinancingEU;
            secondRow.Cells[2].Value = Texts.Global_FinancingNF;
            secondRow.Cells[3].Value = Texts.Global_Total;
            secondRow.Cells[4].Value = Texts.Global_FinancingEU;
            secondRow.Cells[5].Value = Texts.Global_FinancingNF;
            secondRow.Cells[6].Value = Texts.Global_FinancingSelf;
            secondRow.Cells[7].Value = Texts.Global_PercentExecution;
            secondRow.Cells[8].Value = Texts.Global_Total;
            secondRow.Cells[9].Value = Texts.Global_FinancingEU;
            secondRow.Cells[10].Value = Texts.Global_FinancingNF;
            secondRow.Cells[11].Value = Texts.Global_PercentExecution;

            table.Rows.Add(secondRow);

            foreach (var axis in model.PriorityAxises)
            {
                var row = new ExportRow();

                row.Cells.Add(axis.TransProgrammePriorityName.ToExportCell());
                row.Cells.Add(axis.TotalAmount.ToExportCell());
                row.Cells.Add(axis.EuAmount.ToExportCell());
                row.Cells.Add(axis.BgAmount.ToExportCell());
                row.Cells.Add(axis.ContractedTotalSum.ToExportCell());
                row.Cells.Add(axis.ContractedEuSum.ToExportCell());
                row.Cells.Add(axis.ContractedNationalSum.ToExportCell());
                row.Cells.Add(axis.ContractedSelfSum.ToExportCell());
                row.Cells.Add(axis.ContractedPercentExec.ToExportCell());
                row.Cells.Add(axis.PayedTotalSum.ToExportCell());
                row.Cells.Add(axis.PayedEuSum.ToExportCell());
                row.Cells.Add(axis.PayedNationalSum.ToExportCell());
                row.Cells.Add(axis.PayedPercentExec.ToExportCell());

                table.Rows.Add(row);
            }

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 700 },
                { 2, 150 },
                { 3, 150 },
                { 4, 150 },
                { 5, 150 },
                { 6, 150 },
                { 7, 150 },
                { 8, 150 },
                { 9, 150 },
                { 10, 150 },
                { 11, 150 },
                { 12, 150 },
                { 13, 150 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add(Texts.PriorityLines_Index_NoteArrow);
            template.Sheet.EndNotes.Add(Texts.Global_NoteUnderlined);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        public virtual ActionResult GetBudgetPieData()
        {
            PieModel pm = new PieModel();

            var budgetData = this.InitializeModel().PriorityAxises
                .Select(axis => new
                {
                    Name = axis.TransProgrammePriorityName,
                    Y = axis.TotalAmount,
                }).ToList();

            List<PieDataModel> data = new List<PieDataModel>();

            var total = budgetData.Sum(e => e.Y);

            foreach (var budget in budgetData)
            {
                var pieDataModel = new PieDataModel()
                {
                    Name = budget.Name,
                    Value = DataUtils.DecimalToStringDecimalPointSpace(budget.Y),
                };

                if (total != 0)
                {
                    pieDataModel.Y = Math.Round(budget.Y / total * 100, 2);
                }

                data.Add(pieDataModel);
            }

            pm.Name = Texts.PriorityLines_Index_Budget;
            pm.ColorByPoint = true;
            pm.Data = data;

            return new JsonPieResult(pm);
        }

        public virtual ActionResult GetBudgetChartData()
        {
            ChartModel cm = new ChartModel();

            var budgetData = this.InitializeModel().PriorityAxises
                .Select(axis => new
                {
                    Name = axis.TransProgrammePriorityName,
                    ContractedNational = axis.ContractedNationalSum,
                    ContractedEu = axis.ContractedEuSum,
                    PayedNational = axis.PayedNationalSum,
                    PayedEU = axis.PayedEuSum,
                }).ToList();

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfNF, Data = budgetData.Select(c => c.ContractedNational), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfEU, Data = budgetData.Select(c => c.ContractedEu), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfNF, Data = budgetData.Select(c => c.PayedNational), Stack = "payed" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfEU, Data = budgetData.Select(c => c.PayedEU), Stack = "payed" });

            cm.Categories = budgetData.Select(c => c.Name);
            cm.Data = data;

            return new JsonChartResult(cm);
        }

        private PriorityLinesIndexModel InitializeModel()
        {
            var programmePriorities = this.umisRepository.GetPPFundsWithProcedureFunds(this.OpId);

            var model = new PriorityLinesIndexModel()
            {
                PriorityAxises = programmePriorities.Select(axis => new PriorityAxisModel()
                {
                    ProgrammePriorityId = axis.ProgrammePriorityId,
                    ProgrammePriorityName = axis.ProgrammePriorityName,
                    ProgrammePriorityNameAlt = axis.ProgrammePriorityNameAlt,
                    TotalAmount = axis.TotalAmount,
                    EuAmount = axis.EuAmount,
                    BgAmount = axis.BgAmount,
                    Procedures = axis.Procedures.Select(procedure => new ProcedureModel()
                    {
                        ProcedureId = procedure.ProcedureId,
                        ProcedureName = procedure.ProcedureName,
                        ProcedureNameAlt = procedure.ProcedureNameAlt,
                        ProcedureCode = procedure.ProcedureCode,
                        ContractedTotalAmount = procedure.ContractedTotalAmount,
                        ContractedBgAmount = procedure.ContractedBgAmount,
                        ContractedEuAmount = procedure.ContractedEuAmount,
                        ContractedSelfAmount = procedure.ContractedSelfAmount,
                        PayedTotalAmount = procedure.PayedTotalAmount,
                        PayedBgAmount = procedure.PayedBgAmount,
                        PayedEuAmount = procedure.PayedEuAmount,
                        BudgetTotalAmount = procedure.BfpTotalmount,
                    }),
                })
                .ToList(),
            };

            var procedureIds = new List<int>();

            foreach (var programmePriority in programmePriorities)
            {
                procedureIds.AddRange(programmePriority.Procedures.Select(p => p.ProcedureId));
            }

            var totalContractsCount = this.umisRepository.GetProgrammePriorityTotalContractsCount(procedureIds);

            var totalCompaniesCount = this.umisRepository.GetProgrammePriorityTotalCompaniesCount(procedureIds);

            model.SummarizedData = new ProgrammePrioritiesSummarizedDataVO(programmePriorities, totalContractsCount, totalCompaniesCount);

            model.BudgetPie = new PieRendererModel(
                Texts.PriorityLines_Index_Budget,
                new UrlDef(MVC.PriorityLines.Name, MVC.PriorityLines.ActionNames.GetBudgetPieData),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip);

            model.BudgetChart = new ChartRendererModel(
                Texts.PriorityLines_Index_ContractedPayed,
                string.Empty,
                new UrlDef(MVC.PriorityLines.Name, MVC.PriorityLines.ActionNames.GetBudgetChartData),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            return model;
        }
    }
}