using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Debts
{
    public partial class ContractDebtVersion : IAggregateRoot
    {
        private ContractDebtVersion()
        {
        }

        public ContractDebtVersion(int contractDebtId, int createdByUserId)
            : this()
        {
            this.ContractDebtId = contractDebtId;
            this.OrderNum = 1;
            this.Status = ContractDebtVersionStatus.Draft;
            this.CreatedByUserId = createdByUserId;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public ContractDebtVersion(ContractDebtVersion previousVersion, int createdByUserId)
            : this()
        {
            this.ContractDebtId = previousVersion.ContractDebtId;
            this.OrderNum = previousVersion.OrderNum + 1;
            this.Status = ContractDebtVersionStatus.Draft;
            this.EuAmount = previousVersion.EuAmount;
            this.BgAmount = previousVersion.BgAmount;
            this.TotalAmount = previousVersion.TotalAmount;
            this.CertStatus = previousVersion.CertStatus;
            this.CertBgAmount = previousVersion.CertBgAmount;
            this.CertEuAmount = previousVersion.CertEuAmount;
            this.CertTotalAmount = previousVersion.CertTotalAmount;
            this.CreatedByUserId = createdByUserId;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int ContractDebtVersionId { get; set; }

        public int ContractDebtId { get; set; }

        public int OrderNum { get; set; }

        public ContractDebtVersionStatus Status { get; set; }

        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public DateTime? ActivationDate { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public ContractDebtCertStatus? CertStatus { get; set; }

        public decimal? CertEuAmount { get; set; }

        public decimal? CertBgAmount { get; set; }

        public decimal? CertTotalAmount { get; set; }

        public string CreateNotes { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractDebtVersionMap : EntityTypeConfiguration<ContractDebtVersion>
    {
        public ContractDebtVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractDebtVersionId);

            // Properties
            this.Property(t => t.ContractDebtVersionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractDebtId)
                .IsRequired();
            this.Property(t => t.OrderNum)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.EuAmount)
                .IsOptional();
            this.Property(t => t.BgAmount)
                .IsOptional();
            this.Property(t => t.TotalAmount)
                .IsOptional();
            this.Property(t => t.CertStatus)
                .IsOptional();
            this.Property(t => t.CertEuAmount)
               .IsOptional();
            this.Property(t => t.CertBgAmount)
                .IsOptional();
            this.Property(t => t.CertTotalAmount)
                .IsOptional();
            this.Property(t => t.CreateNotes)
                .IsOptional();
            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();
            this.Property(t => t.ModifyDate)
                .IsRequired();
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ContractDebtVersions");
            this.Property(t => t.ContractDebtVersionId).HasColumnName("ContractDebtVersionId");
            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ExecutionStatus).HasColumnName("ExecutionStatus");
            this.Property(t => t.ActivationDate).HasColumnName("ActivationDate");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.CertStatus).HasColumnName("CertStatus");
            this.Property(t => t.CertEuAmount).HasColumnName("CertEuAmount");
            this.Property(t => t.CertBgAmount).HasColumnName("CertBgAmount");
            this.Property(t => t.CertTotalAmount).HasColumnName("CertTotalAmount");
            this.Property(t => t.CreateNotes).HasColumnName("CreateNotes");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
