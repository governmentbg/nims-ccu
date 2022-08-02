using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public class ContractProcurementXmlFile : RioXmlFile
    {
        private ContractProcurementXmlFile()
        {
        }

        public ContractProcurementXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ContractProcurementXmlId { get; set; }

        public ContractProcurementXmlFileType Type { get; set; }

        public ContractProcurementXml ContractProcurementXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractProcurementXmlFileMap : EntityTypeConfiguration<ContractProcurementXmlFile>
    {
        public ContractProcurementXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractProcurementXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractProcurementXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ContractProcurementXmlFileId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ContractProcurementXmlId).HasColumnName("ContractProcurementXmlId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.ContractProcurementXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractProcurementXmlId)
                .WillCascadeOnDelete();
        }
    }
}
