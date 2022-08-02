using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Audits
{
    public partial class AuditProject
    {
        public int AuditProjectId { get; set; }

        public int AuditId { get; set; }

        public int ProjectId { get; set; }

        public virtual Audit Audit { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AuditProjectMap : EntityTypeConfiguration<AuditProject>
    {
        public AuditProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditProjectId);

            // Properties
            this.Property(t => t.AuditProjectId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AuditId)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AuditProjects");
            this.Property(t => t.AuditProjectId).HasColumnName("AuditProjectId");
            this.Property(t => t.AuditId).HasColumnName("AuditId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");

            this.HasRequired(t => t.Audit)
                .WithMany(t => t.Projects)
                .HasForeignKey(t => t.AuditId)
                .WillCascadeOnDelete();
        }
    }
}
