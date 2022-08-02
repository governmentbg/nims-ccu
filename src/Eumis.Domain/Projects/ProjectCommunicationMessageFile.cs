using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Projects
{
    public class ProjectCommunicationMessageFile : RioXmlFile
    {
        private ProjectCommunicationMessageFile()
        {
        }

        public ProjectCommunicationMessageFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ProjectCommunicationId { get; set; }

        public int? ProjectCommunicationAnswerId { get; set; }

        public ProjectCommunicationMessageType MessageType { get; set; }

        public ProjectCommunicationMessageFileType Type { get; set; }

        public ProjectCommonCommunication Communication { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommunicationMessageFileMap : EntityTypeConfiguration<ProjectCommunicationMessageFile>
    {
        public ProjectCommunicationMessageFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectCommunicationId)
                .IsRequired();

            this.Property(t => t.MessageType)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectCommunicationMessageFiles");
            this.Property(t => t.FileId).HasColumnName("ProjectCommunicationMessageFileId");
            this.Property(t => t.ProjectCommunicationId).HasColumnName("ProjectCommunicationId");
            this.Property(t => t.ProjectCommunicationAnswerId).HasColumnName("ProjectCommunicationAnswerId");
            this.Property(t => t.MessageType).HasColumnName("MessageType");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.Communication)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ProjectCommunicationId)
                .WillCascadeOnDelete();
        }
    }
}
