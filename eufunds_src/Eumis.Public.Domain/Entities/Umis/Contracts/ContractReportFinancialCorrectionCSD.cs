using Eumis.Public.Domain.Entities.Umis.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportFinancialCorrectionCSD : IAggregateRoot
    {

        private ContractReportFinancialCorrectionCSD()
        {
        }

        public ContractReportFinancialCorrectionCSD(
            int contractReportFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId,
            int contractReportFinancialId,
            int contractReportId,
            int contractId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportFinancialCorrectionId = contractReportFinancialCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.Status = ContractReportFinancialCorrectionCSDStatus.Draft;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportFinancialCorrectionCSDId { get; set; }
        public int ContractReportFinancialCorrectionId { get; set; }
        public int ContractReportFinancialCSDBudgetItemId { get; set; }
        public int ContractReportFinancialId { get; set; }
        public int ContractReportId { get; set; }
        public int ContractId { get; set; }
        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }
        public ContractReportFinancialCorrectionCSDStatus Status { get; set; }
        public string Notes { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }

        public decimal? CorrectedUnapprovedEuAmount { get; set; }
        public decimal? CorrectedUnapprovedBgAmount { get; set; }
        public decimal? CorrectedUnapprovedBfpTotalAmount { get; set; }
        public decimal? CorrectedUnapprovedSelfAmount { get; set; }
        public decimal? CorrectedUnapprovedTotalAmount { get; set; }

        public decimal? CorrectedUnapprovedByCorrectionEuAmount { get; set; }
        public decimal? CorrectedUnapprovedByCorrectionBgAmount { get; set; }
        public decimal? CorrectedUnapprovedByCorrectionBfpTotalAmount { get; set; }
        public decimal? CorrectedUnapprovedByCorrectionSelfAmount { get; set; }
        public decimal? CorrectedUnapprovedByCorrectionTotalAmount { get; set; }

        public CorrectionType? CorrectionType { get; set; }
        public int? FinancialCorrectionId { get; set; }
        public int? IrregularityId { get; set; }

        public decimal? CorrectedApprovedEuAmount { get; set; }
        public decimal? CorrectedApprovedBgAmount { get; set; }
        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }
        public decimal? CorrectedApprovedSelfAmount { get; set; }
        public decimal? CorrectedApprovedTotalAmount { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportFinancialCorrectionCSDCertStatus? CertStatus { get; set; }
        public int? CertCheckedByUserId { get; set; }
        public DateTime? CertCheckedDate { get; set; }
        public decimal? UncertifiedCorrectedApprovedEuAmount { get; set; }
        public decimal? UncertifiedCorrectedApprovedBgAmount { get; set; }
        public decimal? UncertifiedCorrectedApprovedBfpTotalAmount { get; set; }
        public decimal? UncertifiedCorrectedApprovedSelfAmount { get; set; }
        public decimal? UncertifiedCorrectedApprovedTotalAmount { get; set; }

        public decimal? CertifiedCorrectedApprovedEuAmount { get; set; }
        public decimal? CertifiedCorrectedApprovedBgAmount { get; set; }
        public decimal? CertifiedCorrectedApprovedBfpTotalAmount { get; set; }
        public decimal? CertifiedCorrectedApprovedSelfAmount { get; set; }
        public decimal? CertifiedCorrectedApprovedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ContractReportFinancialCorrectionCSDMap : EntityTypeConfiguration<ContractReportFinancialCorrectionCSD>
    {
        public ContractReportFinancialCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialCorrectionCSDId);

            // Properties
            this.Property(t => t.ContractReportFinancialCorrectionCSDId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportFinancialCorrectionId)
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
            this.ToTable("ContractReportFinancialCorrectionCSDs");
            this.Property(t => t.ContractReportFinancialCorrectionCSDId).HasColumnName("ContractReportFinancialCorrectionCSDId");
            this.Property(t => t.ContractReportFinancialCorrectionId).HasColumnName("ContractReportFinancialCorrectionId");
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

            this.Property(t => t.CorrectedUnapprovedEuAmount).HasColumnName("CorrectedUnapprovedEuAmount");
            this.Property(t => t.CorrectedUnapprovedBgAmount).HasColumnName("CorrectedUnapprovedBgAmount");
            this.Property(t => t.CorrectedUnapprovedBfpTotalAmount).HasColumnName("CorrectedUnapprovedBfpTotalAmount");
            this.Property(t => t.CorrectedUnapprovedSelfAmount).HasColumnName("CorrectedUnapprovedSelfAmount");
            this.Property(t => t.CorrectedUnapprovedTotalAmount).HasColumnName("CorrectedUnapprovedTotalAmount");

            this.Property(t => t.CorrectedUnapprovedByCorrectionEuAmount).HasColumnName("CorrectedUnapprovedByCorrectionEuAmount");
            this.Property(t => t.CorrectedUnapprovedByCorrectionBgAmount).HasColumnName("CorrectedUnapprovedByCorrectionBgAmount");
            this.Property(t => t.CorrectedUnapprovedByCorrectionBfpTotalAmount).HasColumnName("CorrectedUnapprovedByCorrectionBfpTotalAmount");
            this.Property(t => t.CorrectedUnapprovedByCorrectionSelfAmount).HasColumnName("CorrectedUnapprovedByCorrectionSelfAmount");
            this.Property(t => t.CorrectedUnapprovedByCorrectionTotalAmount).HasColumnName("CorrectedUnapprovedByCorrectionTotalAmount");
            
            this.Property(t => t.CorrectionType).HasColumnName("CorrectionType");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");

            this.Property(t => t.CorrectedApprovedEuAmount).HasColumnName("CorrectedApprovedEuAmount");
            this.Property(t => t.CorrectedApprovedBgAmount).HasColumnName("CorrectedApprovedBgAmount");
            this.Property(t => t.CorrectedApprovedBfpTotalAmount).HasColumnName("CorrectedApprovedBfpTotalAmount");
            this.Property(t => t.CorrectedApprovedSelfAmount).HasColumnName("CorrectedApprovedSelfAmount");
            this.Property(t => t.CorrectedApprovedTotalAmount).HasColumnName("CorrectedApprovedTotalAmount");

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.CertStatus).HasColumnName("CertStatus");
            this.Property(t => t.CertCheckedByUserId).HasColumnName("CertCheckedByUserId");
            this.Property(t => t.CertCheckedDate).HasColumnName("CertCheckedDate");
            this.Property(t => t.UncertifiedCorrectedApprovedEuAmount).HasColumnName("UncertifiedCorrectedApprovedEuAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedBgAmount).HasColumnName("UncertifiedCorrectedApprovedBgAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedBfpTotalAmount).HasColumnName("UncertifiedCorrectedApprovedBfpTotalAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedSelfAmount).HasColumnName("UncertifiedCorrectedApprovedSelfAmount");
            this.Property(t => t.UncertifiedCorrectedApprovedTotalAmount).HasColumnName("UncertifiedCorrectedApprovedTotalAmount");

            this.Property(t => t.CertifiedCorrectedApprovedEuAmount).HasColumnName("CertifiedCorrectedApprovedEuAmount");
            this.Property(t => t.CertifiedCorrectedApprovedBgAmount).HasColumnName("CertifiedCorrectedApprovedBgAmount");
            this.Property(t => t.CertifiedCorrectedApprovedBfpTotalAmount).HasColumnName("CertifiedCorrectedApprovedBfpTotalAmount");
            this.Property(t => t.CertifiedCorrectedApprovedSelfAmount).HasColumnName("CertifiedCorrectedApprovedSelfAmount");
            this.Property(t => t.CertifiedCorrectedApprovedTotalAmount).HasColumnName("CertifiedCorrectedApprovedTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
