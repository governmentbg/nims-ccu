using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportRevalidationDocument
    {
        public int ContractReportRevalidationDocumentId { get; set; }

        public int ContractReportRevalidationId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public Guid FileKey { get; set; }

        public virtual ContractReportRevalidation ContractReportRevalidation { get; set; }

        internal void SetAttributes(string description, string fileName, Guid fileKey)
        {
            this.Description = description;
            this.FileName = fileName;
            this.FileKey = fileKey;
        }
    }

    public class ContractReportRevalidationDocumentMap : EntityTypeConfiguration<ContractReportRevalidationDocument>
    {
        public ContractReportRevalidationDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportRevalidationDocumentId);

            // Properties
            this.Property(t => t.ContractReportRevalidationDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportRevalidationId)
                .IsRequired();

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(t => t.FileKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportRevalidationDocuments");
            this.Property(t => t.ContractReportRevalidationDocumentId).HasColumnName("ContractReportRevalidationDocumentId");
            this.Property(t => t.ContractReportRevalidationId).HasColumnName("ContractReportRevalidationId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileKey).HasColumnName("FileKey");

            this.HasRequired(t => t.ContractReportRevalidation)
                .WithMany(t => t.Documents)
                .HasForeignKey(t => t.ContractReportRevalidationId)
                .WillCascadeOnDelete();
        }
    }
}
