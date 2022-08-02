using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCertCorrectionDocument
    {
        public int ContractReportCertCorrectionDocumentId { get; set; }

        public int ContractReportCertCorrectionId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual ContractReportCertCorrection ContractReportCertCorrection { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportCertCorrectionDocumentMap : EntityTypeConfiguration<ContractReportCertCorrectionDocument>
    {
        public ContractReportCertCorrectionDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportCertCorrectionDocumentId);

            // Properties
            this.Property(t => t.ContractReportCertCorrectionDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportCertCorrectionId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportCertCorrectionDocuments");
            this.Property(t => t.ContractReportCertCorrectionDocumentId).HasColumnName("ContractReportCertCorrectionDocumentId");
            this.Property(t => t.ContractReportCertCorrectionId).HasColumnName("ContractReportCertCorrectionId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.ContractReportCertCorrection)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ContractReportCertCorrectionId)
                .WillCascadeOnDelete();
        }
    }
}
