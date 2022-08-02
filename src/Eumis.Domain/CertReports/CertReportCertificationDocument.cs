using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertReports
{
    public class CertReportCertificationDocument
    {
        public CertReportCertificationDocument()
        {
        }

        public int CertReportCertificationDocumentId { get; set; }

        public int CertReportId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual CertReport CertReport { get; set; }

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
    public class CertReportCertificationDocumentMap : EntityTypeConfiguration<CertReportCertificationDocument>
    {
        public CertReportCertificationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.CertReportCertificationDocumentId);

            // Properties
            this.Property(t => t.CertReportCertificationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("CertReportCertificationDocuments");
            this.Property(t => t.CertReportCertificationDocumentId).HasColumnName("CertReportCertificationDocumentId");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.CertReport)
                .WithMany(t => t.CertReportCertificationDocuments)
                .HasForeignKey(t => t.CertReportId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
