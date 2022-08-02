using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractProcurementPlanPublicDocument
    {
        private ContractProcurementPlanPublicDocument()
        {
        }

        public ContractProcurementPlanPublicDocument(
            Guid blobKey,
            string name,
            string description)
        {
            this.BlobKey = blobKey;
            this.Name = name;
            this.Description = description;
        }

        public int ContractProcurementPlanPublicDocumentId { get; set; }
        public int ContractProcurementPlanId { get; set; }

        public Guid BlobKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ContractProcurementPlan ContractProcurementPlan { get; set; }
    }

    public class ContractProcurementPlanPublicDocumentMap : EntityTypeConfiguration<ContractProcurementPlanPublicDocument>
    {
        public ContractProcurementPlanPublicDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractProcurementPlanPublicDocumentId);

            // Properties
            this.Property(t => t.ContractProcurementPlanPublicDocumentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BlobKey)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.Description)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ContractProcurementPlanPublicDocuments");
            this.Property(t => t.ContractProcurementPlanPublicDocumentId).HasColumnName("ContractProcurementPlanPublicDocumentId");
            this.Property(t => t.ContractProcurementPlanId).HasColumnName("ContractProcurementPlanId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.ContractProcurementPlan)
                .WithMany(t => t.PublicDocuments)
                .HasForeignKey(t => t.ContractProcurementPlanId)
                .WillCascadeOnDelete();
        }
    }
}
