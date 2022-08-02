using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public partial class ProjectCommunicationFileSignature
    {
        public ProjectCommunicationFileSignature()
        {
        }

        public int ProjectCommunicationFileSignatureId { get; set; }

        public int ProjectCommunicationFileId { get; set; }

        public byte[] Signature { get; set; }

        public string FileName { get; set; }

        public ProjectCommunicationFile ProjectCommunicationFile { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommunicationFileSignatureMap : EntityTypeConfiguration<ProjectCommunicationFileSignature>
    {
        public ProjectCommunicationFileSignatureMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectCommunicationFileSignatureId);

            // Properties
            this.Property(t => t.ProjectCommunicationFileSignatureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Signature)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectCommunicationFileSignatures");
            this.Property(t => t.ProjectCommunicationFileSignatureId).HasColumnName("ProjectCommunicationFileSignatureId");
            this.Property(t => t.ProjectCommunicationFileId).HasColumnName("ProjectCommunicationFileId");
            this.Property(t => t.Signature).HasColumnName("Signature");
            this.Property(t => t.FileName).HasColumnName("FileName");

            this.HasRequired(t => t.ProjectCommunicationFile)
                .WithMany(t => t.ProjectCommunicationFileSignatures)
                .HasForeignKey(t => t.ProjectCommunicationFileId);
        }
    }
}