using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportAdvanceNVPaymentAmount : IAggregateRoot
    {
        private ContractReportAdvanceNVPaymentAmount()
        {
        }

        public ContractReportAdvanceNVPaymentAmount(
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

            this.Status = ContractReportAdvanceNVPaymentAmountStatus.Draft;

            this.BgAmount = 0;
            this.EuAmount = 0;
            this.BfpTotalAmount = 0;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportAdvanceNVPaymentAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportAdvanceNVPaymentAmountStatus Status { get; set; }

        public ContractReportAdvanceNVPaymentAmountApproval? Approval { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public int ProgrammePriorityId { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportAdvanceNVPaymentAmountMap : EntityTypeConfiguration<ContractReportAdvanceNVPaymentAmount>
    {
        public ContractReportAdvanceNVPaymentAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportAdvanceNVPaymentAmountId);

            // Properties
            this.Property(t => t.ContractReportAdvanceNVPaymentAmountId)
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
            this.ToTable("ContractReportAdvanceNVPaymentAmounts");
            this.Property(t => t.ContractReportAdvanceNVPaymentAmountId).HasColumnName("ContractReportAdvanceNVPaymentAmountId");
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
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.BfpTotalAmount).HasColumnName("BfpTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
