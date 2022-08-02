using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportCertCorrections.Repositories
{
    public interface IContractReportCertCorrectionsRepository : IAggregateRepository<ContractReportCertCorrection>
    {
        IList<ContractReportCertCorrectionVO> GetContractReportCertCorrections(
            int[] programmeIds,
            ContractReportCertCorrectionType? type = null,
            ContractReportCertCorrectionStatus? status = null);

        ContractReportCertCorrectionInfoVO GetInfo(int contractReportCertCorrectionId);

        ContractReportCertCorrectionBasicDataVO GetBasicData(int contractReportCertCorrectionId);

        IList<ContractReportCertCorrectionDocumentVO> GetDocuments(int contractReportCertCorrectionId);

        int GetProgrammeId(int contractReportCertCorrectionId);

        bool HasCertContractReportCertCorrections(int certReportId);

        bool IsIncludedInCertReport(int contractReportCertCorrectionId);

        IList<ContractReportCertCorrectionVO> GetContractReportCertCorrectionsForProjectDossier(int contractId);
    }
}
