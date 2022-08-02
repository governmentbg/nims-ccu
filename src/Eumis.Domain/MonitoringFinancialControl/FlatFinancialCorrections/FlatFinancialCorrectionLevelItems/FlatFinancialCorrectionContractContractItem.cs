using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems
{
    public class FlatFinancialCorrectionContractContractItem : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem
    {
        public FlatFinancialCorrectionContractContractItem()
        {
        }

        public override FlatFinancialCorrectionLevel Type
        {
            get
            {
                return FlatFinancialCorrectionLevel.ContractContract;
            }
        }

        public int ItemId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FlatFinancialCorrectionContractContractItemMap : EntityTypeConfiguration<FlatFinancialCorrectionContractContractItem>
    {
        public FlatFinancialCorrectionContractContractItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ContractContractId");
        }
    }
}
