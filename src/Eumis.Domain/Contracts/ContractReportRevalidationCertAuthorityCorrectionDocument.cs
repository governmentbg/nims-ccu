using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportRevalidationCertAuthorityCorrectionDocument
    {
        public int ContractReportRevalidationCertAuthorityCorrectionDocumentId { get; set; }

        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual ContractReportRevalidationCertAuthorityCorrection ContractReportRevalidationCertAuthorityCorrection { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportRevalidationCertAuthorityCorrectionDocumentMap : EntityTypeConfiguration<ContractReportRevalidationCertAuthorityCorrectionDocument>
    {
        public ContractReportRevalidationCertAuthorityCorrectionDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportRevalidationCertAuthorityCorrectionDocumentId);

            // Properties
            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportRevalidationCertAuthorityCorrectionDocuments");
            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionDocumentId).HasColumnName("ContractReportRevalidationCertAuthorityCorrectionDocumentId");
            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionId).HasColumnName("ContractReportRevalidationCertAuthorityCorrectionId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.ContractReportRevalidationCertAuthorityCorrection)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ContractReportRevalidationCertAuthorityCorrectionId)
                .WillCascadeOnDelete();
        }
    }
}
