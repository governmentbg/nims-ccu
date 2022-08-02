using Eumis.Domain.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCertAuthorityFinancialCorrectionCSD : IAggregateRoot
    {
        private ContractReportCertAuthorityFinancialCorrectionCSD()
        {
        }

        public ContractReportCertAuthorityFinancialCorrectionCSD(
            int contractReportCertAuthorityFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId,
            int contractReportFinancialId,
            int contractReportId,
            int contractId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportCertAuthorityFinancialCorrectionId = contractReportCertAuthorityFinancialCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Status = ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportCertAuthorityFinancialCorrectionCSDId { get; set; }

        public int ContractReportCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportCertAuthorityFinancialCorrectionCSDStatus Status { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportCertAuthorityFinancialCorrectionCSDMap : EntityTypeConfiguration<ContractReportCertAuthorityFinancialCorrectionCSD>
    {
        public ContractReportCertAuthorityFinancialCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportCertAuthorityFinancialCorrectionCSDId);

            // Properties
            this.Property(t => t.ContractReportCertAuthorityFinancialCorrectionCSDId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportCertAuthorityFinancialCorrectionId)
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
            this.ToTable("ContractReportCertAuthorityFinancialCorrectionCSDs");
            this.Property(t => t.ContractReportCertAuthorityFinancialCorrectionCSDId).HasColumnName("ContractReportCertAuthorityFinancialCorrectionCSDId");
            this.Property(t => t.ContractReportCertAuthorityFinancialCorrectionId).HasColumnName("ContractReportCertAuthorityFinancialCorrectionId");
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

            this.Property(t => t.CertifiedEuAmount).HasColumnName("CertifiedEuAmount");
            this.Property(t => t.CertifiedBgAmount).HasColumnName("CertifiedBgAmount");
            this.Property(t => t.CertifiedBfpTotalAmount).HasColumnName("CertifiedBfpTotalAmount");
            this.Property(t => t.CertifiedSelfAmount).HasColumnName("CertifiedSelfAmount");
            this.Property(t => t.CertifiedTotalAmount).HasColumnName("CertifiedTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
