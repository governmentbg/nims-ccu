using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public partial class MapNodeIndicatorTable5 : MapNodeIndicator
    {
        public MapNodeIndicatorTable5()
        {
        }

        public override MapNodeIndicatorType Type
        {
            get
            {
                return MapNodeIndicatorType.Table5;
            }
        }

        public RegionCategory RegionCategory { get; set; }
        public FinanceSource FinanceSource { get; set; }
        public decimal TargetTotalValue { get; set; }
        public decimal? TargetMenValue { get; set; }
        public decimal? TargetWomenValue { get; set; }
        public string ReportingFrequancy { get; set; }

        internal void SetAttributes(
            FinanceSource financeSource,
            RegionCategory regionCategory,
            decimal targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            string dataSource,
            string reportingFrequancy)
        {
            this.RegionCategory = regionCategory;
            this.FinanceSource = financeSource;
            this.TargetTotalValue = targetTotalValue;
            this.TargetMenValue = targetMenValue;
            this.TargetWomenValue = targetWomenValue;
            this.DataSource = dataSource;
            this.ReportingFrequancy = reportingFrequancy;
        }
    }

    public class MapNodeIndicatorTable5Map : EntityTypeConfiguration<MapNodeIndicatorTable5>
    {
        public MapNodeIndicatorTable5Map()
        {
            // Properties
            this.Property(t => t.ReportingFrequancy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.Property(t => t.RegionCategory).HasColumnName("RegionCategory");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.TargetMenValue).HasColumnName("TargetMenValue");
            this.Property(t => t.TargetWomenValue).HasColumnName("TargetWomenValue");
            this.Property(t => t.ReportingFrequancy).HasColumnName("ReportingFrequancy");
        }
    }
}
