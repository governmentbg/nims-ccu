using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Debts.DataObjects
{
    public class ContractDebtDO
    {
        public ContractDebtDO()
        {
            this.PaymentIds = new List<int>().ToArray();
        }

        public ContractDebtDO(ContractDebt contractDebt, Contract contract, int? certReportNum)
        {
            this.ContractDebtId = contractDebt.ContractDebtId;
            this.ContractId = contractDebt.ContractId;
            this.RegNumber = contractDebt.RegNumber;
            this.RegDate = contractDebt.RegDate;
            this.DebtStartDate = contractDebt.DebtStartDate;
            this.InterestStartDate = contractDebt.InterestStartDate;
            this.ProgrammePriorityId = contractDebt.ProgrammePriorityId;
            this.ExecutionStatus = contractDebt.ExecutionStatus;
            this.IrregularityId = contractDebt.IrregularityId;
            this.FinancialCorrectionId = contractDebt.FinancialCorrectionId;
            this.Comment = contractDebt.Comment;
            this.Status = contractDebt.Status;
            this.CertReportNum = certReportNum;
            this.IsDeleted = contractDebt.Status == ContractDebtStatus.Removed;
            this.IsDeletedNote = contractDebt.IsDeletedNote;

            this.HasContractDebtInterests = contractDebt.ContractDebtInterests.Any();
            this.PaymentIds = contractDebt.ContractDebtPayments.Select(t => t.ContractReportPaymentId).ToArray();

            this.CreateDate = contractDebt.CreateDate;
            this.ModifyDate = contractDebt.ModifyDate;
            this.Version = contractDebt.Version;

            this.Contract = new ContractDataDO(contract);
        }

        public int? ContractDebtId { get; set; }

        public int? ContractId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public string RegNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? DebtStartDate { get; set; }

        public DateTime? InterestStartDate { get; set; }

        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public int? IrregularityId { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public string Comment { get; set; }

        public ContractDebtStatus? Status { get; set; }

        public int? CertReportNum { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractDataDO Contract { get; set; }

        public bool HasContractDebtInterests { get; set; }

        public int[] PaymentIds { get; set; }
    }
}
