using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public class MapNodeFinanceSource
    {
        public int MapNodeId { get; set; }

        public FinanceSource FinanceSource { get; set; }

        public virtual Programme Programme { get; set; }
    }

    public class MapNodeFinanceSourceMap : EntityTypeConfiguration<MapNodeFinanceSource>
    {
        public MapNodeFinanceSourceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MapNodeId, t.FinanceSource });

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.FinanceSource)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MapNodeFinanceSources");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");

            this.HasRequired(t => t.Programme)
                .WithMany(t => t.MapNodeFinanceSources)
                .HasForeignKey(d => d.MapNodeId);
        }
    }
}
