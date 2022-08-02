using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public partial class MapNodeIndicatorTable4a : MapNodeIndicator
    {
        public MapNodeIndicatorTable4a()
        {
        }

        public override MapNodeIndicatorType Type
        {
            get
            {
                return MapNodeIndicatorType.Table4a;
            }
        }

        public decimal? BaseTotalValue { get; set; }
        public decimal? BaseMenValue { get; set; }
        public decimal? BaseWomenValue { get; set; }
        public int? BaseYear { get; set; }
        public decimal TargetTotalValue { get; set; }
        public decimal? TargetMenValue { get; set; }
        public decimal? TargetWomenValue { get; set; }
        public int? MeasureId { get; set; }
        public string ReportingFrequancy { get; set; }
        public string CommonBaseIndicator { get; set; }

        internal void SetAttributes(
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            int? baseYear,
            decimal targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            int? measureId,
            string dataSource,
            string reportingFrequancy,
            string commonBaseIndicator)
        {
            this.BaseTotalValue = baseTotalValue;
            this.BaseMenValue = baseMenValue;
            this.BaseWomenValue = baseWomenValue;
            this.BaseYear = baseYear;
            this.TargetTotalValue = targetTotalValue;
            this.TargetMenValue = targetMenValue;
            this.TargetWomenValue = targetWomenValue;
            this.MeasureId = measureId;
            this.DataSource = dataSource;
            this.ReportingFrequancy = reportingFrequancy;
            this.CommonBaseIndicator = commonBaseIndicator;
        }
    }

    public class MapNodeIndicatorTable4aMap : EntityTypeConfiguration<MapNodeIndicatorTable4a>
    {
        public MapNodeIndicatorTable4aMap()
        {
            // Properties
            this.Property(t => t.ReportingFrequancy)
                .HasMaxLength(100);

            this.Property(t => t.CommonBaseIndicator)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.Property(t => t.BaseTotalValue).HasColumnName("BaseTotalValue");
            this.Property(t => t.BaseMenValue).HasColumnName("BaseMenValue");
            this.Property(t => t.BaseWomenValue).HasColumnName("BaseWomenValue");
            this.Property(t => t.BaseYear).HasColumnName("BaseYear");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.TargetMenValue).HasColumnName("TargetMenValue");
            this.Property(t => t.TargetWomenValue).HasColumnName("TargetWomenValue");
            this.Property(t => t.MeasureId).HasColumnName("MeasureId");
            this.Property(t => t.ReportingFrequancy).HasColumnName("ReportingFrequancy");
            this.Property(t => t.CommonBaseIndicator).HasColumnName("CommonBaseIndicator");
        }
    }
}
