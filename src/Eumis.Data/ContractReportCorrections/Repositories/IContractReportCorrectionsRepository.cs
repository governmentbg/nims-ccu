using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportCorrections.Repositories
{
    public interface IContractReportCorrectionsRepository : IAggregateRepository<ContractReportCorrection>
    {
        IList<ContractReportCorrection> FindAllByCertReport(int certReportId);

        IList<ContractReportCorrectionVO> GetContractReportCorrections(
            int[] programmeIds,
            int userId,
            ContractReportCorrectionType? type = null,
            ContractReportCorrectionStatus? status = null);

        ContractReportCorrectionInfoVO GetInfo(int contractReportCorrectionId);

        ContractReportCorrectionBasicDataVO GetBasicData(int contractReportCorrectionId);

        IList<ContractReportCorrectionDocumentVO> GetDocuments(int contractReportCorrectionId);

        int GetProgrammeId(int contractReportCorrectionId);

        bool HasCertDraftContractReportCorrections(int certReportId);

        bool HasCertContractReportCorrections(int certReportId);

        bool IsIncludedInCertReport(int contractReportCorrectionId);

        IList<ContractReportCorrectionVO> GetContractReportCorrectionsForProjectDossier(int contractId);

        IList<ContractReportCertifiedAmountCorrectionVO> GetContractReportCertifiedAmountCorrectionsForProjectDossier(int contractId);

        int? GetContractId(int contractReportCorrectionId);
    }
}
