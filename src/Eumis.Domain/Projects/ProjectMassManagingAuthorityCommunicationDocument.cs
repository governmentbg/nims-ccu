using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Projects
{
    public class ProjectMassManagingAuthorityCommunicationDocument
    {
        public int ProjectMassManagingAuthorityCommunicationDocumentId { get; set; }

        public int ProjectMassManagingAuthorityCommunicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid? FileKey { get; set; }

        public virtual ProjectMassManagingAuthorityCommunication Communication { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectMassManagingAuthorityCommunicationDocumentMap : EntityTypeConfiguration<ProjectMassManagingAuthorityCommunicationDocument>
    {
        public ProjectMassManagingAuthorityCommunicationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectMassManagingAuthorityCommunicationDocumentId);

            // Properties
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectMassManagingAuthorityCommunicationId)
                .IsRequired();

            this.Property(t => t.FileName)
                .IsOptional();

            this.Property(t => t.FileKey)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ProjectMassManagingAuthorityCommunicationDocuments");
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationDocumentId).HasColumnName("ProjectMassManagingAuthorityCommunicationDocumentId");
            this.Property(t => t.ProjectMassManagingAuthorityCommunicationId).HasColumnName("ProjectMassManagingAuthorityCommunicationId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Communication)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ProjectMassManagingAuthorityCommunicationId)
                .WillCascadeOnDelete();
        }
    }
}
