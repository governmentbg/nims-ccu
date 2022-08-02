using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public class ProjectCommunicationMessageFile : RioXmlFile
    {
        public int ProjectCommunicationId { get; set; }

        public ProjectCommunicationMessageType MessageType { get; set; }

        public ProjectCommunication Communication { get; set; }
    }

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
            this.Property(t => t.MessageType).HasColumnName("MessageType");
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
