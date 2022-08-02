using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public class MapNodeInterventionCategory
    {
        public MapNodeInterventionCategory()
        {
        }

        public int MapNodeId { get; set; }
        public int InterventionCategoryId { get; set; }
        public decimal Amount { get; set; }

        public virtual MapNodeWithCategories MapNodeWithCategories { get; set; }

        internal void SetAttributes(decimal amount)
        {
            this.Amount = amount;
        }
    }

    public class MapNodeInterventionCategoryMap : EntityTypeConfiguration<MapNodeInterventionCategory>
    {
        public MapNodeInterventionCategoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MapNodeId, t.InterventionCategoryId });

            // Properties
            this.Property(t => t.MapNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.InterventionCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MapNodeInterventionCategories");
            this.Property(t => t.MapNodeId).HasColumnName("MapNodeId");
            this.Property(t => t.InterventionCategoryId).HasColumnName("InterventionCategoryId");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.MapNodeWithCategories)
                .WithMany(t => t.MapNodeInterventionCategories)
                .HasForeignKey(d => d.MapNodeId)
                .WillCascadeOnDelete();
        }
    }
}
