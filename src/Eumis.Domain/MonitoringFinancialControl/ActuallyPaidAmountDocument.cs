using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public class ActuallyPaidAmountDocument
    {
        public ActuallyPaidAmountDocument()
        {
        }

        public int ActuallyPaidAmountDocumentId { get; set; }

        public int ActuallyPaidAmountId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? BlobKey { get; set; }

        public virtual Blob File { get; set; }

        public virtual ActuallyPaidAmount ActuallyPaidAmount { get; set; }

        internal void SetAttributes(
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
    public class ActuallyPaidAmountDocumentMap : EntityTypeConfiguration<ActuallyPaidAmountDocument>
    {
        public ActuallyPaidAmountDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ActuallyPaidAmountDocumentId);

            // Properties
            this.Property(t => t.ActuallyPaidAmountDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ActuallyPaidAmountDocuments");
            this.Property(t => t.ActuallyPaidAmountDocumentId).HasColumnName("ActuallyPaidAmountDocumentId");
            this.Property(t => t.ActuallyPaidAmountId).HasColumnName("ActuallyPaidAmountId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            // Relationships
            this.HasOptional(t => t.File)
                .WithMany()
                .HasForeignKey(d => d.BlobKey);

            this.HasRequired(t => t.ActuallyPaidAmount)
                .WithMany(t => t.ActuallyPaidAmountDocuments)
                .HasForeignKey(d => d.ActuallyPaidAmountId)
                .WillCascadeOnDelete();
        }
    }
}
