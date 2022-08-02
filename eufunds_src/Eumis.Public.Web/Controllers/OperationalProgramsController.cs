using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Data.ProgrammeGroups.Repositories;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammeGroups;
using Eumis.Public.Resources;
using Eumis.Public.Web.InfrastructureClasses;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;
using Eumis.Public.Web.Models.Charts;
using Eumis.Public.Web.Models.OperationalProgram;
using Eumis.Public.Web.Models.Pies;

namespace Eumis.Public.Web.Controllers
{
    public partial class OperationalProgramsController : BaseWithExportController
    {
        private readonly IProgrammeGroupsRepository programmeGroupsRepository;

        public OperationalProgramsController(
            IMapsRepository mapsRepository,
            IProgrammeGroupsRepository programmeGroupsRepository,
            IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.programmeGroupsRepository = programmeGroupsRepository;
        }

        public virtual ActionResult Index(bool getProgrammeGroups)
        {
            var model = this.InitializeModel(getProgrammeGroups);

            return this.View(model);
        }

        public virtual ActionResult GetBudgetPieData(bool getProgrammeGroups)
        {
            PieModel pm = new PieModel();

            var budgetData = this.programmeGroupsRepository.GetProgrammeBudgetTotals(getProgrammeGroups)
                .Select(programme => new
                {
                    Name = programme.TransName,
                    Y = programme.TotalAmount,
                })
                .ToList();

            List<PieDataModel> data = new List<PieDataModel>();

            var total = budgetData.Sum(e => e.Y);
            total = total != 0 ? total : 1;

            foreach (var budget in budgetData)
            {
                data.Add(new PieDataModel() { Name = budget.Name, Value = DataUtils.DecimalToStringDecimalPointSpace(budget.Y), Y = Math.Round(budget.Y / total * 100, 2) });
            }

            pm.Name = Texts.ProgrammeGroups_Index_ProgramBudget;
            pm.ColorByPoint = true;
            pm.Data = data;

            return new JsonPieResult(pm);
        }

        public virtual ActionResult GetFinanceSourcesPieData(bool getProgrammeGroups)
        {
            PieModel pm = new PieModel();

            var financeSourcesData = this.programmeGroupsRepository.GetFinanceSourceTotals(getProgrammeGroups)
                .Select(financeSource => new
                {
                    Name = financeSource.FinanceSource.GetEnumDescription(),
                    Y = financeSource.TotalAmount,
                })
                .ToList();

            List<PieDataModel> data = new List<PieDataModel>();

            var total = financeSourcesData.Sum(e => e.Y);
            total = total != 0 ? total : 1;

            foreach (var financeSource in financeSourcesData)
            {
                data.Add(new PieDataModel() { Name = financeSource.Name, Value = DataUtils.DecimalToStringDecimalPointSpace(financeSource.Y), Y = Math.Round(financeSource.Y / total * 100, 2) });
            }

            pm.Name = Texts.ProgrammeGroups_Index_FiananceSourcesDistribution;
            pm.ColorByPoint = true;
            pm.Data = data;

            return new JsonPieResult(pm);
        }

        public virtual ActionResult GetBudgetChartData(bool getProgrammeGroups)
        {
            ChartModel chartModel = new ChartModel();

            var budgetData = this.programmeGroupsRepository.GetProgrammeBudgetDetailed(getProgrammeGroups)
                .Select(programme => new
                {
                    Name = programme.TransName,
                    ContractedNational = programme.ContractNational,
                    ContractedEu = programme.ContractEU,
                    PayedNational = programme.PaidNational,
                    PayedEu = programme.PaidEU,
                    TotalContracted = programme.ContractNational + programme.ContractEU,
                    TotalPayed = programme.PaidNational + programme.PaidEU,
                })
                .ToList();

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfNF, Data = budgetData.Select(c => c.ContractedNational), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfEU, Data = budgetData.Select(c => c.ContractedEu), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingTotal, Data = budgetData.Select(c => c.TotalContracted), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfNF, Data = budgetData.Select(c => c.PayedNational), Stack = "payed" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfEU, Data = budgetData.Select(c => c.PayedEu), Stack = "payed" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingTotal, Data = budgetData.Select(c => c.TotalPayed), Stack = "payed" });

