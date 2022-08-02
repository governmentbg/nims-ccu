using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.AnnualAccountReports
{
    public class AnnualAccountReportCertificationDocument
    {
        public AnnualAccountReportCertificationDocument()
        {
        }

        public int AnnualAccountReportCertificationDocumentId { get; set; }

        public int AnnualAccountReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }

        public virtual Blob File { get; set; }

        public void SetAttributes(
            string name,
            string description,
            Guid? blobKey)
        {
            this.Name = name;
            this.Description = description;
            this.BlobKey = blobKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportCertificationDocumentMap : EntityTypeConfiguration<AnnualAccountReportCertificationDocument>
    {
        public AnnualAccountReportCertificationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.AnnualAccountReportCertificationDocumentId);

            // Properties
            this.Property(t => t.AnnualAccountReportCertificationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportCertificationDocuments");
            this.Property(t => t.AnnualAccountReportCertificationDocumentId).HasColumnName("AnnualAccountReportCertificationDocumentId");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.AnnualAccountReport)
                .WithMany(t => t.CertificationDocuments)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
