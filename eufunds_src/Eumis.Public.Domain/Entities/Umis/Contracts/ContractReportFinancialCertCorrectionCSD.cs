using Eumis.Public.Domain.Entities.Umis.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportFinancialCertCorrectionCSD : IAggregateRoot
    {

        private ContractReportFinancialCertCorrectionCSD()
        {
        }

        public ContractReportFinancialCertCorrectionCSD(
            int contractReportFinancialCertCorrectionId,
            int contractReportFinancialCSDBudgetItemId,
            int contractReportFinancialId,
            int contractReportId,
            int contractId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportFinancialCertCorrectionId = contractReportFinancialCertCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Status = ContractReportFinancialCertCorrectionCSDStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportFinancialCertCorrectionCSDId { get; set; }
        public int ContractReportFinancialCertCorrectionId { get; set; }
        public int ContractReportFinancialCSDBudgetItemId { get; set; }
        public int ContractReportFinancialId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }
        public ContractReportFinancialCertCorrectionCSDStatus Status { get; set; }
        public string Notes { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }

        public decimal? CertifiedEuAmount { get; set; }
        public decimal? CertifiedBgAmount { get; set; }
        public decimal? CertifiedBfpTotalAmount { get; set; }
        public decimal? CertifiedSelfAmount { get; set; }
        public decimal? CertifiedTotalAmount { get; set; }

        public int? CertReportId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ContractReportFinancialCertCorrectionCSDMap : EntityTypeConfiguration<ContractReportFinancialCertCorrectionCSD>
    {
        public ContractReportFinancialCertCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialCertCorrectionCSDId);

            // Properties
            this.Property(t => t.ContractReportFinancialCertCorrectionCSDId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportFinancialCertCorrectionId)
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
            this.ToTable("ContractReportFinancialCertCorrectionCSDs");
            this.Property(t => t.ContractReportFinancialCertCorrectionCSDId).HasColumnName("ContractReportFinancialCertCorrectionCSDId");
            this.Property(t => t.ContractReportFinancialCertCorrectionId).HasColumnName("ContractReportFinancialCertCorrectionId");
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

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
