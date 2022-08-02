using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public class ContractReimbursedAmount : ReimbursedAmount
    {
        public ContractReimbursedAmount()
        {
            this.ContractReimbursedAmountPayments = new List<ContractReimbursedAmountPayment>();
        }

        public ContractReimbursedAmount(
            int programmeId,
            int contractId,
            DateTime reimbursementDate,
            ReimbursementType type,
            Reimbursement reimbursement,
            int programmePriorityId)
            : base(programmeId, contractId, reimbursementDate, type, reimbursement)
        {
            this.ProgrammePriorityId = programmePriorityId;
        }

        public override ReimbursedAmountDiscriminator Discriminator
        {
            get
            {
                return ReimbursedAmountDiscriminator.Contract;
            }
        }

        public virtual ICollection<ContractReimbursedAmountPayment> ContractReimbursedAmountPayments { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReimbursedAmountMap : EntityTypeConfiguration<ContractReimbursedAmount>
    {
        public ContractReimbursedAmountMap()
        {
        }
    }
}
