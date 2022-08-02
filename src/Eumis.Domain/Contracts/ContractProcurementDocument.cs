using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public class ContractProcurementDocument
    {
        public ContractProcurementDocument()
        {
        }

        public int ContractProcurementDocumentId { get; set; }

        public int ContractId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual Contract Contract { get; set; }

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
    public class ContractProcurementDocumentMap : EntityTypeConfiguration<ContractProcurementDocument>
    {
        public ContractProcurementDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractProcurementDocumentId);

            // Properties
            this.Property(t => t.ContractProcurementDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ContractProcurementDocuments");
            this.Property(t => t.ContractProcurementDocumentId).HasColumnName("ContractProcurementDocumentId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractProcurementDocuments)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
