using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
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

    public class FlatFinancialCorrectionContractItemMap : EntityTypeConfiguration<FlatFinancialCorrectionContractItem>
    {
        public FlatFinancialCorrectionContractItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ContractId");
        }
    }
}
