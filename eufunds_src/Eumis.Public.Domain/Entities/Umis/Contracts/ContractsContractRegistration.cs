using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractsContractRegistration
    {
        public int ContractsContractRegistrationId { get; set; }

        public int ContractRegistrationId { get; set; }

        public int ContractId { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid BlobKey { get; set; }

        public bool IsActive { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Blob File { get; set; }
    }

    public class ContractsContractRegistrationMap : EntityTypeConfiguration<ContractsContractRegistration>
    {
        public ContractsContractRegistrationMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractsContractRegistrationId);

            // Properties
            this.Property(t => t.ContractsContractRegistrationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractRegistrationId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractsContractRegistrations");
            this.Property(t => t.ContractsContractRegistrationId).HasColumnName("ContractsContractRegistrationId");
            this.Property(t => t.ContractRegistrationId).HasColumnName("ContractRegistrationId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractRegistrations)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.File)
                .WithMany()
                .HasForeignKey(t => t.BlobKey);
        }
    }
}
