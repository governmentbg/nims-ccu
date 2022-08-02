using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public class CertAuthorityCheckDocument
    {
        public CertAuthorityCheckDocument()
        {
        }

        public int CertAuthorityCheckDocumentId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual CertAuthorityCheck CertAuthorityCheck { get; set; }

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

    public class CertAuthorityCheckDocumentMap : EntityTypeConfiguration<CertAuthorityCheckDocument>
    {
        public CertAuthorityCheckDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.CertAuthorityCheckDocumentId);

            // Properties
            this.Property(t => t.CertAuthorityCheckDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("CertAuthorityCheckDocuments");
            this.Property(t => t.CertAuthorityCheckDocumentId).HasColumnName("CertAuthorityCheckDocumentId");
            this.Property(t => t.CertAuthorityCheckId).HasColumnName("CertAuthorityCheckId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.CertAuthorityCheck)
                .WithMany(t => t.CertAuthorityCheckDocuments)
                .HasForeignKey(t => t.CertAuthorityCheckId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
