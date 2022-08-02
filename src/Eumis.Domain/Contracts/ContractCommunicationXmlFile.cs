using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public class ContractCommunicationXmlFile : RioXmlFile
    {
        private ContractCommunicationXmlFile()
        {
        }

        public ContractCommunicationXmlFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ContractCommunicationXmlId { get; set; }

        public ContractCommunicationXml CommunicationXml { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractCommunicationXmlFileMap : EntityTypeConfiguration<ContractCommunicationXmlFile>
    {
        public ContractCommunicationXmlFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractCommunicationXmlId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractCommunicationXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ContractCommunicationXmlFileId");
            this.Property(t => t.ContractCommunicationXmlId).HasColumnName("ContractCommunicationXmlId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.CommunicationXml)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractCommunicationXmlId)
                .WillCascadeOnDelete();
        }
    }
}
