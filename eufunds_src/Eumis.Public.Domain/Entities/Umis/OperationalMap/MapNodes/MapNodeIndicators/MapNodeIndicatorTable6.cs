using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public partial class MapNodeIndicatorTable6 : MapNodeIndicator
    {
        public MapNodeIndicatorTable6()
        {
        }

        public override MapNodeIndicatorType Type
        {
            get
            {
                return MapNodeIndicatorType.Table6;
            }
        }

        public RegionCategory RegionCategory { get; set; }
        public FinanceSource FinanceSource { get; set; }
        public decimal? MilestoneTargetTotalValue { get; set; }
        public decimal? MilestoneTargetMenValue { get; set; }
        public decimal? MilestoneTargetWomenValue { get; set; }
        public decimal FinalTargetTotalValue { get; set; }
        public decimal? FinalTargetMenValue { get; set; }
        public decimal? FinalTargetWomenValue { get; set; }
        public string Description { get; set; }

        internal void SetAttributes(
            RegionCategory regionCategory,
            FinanceSource financeSource,
            decimal? milestoneTargetTotalValue,
            decimal? milestoneTargetMenValue,
            decimal? milestoneTargetWomenValue,
            decimal finalTargetTotalValue,
            decimal? finalTargetMenValue,
            decimal? finalTargetWomenValue,
            string dataSource,
            string description)
        {
            this.RegionCategory = regionCategory;
            this.FinanceSource = financeSource;
            this.MilestoneTargetTotalValue = milestoneTargetTotalValue;
            this.MilestoneTargetMenValue = milestoneTargetMenValue;
            this.MilestoneTargetWomenValue = milestoneTargetWomenValue;
            this.FinalTargetTotalValue = finalTargetTotalValue;
            this.FinalTargetMenValue = finalTargetMenValue;
            this.FinalTargetWomenValue = finalTargetWomenValue;
            this.DataSource = dataSource;
            this.Description = description;
        }
    }

    public class MapNodeIndicatorTable6Map : EntityTypeConfiguration<MapNodeIndicatorTable6>
    {
        public MapNodeIndicatorTable6Map()
        {
            // Table & Column Mappings
            this.Property(t => t.RegionCategory).HasColumnName("RegionCategory");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");
            this.Property(t => t.MilestoneTargetTotalValue).HasColumnName("MilestoneTargetTotalValue");
            this.Property(t => t.MilestoneTargetMenValue).HasColumnName("MilestoneTargetMenValue");
            this.Property(t => t.MilestoneTargetWomenValue).HasColumnName("MilestoneTargetWomenValue");
            this.Property(t => t.FinalTargetTotalValue).HasColumnName("FinalTargetTotalValue");
            this.Property(t => t.FinalTargetMenValue).HasColumnName("FinalTargetMenValue");
            this.Property(t => t.FinalTargetWomenValue).HasColumnName("FinalTargetWomenValue");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
