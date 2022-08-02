using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procurements
{
    public class ProcurementDocument
    {
        public ProcurementDocument()
        {
            this.Gid = Guid.NewGuid();
        }

        public int ProcurementDocumentId { get; set; }

        public int ProcurementId { get; set; }

        public string Name { get; set; }

        public Guid Gid { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual Procurement Procurement { get; set; }

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
    public class ProcurementDocumentMap : EntityTypeConfiguration<ProcurementDocument>
    {
        public ProcurementDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcurementDocumentId);

            // Properties
            this.Property(t => t.ProcurementDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcurementDocuments");
            this.Property(t => t.ProcurementDocumentId).HasColumnName("ProcurementDocumentId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ProcurementId).HasColumnName("ProcurementId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Procurement)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ProcurementId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
