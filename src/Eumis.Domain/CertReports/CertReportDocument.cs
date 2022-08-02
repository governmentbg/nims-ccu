using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertReports
{
    public class CertReportDocument
    {
        public CertReportDocument()
        {
        }

        public int CertReportDocumentId { get; set; }

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
    public class CertReportDocumentMap : EntityTypeConfiguration<CertReportDocument>
    {
        public CertReportDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.CertReportDocumentId);

            // Properties
            this.Property(t => t.CertReportDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("CertReportDocuments");
            this.Property(t => t.CertReportDocumentId).HasColumnName("CertReportDocumentId");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.CertReport)
                .WithMany(t => t.CertReportDocuments)
                .HasForeignKey(t => t.CertReportId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
