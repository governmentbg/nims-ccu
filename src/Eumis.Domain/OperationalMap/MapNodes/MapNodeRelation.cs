using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public class MapNodeRelation
    {
        public MapNodeRelation()
        {
        }

        public int MapNodeId { get; set; }

        public int? ParentMapNodeId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public virtual MapNode MapNode { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MapNodeRelationMap : EntityTypeConfiguration<MapNodeRelation>
    {
        public MapNodeRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.MapNodeId);

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MapNodeRelations");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.ParentMapNodeId).HasColumnName("ParentMapNodeId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");

            // Relationships
            this.HasRequired(t => t.MapNode)
                .WithOptional(t => t.MapNodeRelation);
        }
    }
}
