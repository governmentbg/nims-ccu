using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems
{
    public partial class FlatFinancialCorrectionContractItem : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem
    {
        public FlatFinancialCorrectionContractItem()
        {
        }

        public override FlatFinancialCorrectionLevel Type
        {
            get
            {
                return FlatFinancialCorrectionLevel.Contract;
            }
        }

        public int ItemId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FlatFinancialCorrectionContractItemMap : EntityTypeConfiguration<FlatFinancialCorrectionContractItem>
    {
        public FlatFinancialCorrectionContractItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ContractId");
        }
    }
}
