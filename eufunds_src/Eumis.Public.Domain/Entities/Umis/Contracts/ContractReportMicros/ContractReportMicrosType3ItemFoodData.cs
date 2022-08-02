using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public class ContractReportMicrosType3ItemFoodData
    {
        public decimal? TargetValue { get; set; }

        public decimal? ActualValue { get; set; }
    }

    public class ContractReportMicrosType3ItemFoodDataMap : ComplexTypeConfiguration<ContractReportMicrosType3ItemFoodData>
    {
        public ContractReportMicrosType3ItemFoodDataMap()
        {
        }
    }
}
