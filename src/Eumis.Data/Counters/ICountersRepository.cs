using Eumis.Domain.EvalSessions;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Counters
{
    public interface ICountersRepository
    {
        void CreateProjectCounter(int procedureId);

        string GetNextProjectNumber(int procedureId);

        void CreateRequestPackageCounter();

        string GetNextRequestPackageNumber();

        void CreateEvalSessionCounter(int procedureId);

        string GetNextEvalSessionNumber(int procedureId);

        void CreateEvalSessionDistributionCounter(int evalSessionId);

        string GetNextEvalSessionDistributionNumber(int evalSessionId);

        void CreateEvalSessionReportCounter(int evalSessionId);

        string GetNextEvalSessionReportNumber(int evalSessionId, EvalSessionReportType type);

        void CreateProjectCommunicationCounter(int projectId);

        string GetNextProjectCommunicationNumber(int projectId);

        void CreateProjectManagingAuthorityCommunicationCounter(int projectId);

        string GetNextProjectManagingAuthorityCommunicationNumber(int projectId);

        void CreateEvalSessionStandingCounter(int evalSessionId);

        string GetNextEvalSessionStandingNumber(int evalSessionId);

        void CreateContractCommunicationCounter(int contractId);

        string GetNextContractCommunicationNumber(int contractId);

        void CreateIrregularitySignalCounter(int programmeId);

        int GetNextIrregularitySignalNumber(int programmeId);

        int GetCurrentIrregularitySignalNumber(int programmeId);

        void DecrementCurrentIrregularitySignalNumber(int programmeId);

        void CreateIrregularityCounter(int programmeId);

        void CreateContractDebtCounter(int contractId);

        string GetNextContractDebtNumber(int contractId);

        void CreateCorrectionDebtCounter(int flatFinancialCorrectionId);

        string GetNextCorrectionDebtNumber(int flatFinancialCorrectionId);

        void CreateActuallyPaidAmountCounter(int contractId);

        string GetNextActuallyPaidAmountNumber(int contractId);

        string[] GetNextNActuallyPaidAmountNumbers(int contractId, int n);

        void CreateCompensationDocumentCounter(int contractId);

        string GetNextCompensationDocumentNumber(int contractId);

        void CreateDebtReimbursedAmountCounter(int contractDebtId);

        string GetNextDebtReimbursedAmountNumber(int contractDebtId);

        string[] GetNextNDebtReimbursedAmountNumbers(int contractDebtId, int n);

        void CreateContractReimbursedAmountCounter(int contractId);

        string GetNextContractReimbursedAmountNumber(int contractId);

        void CreateCertAuthorityCheckCounter();

        int GetNextCertAuthorityCheckNumber();

        void CreateContractReportCorrectionCounter(int programmeId);

        string GetNextContractReportCorrectionNumber(int programmeId);

        void CreateContractReportRevalidationCounter(int programmeId);

        string GetNextContractReportRevalidationNumber(int programmeId);

        void CreateContractReportCertCorrectionCounter(int programmeId);

        string GetNextContractReportCertCorrectionNumber(int programmeId);

        void CreateContractReportCertAuthorityCorrectionCounter(int programmeId);

        string GetNextContractReportCertAuthorityCorrectionNumber(int programmeId);

        void CreateContractReportRevalidationCertAuthorityCorrectionCounter(int programmeId);

        string GetNextContractReportRevalidationCertAuthorityCorrectionNumber(int programmeId);

        void CreateFIReimbursedAmountCounter(int contractId);

        string GetNextFIReimbursedAmountNumber(int contractId);
    }
}
