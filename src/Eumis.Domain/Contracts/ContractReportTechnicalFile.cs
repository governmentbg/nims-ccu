using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Contracts
{
    public class ContractReportTechnicalFile : RioXmlFile
    {
        private ContractReportTechnicalFile()
        {
        }

        public ContractReportTechnicalFile(AttachedDocument attachedDocument)
            : base(attachedDocument)
        {
        }

        public int ContractReportTechnicalId { get; set; }

        public ContractReportTechnicalFileType Type { get; set; }

        public ContractReportTechnical TechReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportTechnicalFileMap : EntityTypeConfiguration<ContractReportTechnicalFile>
    {
        public ContractReportTechnicalFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.FileId);

            // Properties
            this.Property(t => t.FileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportTechnicalId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportTechnicalXmlFiles");
            this.Property(t => t.FileId).HasColumnName("ContractReportTechnicalXmlFileId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ContractReportTechnicalId).HasColumnName("ContractReportTechnicalId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.TechReport)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractReportTechnicalId)
                .WillCascadeOnDelete();
        }
    }
}
