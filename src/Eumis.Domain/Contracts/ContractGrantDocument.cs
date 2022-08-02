using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public class ContractGrantDocument
    {
        public ContractGrantDocument()
        {
        }

        public int ContractGrantDocumentId { get; set; }

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
    public class ContractGrantDocumentMap : EntityTypeConfiguration<ContractGrantDocument>
    {
        public ContractGrantDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractGrantDocumentId);

            // Properties
            this.Property(t => t.ContractGrantDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ContractGrantDocuments");
            this.Property(t => t.ContractGrantDocumentId).HasColumnName("ContractGrantDocumentId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractGrantDocuments)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
