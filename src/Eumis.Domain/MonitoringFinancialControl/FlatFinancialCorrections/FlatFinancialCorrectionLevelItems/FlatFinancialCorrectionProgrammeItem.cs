using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems
{
    public class FlatFinancialCorrectionProgrammeItem : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem
    {
        public FlatFinancialCorrectionProgrammeItem()
        {
        }

        public override FlatFinancialCorrectionLevel Type
        {
            get
            {
                return FlatFinancialCorrectionLevel.Programme;
            }
        }

        public int ItemId { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FlatFinancialCorrectionProgrammeItemMap : EntityTypeConfiguration<FlatFinancialCorrectionProgrammeItem>
    {
        public FlatFinancialCorrectionProgrammeItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ProgrammeId");
        }
    }
}
