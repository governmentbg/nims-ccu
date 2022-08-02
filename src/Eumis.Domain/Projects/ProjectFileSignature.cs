using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectFileSignature
    {
        public ProjectFileSignature()
        {
        }

        public int ProjectFileSignatureId { get; set; }

        public int ProjectFileId { get; set; }

        public byte[] Signature { get; set; }

        public string FileName { get; set; }

        public ProjectFile ProjectFile { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectFileSignatureMap : EntityTypeConfiguration<ProjectFileSignature>
    {
        public ProjectFileSignatureMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectFileSignatureId);

            // Properties
            this.Property(t => t.ProjectFileSignatureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Signature)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectFileSignatures");
            this.Property(t => t.ProjectFileSignatureId).HasColumnName("ProjectFileSignatureId");
            this.Property(t => t.ProjectFileId).HasColumnName("ProjectFileId");
            this.Property(t => t.Signature).HasColumnName("Signature");
            this.Property(t => t.FileName).HasColumnName("FileName");

            this.HasRequired(t => t.ProjectFile)
                .WithMany(t => t.ProjectFileSignatures)
                .HasForeignKey(t => t.ProjectFileId);
        }
    }
}
