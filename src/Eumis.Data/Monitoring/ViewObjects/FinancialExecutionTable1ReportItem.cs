using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class FinancialExecutionTable1ReportItem
    {
        public FinancialExecutionTable1ReportItem(
            string programme,
            string programmePriority,
            decimal budgetBfpAmount,
            decimal contractedTotalAmount,
            decimal contractedBfpAmount,
            decimal reportedTotalAmount,
            int contractsCount)
        {
            this.Programme = programme;
            this.ProgrammePriority = programmePriority;
            this.RegionCategory = RegionCategory.LessDevelopedRegions;
            this.BudgetBfpAmount = budgetBfpAmount;
            this.ContractedTotalAmount = contractedTotalAmount;
            this.ContractedPercent = Math.Round(budgetBfpAmount == 0 ? 0 : contractedTotalAmount * 100 / budgetBfpAmount, 2);
            this.ContractedBfpAmount = contractedBfpAmount;
            this.ReportedTotalAmount = reportedTotalAmount;
            this.ReportedPercent = Math.Round(budgetBfpAmount == 0 ? 0 : reportedTotalAmount * 100 / budgetBfpAmount, 2);
            this.ContractsCount = contractsCount;
        }

        public string Programme { get; set; }

        public string ProgrammePriority { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RegionCategory RegionCategory { get; set; }

        public decimal BudgetBfpAmount { get; set; }

        public decimal ContractedTotalAmount { get; set; }

        public decimal ContractedPercent { get; set; }

        public decimal ContractedBfpAmount { get; set; }

        public decimal ReportedTotalAmount { get; set; }

        public decimal ReportedPercent { get; set; }

        public int ContractsCount { get; set; }
    }
}
