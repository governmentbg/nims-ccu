using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public class ContractReportMicrosType3ItemFoodData
    {
        public decimal? TargetValue { get; set; }

        public decimal? ActualValue { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMicrosType3ItemFoodDataMap : ComplexTypeConfiguration<ContractReportMicrosType3ItemFoodData>
    {
        public ContractReportMicrosType3ItemFoodDataMap()
        {
        }
    }
}
