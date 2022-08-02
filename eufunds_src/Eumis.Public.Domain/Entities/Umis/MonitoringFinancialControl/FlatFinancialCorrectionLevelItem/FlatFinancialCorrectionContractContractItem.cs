using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
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

    public class FlatFinancialCorrectionContractContractItemMap : EntityTypeConfiguration<FlatFinancialCorrectionContractContractItem>
    {
        public FlatFinancialCorrectionContractContractItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ContractContractId");
        }
    }
}
