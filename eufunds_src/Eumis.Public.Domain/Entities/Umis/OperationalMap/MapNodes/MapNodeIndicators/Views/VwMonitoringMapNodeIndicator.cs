using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Indicators;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators.Views
{
    public class VwMonitoringMapNodeIndicator
    {
        public int MapNodeId { get; set; }

        public int IndicatorId { get; set; }

        public int ProgrammeId { get; set; }

        public IndicatorType Type { get; set; }

        public string Name { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? BaseTotalValue { get; set; }
    }

    public class VwMonitoringMapNodeIndicatorMap : EntityTypeConfiguration<VwMonitoringMapNodeIndicator>
    {
        public VwMonitoringMapNodeIndicatorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MapNodeId, t.IndicatorId });

            // Properties
            this.Property(t => t.MapNodeId)
                .IsRequired();
            this.Property(t => t.IndicatorId)
                .IsRequired();
            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("vwMonitoringMapNodeIndicators");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.IndicatorId).HasColumnName("IndicatorId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TargetTotalValue).HasColumnName("TargetTotalValue");
            this.Property(t => t.BaseTotalValue).HasColumnName("BaseTotalValue");
        }
    }
}