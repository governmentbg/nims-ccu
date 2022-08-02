using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class FinancialExecutionTable2ReportItem
    {
        public FinancialExecutionTable2ReportItem(
            string programme,
            string programmePriority,
            string interventionField,
            string formOfFinance,
            string territorialDimension,
            string territorialDeliveryMechanism,
            string thematicObjective,
            string esfSecondaryTheme,
            string economicDimension,
            string nutsName,
            decimal contractTotalAmount,
            decimal contractBfpAmount,
            decimal reportedTotalAmount,
            int contractsCount)
        {
            this.Programme = programme;
            this.ProgrammePriority = programmePriority;
            this.RegionCategory = RegionCategory.LessDevelopedRegions;

            this.InterventionField = interventionField;
            this.FormOfFinance = formOfFinance;
            this.TerritorialDimension = territorialDimension;
            this.TerritorialDeliveryMechanism = territorialDeliveryMechanism;
            this.ThematicObjective = thematicObjective;
            this.ESFSecondaryTheme = esfSecondaryTheme;
            this.EconomicDimension = economicDimension;
            this.NutsName = nutsName;

            this.ContractTotalAmount = contractTotalAmount;
            this.ContractBfpAmount = contractBfpAmount;
            this.ReportedTotalAmount = reportedTotalAmount;
            this.ContractsCount = contractsCount;
        }

        public string Programme { get; set; }

        public string ProgrammePriority { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RegionCategory RegionCategory { get; set; }

        public string InterventionField { get; set; }

        public string FormOfFinance { get; set; }

        public string TerritorialDimension { get; set; }

        public string TerritorialDeliveryMechanism { get; set; }

        public string ThematicObjective { get; set; }

        public string ESFSecondaryTheme { get; set; }

        public string EconomicDimension { get; set; }

        public string NutsName { get; set; }

        public decimal ContractTotalAmount { get; set; }

        public decimal ContractBfpAmount { get; set; }

        public decimal ReportedTotalAmount { get; set; }

        public int ContractsCount { get; set; }
    }
}
