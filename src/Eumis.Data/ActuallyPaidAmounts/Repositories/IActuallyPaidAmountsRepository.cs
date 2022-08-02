using System.Collections.Generic;
using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl;

namespace Eumis.Data.ActuallyPaidAmounts.Repositories
{
    public interface IActuallyPaidAmountsRepository : IAggregateRepository<ActuallyPaidAmount>
    {
        IList<ActuallyPaidAmountVO> GetActuallyPaidAmounts(int[] programmeIds, int userId, int? contractId = null, PaymentReason? paymentReason = null);

        ActuallyPaidAmountInfoVO GetInfo(int paidAmountId);

        ActuallyPaidAmountBasicDataVO GetBasicData(int paidAmountId);

        int GetProgrammeId(int paidAmountId);

        int GetContractId(int paidAmountId);

        IList<ActuallyPaidAmountVO> GetActuallyPaidAmountsForProjectDossier(int contractId);

        IList<ActuallyPaidAmountDocumentVO> GetActuallyPaidAmountDocuments(int paymentAmountId);
    }
}