            chartModel.Categories = budgetData.Select(c => c.Name);
            chartModel.Data = data;

            return new JsonChartResult(chartModel);
        }

        public virtual ActionResult GetProgrammesExecutionChartData(bool getProgrammeGroups)
        {
            ChartModel chartModel = new ChartModel();

            var budgetData = this.programmeGroupsRepository.GetProgrammeBudgetDetailed(getProgrammeGroups)
                .Select(programme => new
                {
                    Name = programme.TransName,
                    ContractedTotal = (programme.ContractBFP / ((programme.ContractBFP + programme.PaidBFP) == 0 ? 1 : (programme.ContractBFP + programme.PaidBFP))) * 100,
                    PayedTotal = (programme.PaidBFP / ((programme.ContractBFP + programme.PaidBFP) == 0 ? 1 : (programme.ContractBFP + programme.PaidBFP))) * 100,
                })
                .ToList();

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Global_Contracted, Data = budgetData.Select(c => Math.Round(c.ContractedTotal, 2)), Stack = "totals" });
            data.Add(new ChartDataModel() { Name = Texts.Global_Payed, Data = budgetData.Select(c => Math.Round(c.PayedTotal, 2)), Stack = "totals" });

            chartModel.Categories = budgetData.Select(c => c.Name);
            chartModel.Data = data;

            return new JsonChartResult(chartModel);
        }

        public virtual ActionResult GetProgrammeFinanceSourceAmountsChartData(bool getProgrammeGroups)
        {
            ChartModel chartModel = new ChartModel();

            var budgetData = this.programmeGroupsRepository.GetFinanceSourceTotalsByProgramme(getProgrammeGroups)
                .Select(programme => new
                {
                    Name = programme.TransName,
                    ESFAmount = programme.ESFAmount,
                    ERDFAmount = programme.ERDFAmount,
                    CFAmount = programme.CFAmount,
                    YEIAmount = programme.YEIAmount,
                    FEAMDAmount = programme.FEAMDAmount,
                    EFMDRAmount = programme.EFMDRAmount,
                    EZFRSRAmount = programme.EZFRSRAmount,
                    FVSAmount = programme.FVSAmount,
                    FUMIAmount = programme.FUMIAmount,
                    OtherAmount = programme.OtherAmount,
                    EEAFMAmount = programme.EEAFMAmount,
                    NFMAmount = programme.NFMAmount,
                })
                .ToList();

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EuropeanSocialFund, Data = budgetData.Select(c => c.ESFAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EuropeanRegionalDevelopmentFund, Data = budgetData.Select(c => c.ERDFAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_CohesionFund, Data = budgetData.Select(c => c.CFAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_YouthEmploymentInitiative, Data = budgetData.Select(c => c.YEIAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_FundForEuropeanAidToTheMostDeprived, Data = budgetData.Select(c => c.FEAMDAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EFMDR, Data = budgetData.Select(c => c.EFMDRAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EZFRSR, Data = budgetData.Select(c => c.EZFRSRAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_FVS, Data = budgetData.Select(c => c.FVSAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_FUMI, Data = budgetData.Select(c => c.FUMIAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_Other, Data = budgetData.Select(c => c.OtherAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EEAFM, Data = budgetData.Select(c => c.EEAFMAmount ?? 0), Stack = "financeSources" });
            data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_NFM, Data = budgetData.Select(c => c.NFMAmount ?? 0), Stack = "financeSources" });

            chartModel.Categories = budgetData.Select(c => c.Name);
            chartModel.Data = data;

            return new JsonChartResult(chartModel);
        }

        public override ExportTemplate RenderTemplate()
        {
            bool getProgrammeGroups = true;

            if (!string.IsNullOrEmpty(this.Request.QueryString["getProgrammeGroups"]))
            {
                _ = bool.TryParse(this.Request.QueryString["getProgrammeGroups"], out getProgrammeGroups);
            }

            var model = this.InitializeModel(getProgrammeGroups);

            var template = new ExportTemplate("operationalPrograms");
            template.Sheet.Name = "operationalPrograms";

            var table = new ExportTable(Texts.OpeationalPrograms_Index_Execution);
            var firstRow = new ExportRow();

            for (int i = 0; i < 5; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.Global_OperationalProgram;
            firstRow.Cells[0].RowSpan = 2;

            firstRow.Cells[1].Value = Texts.Global_BudgetProgram;
            firstRow.Cells[1].ColSpan = 3;

            firstRow.Cells[2].Value = Texts.OpeationalPrograms_Index_Projects;
            firstRow.Cells[2].ColSpan = 2;

            firstRow.Cells[3].Value = Texts.Global_Contracted + " **";
            firstRow.Cells[3].ColSpan = 4;

            firstRow.Cells[4].Value = Texts.Global_Payed;
            firstRow.Cells[4].ColSpan = 3;

            table.Rows.Add(firstRow);

            var secondRow = new ExportRow();

            for (int i = 0; i < 12; i++)
            {
                secondRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            secondRow.Cells[0].Value = Texts.Global_Total;
            secondRow.Cells[1].Value = Texts.Global_FinancingEU;
            secondRow.Cells[2].Value = Texts.Global_FinancingNF;
            secondRow.Cells[3].Value = Texts.OpeationalPrograms_Index_ProjectsCount;
            secondRow.Cells[4].Value = Texts.OpeationalPrograms_Index_ContractsCount;
            secondRow.Cells[5].Value = Texts.Global_Total;
            secondRow.Cells[6].Value = Texts.OpeationalPrograms_Index_BFP;
            secondRow.Cells[7].Value = "% *";
            secondRow.Cells[8].Value = Texts.Global_FinancingEU;
            secondRow.Cells[9].Value = Texts.OpeationalPrograms_Index_BFP;
            secondRow.Cells[10].Value = "% *";
            secondRow.Cells[11].Value = Texts.Global_FinancingEU;

            table.Rows.Add(secondRow);

            foreach (var operationalProgramGroup in model.OperationalPrograms.ProgrammeGroups)
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
                        row.Cells.Add(program.ProjectsCount.ToExportCell());
                        row.Cells.Add(program.ContractsCount.ToExportCell());
                        row.Cells.Add(program.ContractTotal.ToExportCell());
                        row.Cells.Add(program.ContractBFP.ToExportCell());
                        row.Cells.Add(program.ContractPercent.ToExportCell());
                        row.Cells.Add(program.ContractEU.ToExportCell());
                        row.Cells.Add(program.PaidBFP.ToExportCell());
                        row.Cells.Add(program.PaidPercent.ToExportCell());
                        row.Cells.Add(program.PaidEU.ToExportCell());

                        table.Rows.Add(row);
                    }

                    var subtotalsRow = new ExportRow();

                    subtotalsRow.Cells.Add($"{Texts.Global_Total} {operationalProgramGroup.GroupName}:".ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.BudgetTotalSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.BudgetEUSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.BudgetNationalSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ProjectsCountSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractsCountSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractTotalSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractBFPSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractSumPercent.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.ContractEUSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidBFPSum.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidSumPercent.ToExportCell());
                    subtotalsRow.Cells.Add(operationalProgramGroup.Totals.PaidEUSum.ToExportCell());

                    table.Rows.Add(subtotalsRow);
                }
            }

            if (model.OperationalPrograms.OtherProgrammes.Count > 0)
            {
                foreach (var programme in model.OperationalPrograms.OtherProgrammes)
                {
                    var row = new ExportRow();

                    row.Cells.Add(programme.TransName.ToExportCell());
                    row.Cells.Add(programme.BudgetTotal.ToExportCell());
                    row.Cells.Add(programme.BudgetEU.ToExportCell());
                    row.Cells.Add(programme.BudgetNational.ToExportCell());
                    row.Cells.Add(programme.ProjectsCount.ToExportCell());
                    row.Cells.Add(programme.ContractsCount.ToExportCell());
                    row.Cells.Add(programme.ContractTotal.ToExportCell());
                    row.Cells.Add(programme.ContractBFP.ToExportCell());
                    row.Cells.Add(programme.ContractPercent.ToExportCell());
                    row.Cells.Add(programme.ContractEU.ToExportCell());
                    row.Cells.Add(programme.PaidBFP.ToExportCell());
                    row.Cells.Add(programme.PaidPercent.ToExportCell());
                    row.Cells.Add(programme.PaidEU.ToExportCell());

                    table.Rows.Add(row);
                }
            }

            var lastRow = new ExportRow();

            lastRow.Cells.Add($"{Texts.Global_Total.ToUpper()}:".ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.BudgetTotalSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.BudgetEUSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.BudgetNationalSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.ProjectsCountSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.ContractsCountSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.ContractTotalSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.ContractBFPSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.ContractSumPercent.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.ContractEUSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.PaidBFPSum.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.PaidSumPercent.ToExportCell());
            lastRow.Cells.Add(model.OperationalPrograms.GrandTotals.PaidEUSum.ToExportCell());

            table.Rows.Add(lastRow);

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 200 },
                { 2, 100 },
                { 3, 150 },
                { 4, 200 },
                { 5, 150 },
                { 6, 150 },
                { 7, 100 },
                { 8, 100 },
                { 9, 50 },
                { 10, 150 },
                { 11, 150 },
                { 12, 100 },
                { 13, 150 },
            };

            template.Sheet.EndNotes.Add(Texts.Global_Note + ":");
            template.Sheet.EndNotes.Add("* - " + Texts.OpeationalPrograms_Index_ProgramBudgetPercent);
            template.Sheet.EndNotes.Add("** - " + Texts.OperationalPrograms_Index_NoteContracted);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        private OpIndexModel InitializeModel(bool getProgrammeGroups)
        {
            var programmeBudgets = this.programmeGroupsRepository.GetProgrammeBudgetDetailed(getProgrammeGroups).ToList();

            IList<ProgrammeGroup> programmeGroups = new List<ProgrammeGroup>();

            if (getProgrammeGroups)
            {
                programmeGroups = this.programmeGroupsRepository.GetAllProgrammeGroups().ToList();
            }

            var model = new OpIndexModel();

            model.OperationalPrograms = new ProgrammeGroupsVO(programmeBudgets, programmeGroups);

            model.BudgetPie = new PieRendererModel(
                Texts.ProgrammeGroups_Index_ProgramBudget,
                new UrlDef(MVC.OperationalPrograms.Name, MVC.OperationalPrograms.ActionNames.GetBudgetPieData, new { getProgrammeGroups = getProgrammeGroups }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip);

            model.FinanceSourcePie = new PieRendererModel(
                Texts.ProgrammeGroups_Index_FiananceSourcesDistribution,
                new UrlDef(MVC.OperationalPrograms.Name, MVC.OperationalPrograms.ActionNames.GetFinanceSourcesPieData, new { getProgrammeGroups = getProgrammeGroups }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip);

            model.BudgetChart = new ChartRendererModel(
                Texts.ProgrammeGroups_Index_ChartContractedPayed,
                string.Empty,
                new UrlDef(MVC.OperationalPrograms.Name, MVC.OperationalPrograms.ActionNames.GetBudgetChartData, new { getProgrammeGroups = getProgrammeGroups }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            model.ProgrammesExecutionChart = new ChartRendererModel(
                Texts.ProgrammeGroups_Index_ChartProgrammesExecution,
                string.Empty,
                new UrlDef(MVC.OperationalPrograms.Name, MVC.OperationalPrograms.ActionNames.GetProgrammesExecutionChartData, new { getProgrammeGroups = getProgrammeGroups }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            model.ProgrammeFinanceSourceAmountsChart = new ChartRendererModel(
                Texts.ProgrammeGroups_Index_ChartProgrammeFinanceSourceBudget,
                string.Empty,
                new UrlDef(MVC.OperationalPrograms.Name, MVC.OperationalPrograms.ActionNames.GetProgrammeFinanceSourceAmountsChartData, new { getProgrammeGroups = getProgrammeGroups }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            return model;
        }
    }
}