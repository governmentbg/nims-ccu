using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportPaymentCheckAmount
    {
        private ContractReportPaymentCheckAmount()
        {
        }

        public ContractReportPaymentCheckAmount(
            int contractReportPaymentCheckId,
            int contractReportPaymentId,
            int contractReportId,
            int contractId,
            int programmePriorityId,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount,
            decimal? approvedCrossAmount,
            decimal? approvedSelfAmount,
            decimal? approvedTotalAmount,
            decimal? paidEuAmount,
            decimal? paidBgAmount,
            decimal? paidBfpTotalAmount,
            decimal? paidCrossAmount)
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractReportPaymentCheckId = contractReportPaymentCheckId;
            this.ContractReportPaymentId = contractReportPaymentId;
            this.ContractReportId = contractReportId;
            this.ContractId = contractId;

            this.ProgrammePriorityId = programmePriorityId;

            this.ApprovedEuAmount = approvedEuAmount;
            this.ApprovedBgAmount = approvedBgAmount;
            this.ApprovedBfpTotalAmount = approvedBfpTotalAmount;
            this.ApprovedCrossAmount = approvedCrossAmount;
            this.ApprovedSelfAmount = approvedSelfAmount;
            this.ApprovedTotalAmount = approvedTotalAmount;

            this.SetAttributes(
                paidEuAmount,
                paidBgAmount,
                paidBfpTotalAmount,
                paidCrossAmount);
        }

        public int ContractReportPaymentCheckAmountId { get; set; }

        public int ContractReportPaymentCheckId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int ProgrammePriorityId { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedCrossAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? PaidEuAmount { get; set; }

        public decimal? PaidBgAmount { get; set; }

        public decimal? PaidBfpTotalAmount { get; set; }

        public decimal? PaidCrossAmount { get; set; }

        public virtual ContractReportPaymentCheck ContractReportPaymentCheck { get; set; }

        internal void SetAttributes(
            decimal? paidEuAmount,
            decimal? paidBgAmount,
            decimal? paidBfpTotalAmount,
            decimal? paidCrossAmount)
        {
            this.PaidEuAmount = paidEuAmount;
            this.PaidBgAmount = paidBgAmount;
            this.PaidBfpTotalAmount = paidBfpTotalAmount;
            this.PaidCrossAmount = paidCrossAmount;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportPaymentCheckAmountMap : EntityTypeConfiguration<ContractReportPaymentCheckAmount>
    {
        public ContractReportPaymentCheckAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportPaymentCheckAmountId);

            // Properties
            this.Property(t => t.ContractReportPaymentCheckAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportPaymentCheckAmounts");
            this.Property(t => t.ContractReportPaymentCheckAmountId).HasColumnName("ContractReportPaymentCheckAmountId");
            this.Property(t => t.ContractReportPaymentCheckId).HasColumnName("ContractReportPaymentCheckId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");

            this.Property(t => t.ApprovedEuAmount).HasColumnName("ApprovedEuAmount");
            this.Property(t => t.ApprovedBgAmount).HasColumnName("ApprovedBgAmount");
            this.Property(t => t.ApprovedBfpTotalAmount).HasColumnName("ApprovedBfpTotalAmount");
            this.Property(t => t.ApprovedCrossAmount).HasColumnName("ApprovedCrossAmount");
            this.Property(t => t.ApprovedSelfAmount).HasColumnName("ApprovedSelfAmount");
            this.Property(t => t.ApprovedTotalAmount).HasColumnName("ApprovedTotalAmount");

            this.Property(t => t.PaidEuAmount).HasColumnName("PaidEuAmount");
            this.Property(t => t.PaidBgAmount).HasColumnName("PaidBgAmount");
            this.Property(t => t.PaidBfpTotalAmount).HasColumnName("PaidBfpTotalAmount");
            this.Property(t => t.PaidCrossAmount).HasColumnName("PaidCrossAmount");

            // Relationships
            this.HasRequired(t => t.ContractReportPaymentCheck)
                .WithMany(t => t.ContractReportPaymentCheckAmounts)
                .HasForeignKey(d => d.ContractReportPaymentCheckId)
                .WillCascadeOnDelete();
        }
    }
}
