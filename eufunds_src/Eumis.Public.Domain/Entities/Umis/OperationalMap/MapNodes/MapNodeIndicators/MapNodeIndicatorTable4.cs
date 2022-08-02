using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public partial class MapNodeIndicatorTable4 : MapNodeIndicator
    {
        public MapNodeIndicatorTable4()
        {
        }

        public override MapNodeIndicatorType Type
        {
            get
            {
                return MapNodeIndicatorType.Table4;
            }
        }

        public RegionCategory RegionCategory { get; set; }
        public decimal? BaseTotalValue { get; set; }
        public decimal? BaseMenValue { get; set; }
        public decimal? BaseWomenValue { get; set; }
        public string BaseQualitativeValue { get; set; }
        public int? BaseYear { get; set; }
        public decimal? TargetTotalValue { get; set; }
        public decimal? TargetMenValue { get; set; }
        public decimal? TargetWomenValue { get; set; }
        public string TargetQualitativeValue { get; set; }
        public int? MeasureId { get; set; }
        public string ReportingFrequancy { get; set; }
        public string CommonBaseIndicator { get; set; }

        internal void SetAttributes( 
            RegionCategory regionCategory,
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            string baseQualitativeValue,
            int? baseYear,
            decimal? targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            string targetQualitativeValue,
            int? measureId,
            string dataSource,
            string reportingFrequancy,
            string commonBaseIndicator)
        {
            this.RegionCategory = regionCategory;
            this.BaseTotalValue = baseTotalValue;
            this.BaseMenValue = baseMenValue;
            this.BaseWomenValue = baseWomenValue;
            this.BaseQualitativeValue = baseQualitativeValue;
            this.BaseYear = baseYear;
            this.TargetTotalValue = targetTotalValue;
            this.TargetMenValue = targetMenValue;
            this.TargetWomenValue = targetWomenValue;
            this.TargetQualitativeValue = targetQualitativeValue;
            this.MeasureId = measureId;
            this.DataSource = dataSource;
            this.ReportingFrequancy = reportingFrequancy;
            this.CommonBaseIndicator = commonBaseIndicator;
        }
    }

    public class MapNodeIndicatorTable4Map : EntityTypeConfiguration<MapNodeIndicatorTable4>
    {
        public MapNodeIndicatorTable4Map()
        {
            // Properties
            this.Property(t => t.BaseQualitativeValue)
                .HasMaxLength(100);

            this.Property(t => t.TargetQualitativeValue)
                .HasMaxLength(100);

            this.Property(t => t.ReportingFrequancy)
                .HasMaxLength(100);

            this.Property(t => t.CommonBaseIndicator)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.Property(t => t.RegionCategory).HasColumnName("RegionCategory");
            this.Property(t => t.BaseTotalValue).HasColumnName("BaseTotalValue");
            this.Property(t => t.BaseMenValue).HasColumnName("BaseMenValue");
            this.Property(t => t.BaseWomenValue).HasColumnName("BaseWomenValue");
            this.Property(t => t.BaseQualitativeValue).HasColumnName("BaseQualitativeValue");
            this.Property(t => t.BaseYear).HasColumnName("BaseYear");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.TargetMenValue).HasColumnName("TargetMenValue");
            this.Property(t => t.TargetWomenValue).HasColumnName("TargetWomenValue");
            this.Property(t => t.TargetQualitativeValue).HasColumnName("TargetQualitativeValue");
            this.Property(t => t.MeasureId).HasColumnName("MeasureId");
            this.Property(t => t.ReportingFrequancy).HasColumnName("ReportingFrequancy");
            this.Property(t => t.CommonBaseIndicator).HasColumnName("CommonBaseIndicator");
        }
    }
}
