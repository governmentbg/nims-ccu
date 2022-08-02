using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportCertAuthorityCorrections.Repositories
{
    public interface IContractReportCertAuthorityCorrectionsRepository : IAggregateRepository<ContractReportCertAuthorityCorrection>
    {
        IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCertAuthorityCorrections(
            int[] programmeIds,
            ContractReportCertAuthorityCorrectionType? type = null,
            ContractReportCertAuthorityCorrectionStatus? status = null);

        ContractReportCertAuthorityCorrectionInfoVO GetInfo(int contractReportCertAuthorityCorrectionId);

        ContractReportCertAuthorityCorrectionBasicDataVO GetBasicData(int contractReportCertAuthorityCorrectionId);

        IList<ContractReportCertAuthorityCorrectionDocumentVO> GetDocuments(int contractReportCertAuthorityCorrectionId);

        int GetProgrammeId(int contractReportCertAuthorityCorrectionId);

        bool IsIncludedInCertReport(int contractReportCertAuthorityCorrectionId);

        IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCertAuthorityCorrectionsForProjectDossier(int contractId);
    }
}
