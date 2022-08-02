using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportAdvanceNVPaymentAmount
{
    public interface IContractReportAdvanceNVPaymentAmountService
    {
        void CreateContractReportAdvanceNVPaymentAmounts(ContractReportPayment payment);

        void DeleteContractReportAdvanceNVPaymentAmounts(ContractReportPayment payment);

        Eumis.Domain.Contracts.ContractReportAdvanceNVPaymentAmount UpdateContractReportAdvanceNVPaymentAmount(
            int contractReportAdvanceNVPaymentAmountId,
            byte[] version,
            ContractReportAdvanceNVPaymentAmountApproval? approval,
            string notes,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? bfpTotalAmount);

        void ChangeContractReportAdvanceNVPaymentAmountStatus(
            int contractReportAdvanceNVPaymentAmountId,
            byte[] version,
            ContractReportAdvanceNVPaymentAmountStatus status);

        IList<string> CanChangeContractReportAdvanceNVPaymentAmountStatusToEnded(int contractReportAdvanceNVPaymentAmountId);

        IList<string> CanChangeContractReportAdvanceNVPaymentAmountStatusToDraft(int contractReportAdvanceNVPaymentAmountId);
    }
}
