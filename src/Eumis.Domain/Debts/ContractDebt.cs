using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Debts
{
    public partial class ContractDebt : IAggregateRoot
    {
        private ContractDebt()
        {
            this.ContractDebtInterests = new List<ContractDebtInterest>();
            this.ContractDebtPayments = new List<ContractDebtPayment>();
        }

        public ContractDebt(
            int contractId,
            int programmePriorityId,
            DateTime regDate,
            DateTime debtStartDate,
            DateTime interestStartDate,
            int[] paymentIds)
            : this()
        {
            this.ContractId = contractId;
            this.ProgrammePriorityId = programmePriorityId;
            this.RegDate = regDate;
            this.DebtStartDate = debtStartDate;
            this.InterestStartDate = interestStartDate;
            this.Status = ContractDebtStatus.New;

            foreach (var paymentId in paymentIds)
            {
                this.ContractDebtPayments.Add(
                    new ContractDebtPayment(paymentId));
            }

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractDebtId { get; set; }

        public int ContractId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime DebtStartDate { get; set; }

        public DateTime InterestStartDate { get; set; }

        public ContractDebtStatus Status { get; set; }

        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public int? IrregularityId { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public string Comment { get; set; }

        public int? CertReportId { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<ContractDebtInterest> ContractDebtInterests { get; set; }

        public virtual ICollection<ContractDebtPayment> ContractDebtPayments { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractDebtMap : EntityTypeConfiguration<ContractDebt>
    {
        public ContractDebtMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractDebtId);

            // Properties
            this.Property(t => t.ContractDebtId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();
            this.Property(t => t.RegDate)
                .IsRequired();
            this.Property(t => t.DebtStartDate)
                .IsRequired();
            this.Property(t => t.InterestStartDate)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.Comment)
                .IsOptional();
            this.Property(t => t.IsDeletedNote)
                .IsOptional();

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
            this.ToTable("ContractDebts");
            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.DebtStartDate).HasColumnName("DebtStartDate");
            this.Property(t => t.InterestStartDate).HasColumnName("InterestStartDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ExecutionStatus).HasColumnName("ExecutionStatus");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
