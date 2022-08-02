using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Registrations
{
    public class RegProjectXmlFile : RioXmlFile
    {
        private RegProjectXmlFile()
        {
        }

        public RegProjectXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int RegProjectXmlId { get; set; }

        public RegProjectXmlFileType Type { get; set; }

        public RegProjectXml RegProjectXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RegProjectXmlFileMap : EntityTypeConfiguration<RegProjectXmlFile>
    {
        public RegProjectXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.RegProjectXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("RegProjectXmlFiles");
            this.Property(t => t.FileId).HasColumnName("RegProjectXmlFileId");
            this.Property(t => t.RegProjectXmlId).HasColumnName("RegProjectXmlId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.RegProjectXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.RegProjectXmlId)
                .WillCascadeOnDelete();
        }
    }
}
