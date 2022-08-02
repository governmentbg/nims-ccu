using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Projects
{
    public class ProjectVersionXmlFile : RioXmlFile
    {
        private ProjectVersionXmlFile()
        {
        }

        public ProjectVersionXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ProjectVersionXmlId { get; set; }

        public ProjectVersionXmlFileType Type { get; set; }

        public ProjectVersionXml ProjectVersionXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectVersionXmlFileMap : EntityTypeConfiguration<ProjectVersionXmlFile>
    {
        public ProjectVersionXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProjectVersionXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProjectVersionXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ProjectVersionXmlFileId");
            this.Property(t => t.ProjectVersionXmlId).HasColumnName("ProjectVersionXmlId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.ProjectVersionXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ProjectVersionXmlId)
                .WillCascadeOnDelete();
        }
    }
}
