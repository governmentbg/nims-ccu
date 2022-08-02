using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Common.Export;
using Eumis.Public.Common.Helpers;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Resources;
using Eumis.Public.Web.InfrastructureClasses;
using Eumis.Public.Web.InfrastructureClasses.Charts;
using Eumis.Public.Web.Models.Charts;
using Eumis.Public.Web.Models.OPProfile;

namespace Eumis.Public.Web.Controllers
{
    public partial class OPProfileController : BaseWithExportController
    {
        private readonly IUmisRepository umisRepository;

        public OPProfileController(
            IMapsRepository mapsRepository,
            IUmisRepository umisRepository,
            IInfrastructureRepository infrastructureRepository)
            : base(mapsRepository, infrastructureRepository)
        {
            this.umisRepository = umisRepository;
        }

        public virtual ActionResult Index()
        {
            OPProfileIndexModel model = this.GetOpProfileData(this.OpId);

            model.FundsChart = new ChartRendererModel(
                Texts.OPProfile_Index_BudgetOperationalProgram + ": <br/>" + this.GetOP.TransName,
                Texts.OPProfile_Index_ChartYTitle,
                new UrlDef(MVC.OPProfile.Name, MVC.OPProfile.ActionNames.GetFundsChartData, new { id = this.OpId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            model.ExecutionsChart = new ChartRendererModel(
                Texts.OPProfile_Index_Sum_FinancialExecution + ": <br/>" + this.GetOP.TransName,
                Texts.OPProfile_Index_ChartYTitle,
                new UrlDef(MVC.OPProfile.Name, MVC.OPProfile.ActionNames.GetExecutionsChartData, new { id = this.OpId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                false);

            model.BudgetChart = new ChartRendererModel(
                Texts.OPProfile_Index_ContractedPayedOperationalProgram + ": <br/>" + this.GetOP.TransName,
                string.Empty,
                new UrlDef(MVC.OPProfile.Name, MVC.OPProfile.ActionNames.GetBudgetChartData, new { id = this.OpId }),
                MVC.Infrastructure.Views.Charts.PointOnlyTooltip,
                true);

            return this.View(model);
        }

        public override ExportTemplate RenderTemplate()
        {
            OPProfileIndexModel model = this.GetOpProfileData(this.OpId);

            var template = new ExportTemplate("opProfileFinExec");
            template.Sheet.Name = "opProfileFinExec";

            var fondsTable = new ExportTable(Texts.OPProfile_Index_BudgetFonds);
            var headerRow = new ExportRow();

            headerRow.Cells.Add(new ExportCell { Value = Texts.OPProfile_Index_Period, IsHeader = true });

            if (model.ShowERDF)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_EuropeanRegionalDevelopmentFund, IsHeader = true });
            }

            if (model.ShowCF)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_CohesionFund, IsHeader = true });
            }

