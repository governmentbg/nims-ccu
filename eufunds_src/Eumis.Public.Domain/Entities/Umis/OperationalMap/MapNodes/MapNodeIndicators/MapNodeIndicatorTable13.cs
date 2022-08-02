using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public partial class MapNodeIndicatorTable13 : MapNodeIndicator
    {
        public MapNodeIndicatorTable13()
        {
        }

        public override MapNodeIndicatorType Type
        {
            get
            {
                return MapNodeIndicatorType.Table13;
            }
        }

        public decimal TargetTotalValue { get; set; }
        public decimal? TargetMenValue { get; set; }
        public decimal? TargetWomenValue { get; set; }

        internal void SetAttributes(
            decimal targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            string dataSource)
        {
            this.TargetTotalValue = targetTotalValue;
            this.TargetMenValue = targetMenValue;
            this.TargetWomenValue = targetWomenValue;
            this.DataSource = dataSource;
        }
    }

    public class MapNodeIndicatorTable13Map : EntityTypeConfiguration<MapNodeIndicatorTable13>
    {
        public MapNodeIndicatorTable13Map()
        {
            // Table & Column Mappings
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.TargetMenValue).HasColumnName("TargetMenValue");
            this.Property(t => t.TargetWomenValue).HasColumnName("TargetWomenValue");
        }
    }
}
