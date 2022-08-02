using Eumis.Public.Common.Crypto;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Common.Json;
using Eumis.Public.Data.ProgrammeGroups.Repositories;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Resources;
using Eumis.Public.Web.InfrastructureClasses;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.InfrastructureClasses.Pies;
using Eumis.Public.Web.Models.Charts;
using Eumis.Public.Web.Models.Pies;
using Eumis.Public.Web.Models.ProgrammeGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eumis.Public.Web.Controllers
{
    public partial class ProgrammeGroupsController : BaseWithExportController
    {
        private IProgrammeGroupsRepository programmeGroupsRepository;

        public ProgrammeGroupsController(
            IMapsRepository mapsRepository,
            IInfrastructureRepository infrastructureRepository,
            IProgrammeGroupsRepository programmeGroupsRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.programmeGroupsRepository = programmeGroupsRepository;
        }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName =
            new string[]
            {
                "programmeGroupId",
            })]
        public virtual ActionResult Index(string programmeGroupId)
        {
            var model = this.InitializeModel(int.Parse(programmeGroupId));

            return this.View(model);
        }

        public virtual ActionResult GetBudgetPieData(string programmeGroupId)
        {
            PieModel pm = new PieModel();

            var budgetData = this.programmeGroupsRepository.GetProgrammeBudgetTotals(true, int.Parse(programmeGroupId))
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

        public virtual ActionResult GetFinanceSourcesPieData(string programmeGroupId)
        {
            PieModel pm = new PieModel();

            var financeSourcesData = this.programmeGroupsRepository.GetFinanceSourceTotals(true, int.Parse(programmeGroupId))
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

        public virtual ActionResult GetBudgetChartData(string programmeGroupId)
        {
            ChartModel chartModel = new ChartModel();

            var budgetData = this.programmeGroupsRepository.GetProgrammeBudgetDetailed(true, int.Parse(programmeGroupId))
                .Select(programme => new
                {
                    Name = programme.TransName,
                    ContractedNational = programme.ContractNational,
                    ContractedEu = programme.ContractEU,
                    PayedNational = programme.PaidNational,
                    PayedEu = programme.PaidEU,
                })
                .ToList();

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfEU, Data = budgetData.Select(c => c.ContractedEu), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfNF, Data = budgetData.Select(c => c.ContractedNational), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfEU, Data = budgetData.Select(c => c.PayedEu), Stack = "payed" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfNF, Data = budgetData.Select(c => c.PayedNational), Stack = "payed" });

            chartModel.Categories = budgetData.Select(c => c.Name);
            chartModel.Data = data;

            return new JsonChartResult(chartModel);
        }

        public virtual ActionResult GetProgrammesExecutionChartData(string programmeGroupId)
        {
            ChartModel chartModel = new ChartModel();

            var budgetData = this.programmeGroupsRepository.GetProgrammeBudgetDetailed(true, int.Parse(programmeGroupId))
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

        public virtual ActionResult GetProgrammeFinanceSourceAmountsChartData(string programmeGroupId)
        {
            ChartModel chartModel = new ChartModel();

            var budgetData = this.programmeGroupsRepository.GetFinanceSourceTotalsByProgramme(true, int.Parse(programmeGroupId))
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
            int? programmeGroupId = null;

            if (!string.IsNullOrEmpty(this.Request.QueryString["programmeGroupId"]))
            {
                programmeGroupId = int.Parse(ConfigurationBasedStringEncrypter.Decrypt(this.Request.QueryString["programmeGroupId"]));
            }

            var model = this.InitializeModel(programmeGroupId.Value);

            var template = new ExportTemplate("programmes");
            template.Sheet.Name = "programmes";

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

            if (model.ProgrammeBudgets.Count > 0)
            {
                foreach (var programmeBudget in model.ProgrammeBudgets)
                {
                    var row = new ExportRow();

                    row.Cells.Add(programmeBudget.TransName.ToExportCell());
                    row.Cells.Add(programmeBudget.BudgetTotal.ToExportCell());
                    row.Cells.Add(programmeBudget.BudgetEU.ToExportCell());
                    row.Cells.Add(programmeBudget.BudgetNational.ToExportCell());
                    row.Cells.Add(programmeBudget.ProjectsCount.ToExportCell());
                    row.Cells.Add(programmeBudget.ContractsCount.ToExportCell());
                    row.Cells.Add(programmeBudget.ContractTotal.ToExportCell());
                    row.Cells.Add(programmeBudget.ContractBFP.ToExportCell());
                    row.Cells.Add(programmeBudget.ContractPercent.ToExportCell());
                    row.Cells.Add(programmeBudget.ContractEU.ToExportCell());
                    row.Cells.Add(programmeBudget.PaidBFP.ToExportCell());
                    row.Cells.Add(programmeBudget.PaidPercent.ToExportCell());
                    row.Cells.Add(programmeBudget.PaidEU.ToExportCell());

                    table.Rows.Add(row);
                }
            }

            var lastRow = new ExportRow();

            lastRow.Cells.Add($"{Texts.Global_Total.ToUpper()}:".ToExportHeaderCell());
            lastRow.Cells.Add(model.GrandTotals.BudgetTotalSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.BudgetEUSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.BudgetNationalSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.ProjectsCountSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.ContractsCountSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.ContractTotalSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.ContractBFPSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.ContractSumPercent.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.ContractEUSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.PaidBFPSum.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.PaidSumPercent.ToExportCell());
            lastRow.Cells.Add(model.GrandTotals.PaidEUSum.ToExportCell());

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

        private ProgrammeGroupIndexModel InitializeModel(int? programmeGroupId = null)
        {
            var programmeBudgets = this.programmeGroupsRepository.GetProgrammeBudgetDetailed(true, programmeGroupId).ToList();

            var model = new ProgrammeGroupIndexModel();

            model.ProgrammeGroupName = this.programmeGroupsRepository.GetProgrammeGroup(programmeGroupId.Value).TransName;

            model.ProgrammeBudgets = programmeBudgets;

            model.GrandTotals = new OperationalProgramTotalsVO(programmeBudgets);

            model.BudgetPie = new PieRendererModel(
                Texts.ProgrammeGroups_Index_ProgramBudget,
                new UrlDef(MVC.ProgrammeGroups.Name, MVC.ProgrammeGroups.ActionNames.GetBudgetPieData, new { programmeGroupId = programmeGroupId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip);

            model.FinanceSourcePie = new PieRendererModel(
                Texts.ProgrammeGroups_Index_FiananceSourcesDistribution,
                new UrlDef(MVC.ProgrammeGroups.Name, MVC.ProgrammeGroups.ActionNames.GetFinanceSourcesPieData, new { programmeGroupId = programmeGroupId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip);

            model.BudgetChart = new ChartRendererModel(
                Texts.ProgrammeGroups_Index_ChartContractedPayed,
                string.Empty,
                new UrlDef(MVC.ProgrammeGroups.Name, MVC.ProgrammeGroups.ActionNames.GetBudgetChartData, new { programmeGroupId = programmeGroupId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            model.BudgetChart.HasStackLabels = true;

            model.ProgrammesExecutionChart = new ChartRendererModel(
                Texts.ProgrammeGroups_Index_ChartProgrammesExecution,
                string.Empty,
                new UrlDef(MVC.ProgrammeGroups.Name, MVC.ProgrammeGroups.ActionNames.GetProgrammesExecutionChartData, new { programmeGroupId = programmeGroupId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            model.ProgrammeFinanceSourceAmountsChart = new ChartRendererModel(
                Texts.ProgrammeGroups_Index_ChartProgrammeFinanceSourceBudget,
                string.Empty,
                new UrlDef(MVC.ProgrammeGroups.Name, MVC.ProgrammeGroups.ActionNames.GetProgrammeFinanceSourceAmountsChartData, new { programmeGroupId = programmeGroupId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            return model;
        }
    }
}