            if (model.ShowNF)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.OPProfile_Index_NF, IsHeader = true });
            }

            if (model.ShowESF)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_EuropeanSocialFund, IsHeader = true });
            }

            if (model.ShowYEI)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_YouthEmploymentInitiative, IsHeader = true });
            }

            if (model.ShowFEAD)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_FundForEuropeanAidToTheMostDeprived, IsHeader = true });
            }

            if (model.ShowEFMDR)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_EMFF, IsHeader = true });
            }

            if (model.ShowEZFRSR)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_EAFRD, IsHeader = true });
            }

            if (model.ShowFVS)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_ISF, IsHeader = true });
            }

            if (model.ShowFUMI)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_AMIF, IsHeader = true });
            }

            if (model.ShowOther)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_Other, IsHeader = true });
            }

            if (model.ShowEEAFM)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_EEAFM, IsHeader = true });
            }

            if (model.ShowNFM)
            {
                headerRow.Cells.Add(new ExportCell { Value = Texts.Enum_FinanceSource_NFM, IsHeader = true });
            }

            headerRow.Cells.Add(new ExportCell { Value = Texts.Global_Total, IsHeader = true });

            fondsTable.Rows.Add(headerRow);

            foreach (var fond in model.ItemsBySource)
            {
                var row = new ExportRow();

                row.Cells.Add(new ExportCell { Value = fond.Year.ToString() });
                if (model.ShowERDF)
                {
                    row.Cells.Add(fond.EuropeanRegionalDevelopmentFund.ToExportCell());
                }

                if (model.ShowCF)
                {
                    row.Cells.Add(fond.CohesionFund.ToExportCell());
                }

                if (model.ShowNF)
                {
                    row.Cells.Add(fond.BgAmount.ToExportCell());
                }

                if (model.ShowESF)
                {
                    row.Cells.Add(fond.EuropeanSocialFund.ToExportCell());
                }

                if (model.ShowYEI)
                {
                    row.Cells.Add(fond.YouthEmploymentInitiative.ToExportCell());
                }

                if (model.ShowFEAD)
                {
                    row.Cells.Add(fond.FundForEuropeanAidToTheMostDeprived.ToExportCell());
                }

                if (model.ShowEFMDR)
                {
                    row.Cells.Add(fond.EFMDR.ToExportCell());
                }

                if (model.ShowEZFRSR)
                {
                    row.Cells.Add(fond.EZFRSR.ToExportCell());
                }

                if (model.ShowFVS)
                {
                    row.Cells.Add(fond.FVS.ToExportCell());
                }

                if (model.ShowFUMI)
                {
                    row.Cells.Add(fond.FUMI.ToExportCell());
                }

                if (model.ShowOther)
                {
                    row.Cells.Add(fond.Other.ToExportCell());
                }

                if (model.ShowEEAFM)
                {
                    row.Cells.Add(fond.EEAFM.ToExportCell());
                }

                if (model.ShowNFM)
                {
                    row.Cells.Add(fond.NFM.ToExportCell());
                }

                row.Cells.Add(fond.YearTotal.ToExportCell());

                fondsTable.Rows.Add(row);
            }

            var totalRow = new ExportRow();

            totalRow.Cells.Add(new ExportCell { Value = Texts.Global_Total, IsBold = true, IsItalic = true });
            if (model.ShowERDF)
            {
                totalRow.Cells.Add(model.EuropeanRegionalDevelopmentFundTotal.ToExportHeaderCell());
            }

            if (model.ShowCF)
            {
                totalRow.Cells.Add(model.CohesionFundTotal.ToExportHeaderCell());
            }

            if (model.ShowNF)
            {
                totalRow.Cells.Add(model.BgAmountTotal.ToExportHeaderCell());
            }

            if (model.ShowESF)
            {
                totalRow.Cells.Add(model.EuropeanSocialFundTotal.ToExportHeaderCell());
            }

            if (model.ShowYEI)
            {
                totalRow.Cells.Add(model.YouthEmploymentInitiativeTotal.ToExportHeaderCell());
            }

            if (model.ShowFEAD)
            {
                totalRow.Cells.Add(model.FundForEuropeanAidToTheMostDeprivedTotal.ToExportHeaderCell());
            }

            if (model.ShowEFMDR)
            {
                totalRow.Cells.Add(model.EFMDRTotal.ToExportHeaderCell());
            }

            if (model.ShowEZFRSR)
            {
                totalRow.Cells.Add(model.EZFRSRTotal.ToExportHeaderCell());
            }

            if (model.ShowFVS)
            {
                totalRow.Cells.Add(model.FVSTotal.ToExportHeaderCell());
            }

            if (model.ShowFUMI)
            {
                totalRow.Cells.Add(model.FUMITotal.ToExportHeaderCell());
            }

            if (model.ShowOther)
            {
                totalRow.Cells.Add(model.OtherTotal.ToExportHeaderCell());
            }

            if (model.ShowEEAFM)
            {
                totalRow.Cells.Add(model.EEAFMTotal.ToExportHeaderCell());
            }

            if (model.ShowNFM)
            {
                totalRow.Cells.Add(model.NFMTotal.ToExportHeaderCell());
            }

            totalRow.Cells.Add(model.Total.ToExportHeaderCell());

            fondsTable.Rows.Add(totalRow);

            template.Sheet.Tables.Add(fondsTable);

            var operationalProgramName = string.Empty;

            if (this.GetOP != null)
            {
                operationalProgramName = this.GetOP.TransName;
            }

            var table = new ExportTable(string.Format(Texts.OPProfile_Index_Execution + " " + operationalProgramName));
            var firstRow = new ExportRow();

            for (int i = 0; i < 4; i++)
            {
                firstRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            firstRow.Cells[0].Value = Texts.OPProfile_Index_Year;
            firstRow.Cells[0].RowSpan = 2;

            firstRow.Cells[1].Value = Texts.Global_Budget;
            firstRow.Cells[1].ColSpan = 3;

            firstRow.Cells[2].Value = Texts.Global_Contracted;
            firstRow.Cells[2].ColSpan = 3;

            firstRow.Cells[3].Value = Texts.Global_Payed;
            firstRow.Cells[3].ColSpan = 3;

            table.Rows.Add(firstRow);

            var secondRow = new ExportRow();

            for (int i = 0; i < 9; i++)
            {
                secondRow.Cells.Add(new ExportCell { IsHeader = true });
            }

            secondRow.Cells[0].Value = Texts.Global_Total;
            secondRow.Cells[1].Value = Texts.Global_FinancingEU;
            secondRow.Cells[2].Value = Texts.Global_FinancingNF;
            secondRow.Cells[3].Value = Texts.Global_Total;
            secondRow.Cells[4].Value = Texts.Global_FinancingEU;
            secondRow.Cells[5].Value = Texts.Global_PercentExecution;
            secondRow.Cells[6].Value = Texts.Global_Total;
            secondRow.Cells[7].Value = Texts.Global_FinancingEU;
            secondRow.Cells[8].Value = Texts.Global_PercentExecution;

            table.Rows.Add(secondRow);

            foreach (var execution in model.ItemsWithContractedAndPayed)
            {
                var row = new ExportRow();

                row.Cells.Add(execution.Year.ToExportCell());
                row.Cells.Add(execution.Budget.ToExportCell());
                row.Cells.Add(execution.BudgetEuAmount.ToExportCell());
                row.Cells.Add(execution.BudgetBgAmount.ToExportCell());
                row.Cells.Add(execution.Contracted.ToExportCell());
                row.Cells.Add(execution.ContractedEuAmount.ToExportCell());
                row.Cells.Add(execution.ContractedPercentExec.ToExportCell());
                row.Cells.Add(execution.Payed.ToExportCell());
                row.Cells.Add(execution.PayedEuAmount.ToExportCell());
                row.Cells.Add(execution.PayedPercentExec.ToExportCell());

                table.Rows.Add(row);
            }

            var toalRow = new ExportRow();

            toalRow.Cells.Add(Texts.Global_Total.ToExportHeaderCell());
            toalRow.Cells.Add(model.BudgetTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.BudgetEuAmountTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.BudgetBgAmountTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.ContractedTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.ContractedEuAmountTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.ContractedPercentExecTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.PayedTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.PayedEuAmountTotal.ToExportHeaderCell());
            toalRow.Cells.Add(model.PayedPercentExecTotal.ToExportHeaderCell());

            table.Rows.Add(toalRow);

            template.Sheet.Tables.Add(table);

            template.Sheet.ExcelColumnWidths = new Dictionary<int, int>
            {
                { 1, 150 },
                { 2, 150 },
                { 3, 200 },
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
            template.Sheet.EndNotes.Add("* - " + Texts.OPProfile_Index_NoteBudget);
            template.Sheet.EndNotes.Add("** - " + Texts.OPProfile_Index_NoteContracted);
            template.Sheet.EndNotes.Add("*** - " + Texts.OPProfile_Index_NotePayed);
            template.Sheet.EndNotes.Add(Texts.Global_NoteBGN);

            return template;
        }

        public virtual ActionResult GetFundsChartData(int id)
        {
            ChartModel cm = new ChartModel();

            OPProfileIndexModel op = this.GetOpProfileData(id);

            List<ChartDataModel> data = new List<ChartDataModel>();

            if (op.ShowERDF)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EuropeanRegionalDevelopmentFund, Data = op.ItemsBySource.Select(f => f.EuropeanRegionalDevelopmentFund) });
            }

            if (op.ShowCF)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_CohesionFund, Data = op.ItemsBySource.Select(f => f.CohesionFund) });
            }

            if (op.ShowESF)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EuropeanSocialFund, Data = op.ItemsBySource.Select(f => f.EuropeanSocialFund) });
            }

            if (op.ShowYEI)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_YouthEmploymentInitiative, Data = op.ItemsBySource.Select(f => f.YouthEmploymentInitiative) });
            }

            if (op.ShowFEAD)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_FundForEuropeanAidToTheMostDeprived, Data = op.ItemsBySource.Select(f => f.FundForEuropeanAidToTheMostDeprived) });
            }

            if (op.ShowEFMDR)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EMFF, Data = op.ItemsBySource.Select(f => f.EFMDR) });
            }

            if (op.ShowEZFRSR)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EAFRD, Data = op.ItemsBySource.Select(f => f.EZFRSR) });
            }

            if (op.ShowFVS)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_ISF, Data = op.ItemsBySource.Select(f => f.FVS) });
            }

            if (op.ShowFUMI)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_AMIF, Data = op.ItemsBySource.Select(f => f.FUMI) });
            }

            if (op.ShowOther)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_Other, Data = op.ItemsBySource.Select(f => f.Other) });
            }

            if (op.ShowEEAFM)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_EEAFM, Data = op.ItemsBySource.Select(f => f.EEAFM) });
            }

            if (op.ShowNFM)
            {
                data.Add(new ChartDataModel() { Name = Texts.Enum_FinanceSource_NFM, Data = op.ItemsBySource.Select(f => f.NFM) });
            }

            if (op.ShowNF)
            {
                data.Add(new ChartDataModel() { Name = Texts.OPProfile_Index_NF, Data = op.ItemsBySource.Select(f => f.BgAmount) });
            }

            cm.Categories = op.ItemsBySource.Select(f => f.Year.ToString());
            cm.Data = data;

            return new JsonChartResult(cm);
        }

        public virtual ActionResult GetExecutionsChartData(int id)
        {
            ChartModel cm = new ChartModel();

            OPProfileIndexModel opProfile = this.GetOpProfileData(id);

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Global_Budget, Data = opProfile.ItemsWithContractedAndPayed.Select(f => f.Budget) });
            data.Add(new ChartDataModel() { Name = Texts.Global_Contracted, Data = opProfile.ItemsWithContractedAndPayed.Select(f => f.Contracted) });
            data.Add(new ChartDataModel() { Name = Texts.Global_Payed, Data = opProfile.ItemsWithContractedAndPayed.Select(f => f.Payed) });

            cm.Categories = opProfile.ItemsWithContractedAndPayed.Select(f => f.Year.ToString());
            cm.Data = data;

            return new JsonChartResult(cm);
        }

        public virtual ActionResult GetBudgetChartData(int id)
        {
            ChartModel cm = new ChartModel();

            var budgetData = this.GetOpProfileData(id).ItemsWithContractedAndPayed
                .Select(axis => new
                {
                    Name = axis.Year,
                    ContractedNational = axis.ContractedBgAmount,
                    ContractedEu = axis.ContractedEuAmount,
                    PayedNational = axis.PayedBgAmount,
                    PayedEU = axis.PayedEuAmount,
                }).ToList();

            List<ChartDataModel> data = new List<ChartDataModel>();
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfEU, Data = budgetData.Select(c => c.ContractedEu), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_ContractedFinancingfNF, Data = budgetData.Select(c => c.ContractedNational), Stack = "contracted" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfEU, Data = budgetData.Select(c => c.PayedEU), Stack = "payed" });
            data.Add(new ChartDataModel() { Name = Texts.Global_PayedFinancingfNF, Data = budgetData.Select(c => c.PayedNational), Stack = "payed" });

            cm.Categories = budgetData.Select(c => c.Name);
            cm.Data = data;

            return new JsonChartResult(cm);
        }

        private OPProfileIndexModel GetOpProfileData(int id)
        {
            var programBudgetBySource = this.umisRepository.GetProgrammeBudgetBySource(id);

            var model = new OPProfileIndexModel();

            model.Sources = programBudgetBySource.Sources;

            model.ItemsBySource = programBudgetBySource.Items
                .Select(f => new ProgrammeBudgetBySource(f.Year, f.EuropeanRegionalDevelopmentFund, f.EuropeanSocialFund, f.CohesionFund, f.BgAmount, f.YouthEmploymentInitiative, f.FundForEuropeanAidToTheMostDeprived, f.EFMDR, f.EZFRSR, f.FVS, f.FUMI, f.Other, f.EEAFM, f.NFM))
                .ToList();
            model.ItemsBySource.Sort(new Comparison<ProgrammeBudgetBySource>((e, e2) => e.Year.CompareTo(e2.Year)));

            model.EuropeanRegionalDevelopmentFundTotal = model.ItemsBySource.Sum(e => e.EuropeanRegionalDevelopmentFund);
            model.EuropeanSocialFundTotal = model.ItemsBySource.Sum(e => e.EuropeanSocialFund);
            model.CohesionFundTotal = model.ItemsBySource.Sum(e => e.CohesionFund);
            model.BgAmountTotal = model.ItemsBySource.Sum(e => e.BgAmount);
            model.YouthEmploymentInitiativeTotal = model.ItemsBySource.Sum(e => e.YouthEmploymentInitiative);
            model.FundForEuropeanAidToTheMostDeprivedTotal = model.ItemsBySource.Sum(e => e.FundForEuropeanAidToTheMostDeprived);
            model.EFMDRTotal = model.ItemsBySource.Sum(e => e.EFMDR);
            model.EZFRSRTotal = model.ItemsBySource.Sum(e => e.EZFRSR);
            model.FVSTotal = model.ItemsBySource.Sum(e => e.FVS);
            model.FUMITotal = model.ItemsBySource.Sum(e => e.FUMI);
            model.OtherTotal = model.ItemsBySource.Sum(e => e.Other);
            model.EEAFMTotal = model.ItemsBySource.Sum(e => e.EEAFM);
            model.NFMTotal = model.ItemsBySource.Sum(e => e.NFM);

            var programmeBudgetWithContractedAndPayed = this.umisRepository.GetProgrammeBudgetWithContractedAndPayed(id);

            model.ItemsWithContractedAndPayed = programmeBudgetWithContractedAndPayed.Items.Select(e => new ProgrammeBudgetWithContractedAndPayed()
            {
                Year = e.Year,
                Budget = e.Budget,
                BudgetEuAmount = e.BudgetEuAmount,
                BudgetBgAmount = e.BudgetBgAmount,
                Contracted = e.Contracted,
                ContractedBgAmount = e.ContractedBgAmount,
                ContractedEuAmount = e.ContractedEuAmount,
                Payed = e.Payed,
                PayedBgAmount = e.PayedBgAmount,
                PayedEuAmount = e.PayedEuAmount,
            }).ToList();

            model.ItemsWithContractedAndPayed.Sort(new Comparison<ProgrammeBudgetWithContractedAndPayed>((e, e2) => e.Year.CompareTo(e2.Year)));

            return model;
        }
    }
}