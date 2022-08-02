using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FlatFinancialCorrectionProcedureItemMap : EntityTypeConfiguration<FlatFinancialCorrectionProcedureItem>
    {
        public FlatFinancialCorrectionProcedureItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ProcedureId");
        }
    }
}
