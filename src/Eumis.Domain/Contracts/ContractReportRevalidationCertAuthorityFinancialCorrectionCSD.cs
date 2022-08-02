using Eumis.Domain.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportRevalidationCertAuthorityFinancialCorrectionCSD : IAggregateRoot
    {
        private ContractReportRevalidationCertAuthorityFinancialCorrectionCSD()
        {
        }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId,
            int contractReportFinancialId,
            int contractReportId,
            int contractId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportRevalidationCertAuthorityFinancialCorrectionId = contractReportRevalidationCertAuthorityFinancialCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Status = ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId { get; set; }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus Status { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? RevalidatedEuAmount { get; set; }

        public decimal? RevalidatedBgAmount { get; set; }

        public decimal? RevalidatedBfpTotalAmount { get; set; }

        public decimal? RevalidatedSelfAmount { get; set; }

        public decimal? RevalidatedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionCSDMap : EntityTypeConfiguration<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            // Properties
            this.Property(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialCSDBudgetItemId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Status)
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
            this.ToTable("ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs");
            this.Property(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId).HasColumnName("ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId");
            this.Property(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionId).HasColumnName("ContractReportRevalidationCertAuthorityFinancialCorrectionId");
            this.Property(t => t.ContractReportFinancialCSDBudgetItemId).HasColumnName("ContractReportFinancialCSDBudgetItemId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.Sign).HasColumnName("Sign");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.RevalidatedEuAmount).HasColumnName("RevalidatedEuAmount");
            this.Property(t => t.RevalidatedBgAmount).HasColumnName("RevalidatedBgAmount");
            this.Property(t => t.RevalidatedBfpTotalAmount).HasColumnName("RevalidatedBfpTotalAmount");
            this.Property(t => t.RevalidatedSelfAmount).HasColumnName("RevalidatedSelfAmount");
            this.Property(t => t.RevalidatedTotalAmount).HasColumnName("RevalidatedTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
