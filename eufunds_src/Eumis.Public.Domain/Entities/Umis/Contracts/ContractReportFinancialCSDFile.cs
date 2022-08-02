using System.Data.Entity.ModelConfiguration;
using System;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractReportFinancialCSDFile
    {
        public int ContractReportFinancialCSDFileId { get; set; }

        public int ContractReportFinancialCSDId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public Guid BlobKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ContractReportFinancialCSD ContractReportFinancialCSD { get; set; }
    }

    public class ContractReportFinancialCSDFileMap : EntityTypeConfiguration<ContractReportFinancialCSDFile>
    {
        public ContractReportFinancialCSDFileMap()
            : base()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialCSDFileId);

            // Properties
            this.Property(t => t.ContractReportFinancialCSDId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportFinancialCSDFiles");
            this.Property(t => t.ContractReportFinancialCSDFileId).HasColumnName("ContractReportFinancialCSDFileId");
            this.Property(t => t.ContractReportFinancialCSDId).HasColumnName("ContractReportFinancialCSDId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.ContractReportFinancialCSD)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.ContractReportFinancialCSDId)
                .WillCascadeOnDelete();
        }
    }
}
