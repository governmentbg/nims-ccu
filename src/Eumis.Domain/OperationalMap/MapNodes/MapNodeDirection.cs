using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public class MapNodeDirection
    {
        public MapNodeDirection()
        {
        }

        public MapNodeDirection(int directionId, int? subDirectionId)
        {
            this.DirectionId = directionId;
            this.SubDirectionId = subDirectionId;
        }

        public int MapNodeDirectionId { get; set; }

        public int MapNodeId { get; set; }

        public int DirectionId { get; set; }

        public int? SubDirectionId { get; set; }

        public virtual MapNodeWithDirections MapNodeDirections { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MapNodeDirectionMap : EntityTypeConfiguration<MapNodeDirection>
    {
        public MapNodeDirectionMap()
        {
            // Primary Key
            this.HasKey(t => t.MapNodeDirectionId);

            // Properties
            this.Property(t => t.MapNodeDirectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.MapNodeId)
                .IsRequired();
            this.Property(t => t.DirectionId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MapNodeDirections");
            this.Property(t => t.MapNodeDirectionId).HasColumnName("MapNodeDirectionId");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.DirectionId).HasColumnName("DirectionId");
            this.Property(t => t.SubDirectionId).HasColumnName("SubDirectionId");

            // Relationships
            this.HasRequired(t => t.MapNodeDirections)
                .WithMany(t => t.MapNodeDirections)
                .HasForeignKey(d => d.MapNodeId)
                .WillCascadeOnDelete();
        }
    }
}
