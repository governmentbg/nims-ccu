using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public class FlatFinancialCorrectionProcedureItem : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem
    {
        public FlatFinancialCorrectionProcedureItem()
        {
        }

        public override FlatFinancialCorrectionLevel Type
        {
            get
            {
                return FlatFinancialCorrectionLevel.Procedure;
            }
        }

        public int ItemId { get; set; }
    }

    public class FlatFinancialCorrectionProcedureItemMap : EntityTypeConfiguration<FlatFinancialCorrectionProcedureItem>
    {
        public FlatFinancialCorrectionProcedureItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ProcedureId");
        }
    }
}
