using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.CompensationDocuments
{
    public partial class CompensationDocumentDoc
    {
        public int CompensationDocumentDocId { get; set; }

        public int CompensationDocumentId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual CompensationDocument CompensationDocument { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CompensationDocumentDocMap : EntityTypeConfiguration<CompensationDocumentDoc>
    {
        public CompensationDocumentDocMap()
        {
            // Primary Key
            this.HasKey(t => t.CompensationDocumentDocId);

            // Properties
            this.Property(t => t.CompensationDocumentDocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CompensationDocumentId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CompensationDocumentDocs");
            this.Property(t => t.CompensationDocumentDocId).HasColumnName("CompensationDocumentDocId");
            this.Property(t => t.CompensationDocumentId).HasColumnName("CompensationDocumentId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.CompensationDocument)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.CompensationDocumentId)
                .WillCascadeOnDelete();
        }
    }
}
