using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ActuallyPaidAmount
{
    public interface IActuallyPaidAmountService
    {
        bool CanCreate(int userId, int contractId, int? contractReportPaymentId);

        IList<string> CanCreate(string contractRegNum, int userId);

        bool CanAssignContractReportPaymentId(int contractId, int contractReportPaymentId);

        void Delete(int actuallyPaidAmountId, byte[] version);

        IList<string> CanChangeStatusToEntered(Eumis.Domain.MonitoringFinancialControl.ActuallyPaidAmount paidAmount);

        void ChangeStatusToEntered(Eumis.Domain.MonitoringFinancialControl.ActuallyPaidAmount paidAmount);
    }
}
