using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public class FlatFinancialCorrectionProgrammePriorityItem : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem
    {
        public FlatFinancialCorrectionProgrammePriorityItem()
        {
        }

        public override FlatFinancialCorrectionLevel Type
        {
            get
            {
                return FlatFinancialCorrectionLevel.ProgrammePriority;
            }
        }

        public int ItemId { get; set; }
    }

    public class FlatFinancialCorrectionProgrammePriorityItemMap : EntityTypeConfiguration<FlatFinancialCorrectionProgrammePriorityItem>
    {
        public FlatFinancialCorrectionProgrammePriorityItemMap()
        {
            // Table & Column Mappings
            this.Property(t => t.ItemId).HasColumnName("ProgrammePriorityId");
        }
    }
}
