using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public partial class MapNodeIndicatorTable3 : MapNodeIndicator
    {
        public MapNodeIndicatorTable3()
        {
        }

        public override MapNodeIndicatorType Type
        {
            get
            {
                return MapNodeIndicatorType.Table3;
            }
        }

        public RegionCategory RegionCategory { get; set; }
        public decimal? BaseTotalValue { get; set; }
        public int? BaseYear { get; set; }
        public decimal? TargetTotalValue { get; set; }
        public string ReportingFrequancy { get; set; }

        internal void SetAttributes(
            RegionCategory regionCategory,
            decimal? baseTotalValue,
            int? baseYear,
            decimal? targetTotalValue,
            string dataSource,
            string reportingFrequancy)
        {
            this.RegionCategory = regionCategory;
            this.BaseTotalValue = baseTotalValue;
            this.BaseYear = baseYear;
            this.TargetTotalValue = targetTotalValue;
            this.DataSource = dataSource;
            this.ReportingFrequancy = reportingFrequancy;
        }
    }

    public class MapNodeIndicatorTable3Map : EntityTypeConfiguration<MapNodeIndicatorTable3>
    {
        public MapNodeIndicatorTable3Map()
        {
            // Properties
            this.Property(t => t.ReportingFrequancy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.Property(t => t.RegionCategory).HasColumnName("RegionCategory");
            this.Property(t => t.BaseTotalValue).HasColumnName("BaseTotalValue");
            this.Property(t => t.BaseYear).HasColumnName("BaseYear");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.ReportingFrequancy).HasColumnName("ReportingFrequancy");
        }
    }
}
