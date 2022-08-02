using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertAuthorityChecks
{
    public partial class CertAuthorityCheckProject
    {
        public int CertAuthorityCheckProjectId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        public int ProjectId { get; set; }

        public virtual CertAuthorityCheck Check { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CertAuthorityCheckProjectMap : EntityTypeConfiguration<CertAuthorityCheckProject>
    {
        public CertAuthorityCheckProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.CertAuthorityCheckProjectId);

            // Properties
            this.Property(t => t.CertAuthorityCheckProjectId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CertAuthorityCheckId)
                .IsRequired();

            this.Property(t => t.ProjectId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CertAuthorityCheckProjects");
            this.Property(t => t.CertAuthorityCheckProjectId).HasColumnName("CertAuthorityCheckProjectId");
            this.Property(t => t.CertAuthorityCheckId).HasColumnName("CertAuthorityCheckId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Projects)
                .HasForeignKey(t => t.CertAuthorityCheckId)
                .WillCascadeOnDelete();
        }
    }
}
