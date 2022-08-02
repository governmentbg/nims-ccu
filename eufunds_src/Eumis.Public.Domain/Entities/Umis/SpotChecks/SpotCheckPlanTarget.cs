using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public class SpotCheckPlanTarget
    {
        public int SpotCheckPlanTargetId { get; set; }

        public int SpotCheckPlanId { get; set; }

        public SpotCheckTargetType Type { get; set; }

        public string Name { get; set; }

        public virtual SpotCheckPlan Plan { get; set; }

        public void UpdateAttributes(SpotCheckTargetType type, string name)
        {
            this.Type = type;
            this.Name = name;
        }
    }

    public class SpotCheckPlanTargetMap : EntityTypeConfiguration<SpotCheckPlanTarget>
    {
        public SpotCheckPlanTargetMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckPlanTargetId);

            // Properties
            this.Property(t => t.SpotCheckPlanTargetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckPlanId)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(500)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckPlanTargets");
            this.Property(t => t.SpotCheckPlanTargetId).HasColumnName("SpotCheckPlanTargetId");
            this.Property(t => t.SpotCheckPlanId).HasColumnName("SpotCheckPlanId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Name).HasColumnName("Name");

            this.HasRequired(t => t.Plan)
                .WithMany(t => t.Targets)
                .HasForeignKey(t => t.SpotCheckPlanId)
                .WillCascadeOnDelete();
        }
    }
}
