using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public class DebtReimbursedAmount : ReimbursedAmount
    {
        public DebtReimbursedAmount()
        {
        }

        public DebtReimbursedAmount(
            int programmeId,
            int contractId,
            int contractDebtId,
            DateTime reimbursementDate,
            ReimbursementType type,
            Reimbursement reimbursement)
            : base(programmeId, contractId, reimbursementDate, type, reimbursement)
        {
            this.ContractDebtId = contractDebtId;
        }

        public DebtReimbursedAmount(
            int sapFileId,
            int programmeId,
            int contractId,
            int contractDebtId,
            DateTime reimbursementDate,
            ReimbursementType type,
            Reimbursement reimbursement,
            decimal? principalBfpEuAmount,
            decimal? principalBfpBgAmount,
            decimal? interestBfpEuAmount,
            decimal? interestBfpBgAmount,
            string comment)
            : base(programmeId, contractId, reimbursementDate, type, reimbursement)
        {
            this.SapFileId = sapFileId;
            this.ContractDebtId = contractDebtId;

            this.PrincipalBfp.SetAttributes(principalBfpEuAmount, principalBfpBgAmount);
            this.InterestBfp.SetAttributes(interestBfpEuAmount, interestBfpBgAmount);

            this.Comment = comment;
        }

        public int? SapFileId { get; set; }

        public int ContractDebtId { get; set; }

        public override ReimbursedAmountDiscriminator Discriminator
        {
            get
            {
                return ReimbursedAmountDiscriminator.Debt;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class DebtReimbursedAmountMap : EntityTypeConfiguration<DebtReimbursedAmount>
    {
        public DebtReimbursedAmountMap()
        {
            // Properties
            this.Property(t => t.ContractDebtId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.SapFileId).HasColumnName("SapFileId");
            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
        }
    }
}
