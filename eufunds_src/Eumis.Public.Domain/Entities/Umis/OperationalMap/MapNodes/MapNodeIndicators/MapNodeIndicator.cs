using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.MapNodeIndicators
{
    public abstract partial class MapNodeIndicator
    {
        public MapNodeIndicator()
        {
        }

        public abstract MapNodeIndicatorType Type { get; }

        public int MapNodeId { get; set; }
        public int IndicatorId { get; set; }
        public string DataSource { get; set; }

        public virtual MapNode MapNode { get; set; }
    }

    public class MapNodeIndicatorMap : EntityTypeConfiguration<MapNodeIndicator>
    {
        public MapNodeIndicatorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MapNodeId, t.IndicatorId });

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IndicatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Ignore(t => t.Type);

            // Table & Column Mappings
            this.ToTable("MapNodeIndicators");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.IndicatorId).HasColumnName("IndicatorId");
            this.Property(t => t.DataSource).HasColumnName("DataSource");

            // Relationships
            this.HasRequired(t => t.MapNode)
                .WithMany(t => t.MapNodeIndicators)
                .HasForeignKey(d => d.MapNodeId)
                .WillCascadeOnDelete();

            // Derived entities
            this.Map<MapNodeIndicatorTable3>(t => t.Requires("Type").HasValue("Table3"));
            this.Map<MapNodeIndicatorTable4>(t => t.Requires("Type").HasValue("Table4"));
            this.Map<MapNodeIndicatorTable4a>(t => t.Requires("Type").HasValue("Table4a"));
            this.Map<MapNodeIndicatorTable5>(t => t.Requires("Type").HasValue("Table5"));
            this.Map<MapNodeIndicatorTable6>(t => t.Requires("Type").HasValue("Table6"));
            this.Map<MapNodeIndicatorTable12>(t => t.Requires("Type").HasValue("Table12"));
            this.Map<MapNodeIndicatorTable13>(t => t.Requires("Type").HasValue("Table13"));
        }
    }
}
