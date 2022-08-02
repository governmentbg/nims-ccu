using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Registrations
{
    public class RegOfferXmlFile : RioXmlFile
    {
        private RegOfferXmlFile()
        {
        }

        public RegOfferXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int RegOfferXmlId { get; set; }

        public RegOfferXmlFileType Type { get; set; }

        public RegOfferXml RegOfferXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class RegOfferXmlFileMap : EntityTypeConfiguration<RegOfferXmlFile>
    {
        public RegOfferXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.RegOfferXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("RegOfferXmlFiles");
            this.Property(t => t.FileId).HasColumnName("RegOfferXmlFileId");
            this.Property(t => t.RegOfferXmlId).HasColumnName("RegOfferXmlId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.RegOfferXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.RegOfferXmlId)
                .WillCascadeOnDelete();
        }
    }
}
