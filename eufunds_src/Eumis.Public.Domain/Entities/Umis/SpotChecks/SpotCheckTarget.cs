using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public class SpotCheckTarget
    {
        public int SpotCheckTargetId { get; set; }

        public int SpotCheckId { get; set; }

        public SpotCheckTargetType Type { get; set; }

        public string Name { get; set; }

        public virtual SpotCheck Check { get; set; }

        public void UpdateAttributes(SpotCheckTargetType type, string name)
        {
            this.Type = type;
            this.Name = name;
        }
    }

    public class SpotCheckTargetMap : EntityTypeConfiguration<SpotCheckTarget>
    {
        public SpotCheckTargetMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckTargetId);

            // Properties
            this.Property(t => t.SpotCheckTargetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckId)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(500)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckTargets");
            this.Property(t => t.SpotCheckTargetId).HasColumnName("SpotCheckTargetId");
            this.Property(t => t.SpotCheckId).HasColumnName("SpotCheckId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Name).HasColumnName("Name");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Targets)
                .HasForeignKey(t => t.SpotCheckId)
                .WillCascadeOnDelete();
        }
    }
}
