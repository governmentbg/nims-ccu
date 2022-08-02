using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
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

    public class FlatFinancialCorrectionProgrammeItemMap : EntityTypeConfiguration<FlatFinancialCorrectionProgrammeItem>
    {
        public FlatFinancialCorrectionProgrammeItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ProgrammeId");
        }
    }
}
