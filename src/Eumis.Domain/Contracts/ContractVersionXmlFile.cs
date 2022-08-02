using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public class ContractVersionXmlFile : RioXmlFile
    {
        private ContractVersionXmlFile()
        {
        }

        public ContractVersionXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ContractVersionXmlId { get; set; }

        public ContractVersionXmlFileType Type { get; set; }

        public ContractVersionXml ContractVersionXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractVersionXmlFileMap : EntityTypeConfiguration<ContractVersionXmlFile>
    {
        public ContractVersionXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractVersionXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractVersionXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ContractVersionXmlFileId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ContractVersionXmlId).HasColumnName("ContractVersionXmlId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.ContractVersionXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractVersionXmlId)
                .WillCascadeOnDelete();
        }
    }
}
