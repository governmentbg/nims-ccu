using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCorrectionDocument
    {
        public int ContractReportCorrectionDocumentId { get; set; }

        public int ContractReportCorrectionId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual ContractReportCorrection ContractReportCorrection { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportCorrectionDocumentMap : EntityTypeConfiguration<ContractReportCorrectionDocument>
    {
        public ContractReportCorrectionDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportCorrectionDocumentId);

            // Properties
            this.Property(t => t.ContractReportCorrectionDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportCorrectionId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportCorrectionDocuments");
            this.Property(t => t.ContractReportCorrectionDocumentId).HasColumnName("ContractReportCorrectionDocumentId");
            this.Property(t => t.ContractReportCorrectionId).HasColumnName("ContractReportCorrectionId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.ContractReportCorrection)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ContractReportCorrectionId)
                .WillCascadeOnDelete();
        }
    }
}
