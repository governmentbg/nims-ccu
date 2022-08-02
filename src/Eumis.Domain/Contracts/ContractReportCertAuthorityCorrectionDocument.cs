using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCertAuthorityCorrectionDocument
    {
        public int ContractReportCertAuthorityCorrectionDocumentId { get; set; }

        public int ContractReportCertAuthorityCorrectionId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual ContractReportCertAuthorityCorrection ContractReportCertAuthorityCorrection { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportCertAuthorityCorrectionDocumentMap : EntityTypeConfiguration<ContractReportCertAuthorityCorrectionDocument>
    {
        public ContractReportCertAuthorityCorrectionDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportCertAuthorityCorrectionDocumentId);

            // Properties
            this.Property(t => t.ContractReportCertAuthorityCorrectionDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportCertAuthorityCorrectionId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportCertAuthorityCorrectionDocuments");
            this.Property(t => t.ContractReportCertAuthorityCorrectionDocumentId).HasColumnName("ContractReportCertAuthorityCorrectionDocumentId");
            this.Property(t => t.ContractReportCertAuthorityCorrectionId).HasColumnName("ContractReportCertAuthorityCorrectionId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.ContractReportCertAuthorityCorrection)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ContractReportCertAuthorityCorrectionId)
                .WillCascadeOnDelete();
        }
    }
}
