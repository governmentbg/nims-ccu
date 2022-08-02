using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportAdvancePaymentAmount : IAggregateRoot
    {
        private ContractReportAdvancePaymentAmount()
        {
        }

        public ContractReportAdvancePaymentAmount(
            int contractReportPaymentId,
            int contractReportId,
            int contractId,
            int programmePriorityId)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportPaymentId = contractReportPaymentId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;
            this.ProgrammePriorityId = programmePriorityId;

            this.Status = ContractReportAdvancePaymentAmountStatus.Draft;

            this.ApprovedBgAmount = 0;
            this.ApprovedEuAmount = 0;
            this.ApprovedBfpTotalAmount = 0;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportAdvancePaymentAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportAdvancePaymentAmountStatus Status { get; set; }

        public ContractReportAdvancePaymentAmountApproval? Approval { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public int ProgrammePriorityId { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportAdvancePaymentAmountCertStatus? CertStatus { get; set; }

        public int? CertCheckedByUserId { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        public decimal? UncertifiedApprovedEuAmount { get; set; }

        public decimal? UncertifiedApprovedBgAmount { get; set; }

        public decimal? UncertifiedApprovedBfpTotalAmount { get; set; }

        public decimal? CertifiedApprovedEuAmount { get; set; }

        public decimal? CertifiedApprovedBgAmount { get; set; }

        public decimal? CertifiedApprovedBfpTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportAdvancePaymentAmountMap : EntityTypeConfiguration<ContractReportAdvancePaymentAmount>
    {
        public ContractReportAdvancePaymentAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportAdvancePaymentAmountId);

            // Properties
            this.Property(t => t.ContractReportAdvancePaymentAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.ProgrammePriorityId)
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
            this.ToTable("ContractReportAdvancePaymentAmounts");
            this.Property(t => t.ContractReportAdvancePaymentAmountId).HasColumnName("ContractReportAdvancePaymentAmountId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Approval).HasColumnName("Approval");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ApprovedEuAmount).HasColumnName("ApprovedEuAmount");
            this.Property(t => t.ApprovedBgAmount).HasColumnName("ApprovedBgAmount");
            this.Property(t => t.ApprovedBfpTotalAmount).HasColumnName("ApprovedBfpTotalAmount");

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.CertStatus).HasColumnName("CertStatus");
            this.Property(t => t.CertCheckedByUserId).HasColumnName("CertCheckedByUserId");
            this.Property(t => t.CertCheckedDate).HasColumnName("CertCheckedDate");
            this.Property(t => t.UncertifiedApprovedEuAmount).HasColumnName("UncertifiedApprovedEuAmount");
            this.Property(t => t.UncertifiedApprovedBgAmount).HasColumnName("UncertifiedApprovedBgAmount");
            this.Property(t => t.UncertifiedApprovedBfpTotalAmount).HasColumnName("UncertifiedApprovedBfpTotalAmount");

            this.Property(t => t.CertifiedApprovedEuAmount).HasColumnName("CertifiedApprovedEuAmount");
            this.Property(t => t.CertifiedApprovedBgAmount).HasColumnName("CertifiedApprovedBgAmount");
            this.Property(t => t.CertifiedApprovedBfpTotalAmount).HasColumnName("CertifiedApprovedBfpTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
