using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportRevalidationCertAuthorityCorrections.Repositories
{
    public interface IContractReportRevalidationCertAuthorityCorrectionsRepository : IAggregateRepository<ContractReportRevalidationCertAuthorityCorrection>
    {
        IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetContractReportRevalidationCertAuthorityCorrections(
            int[] programmeIds,
            ContractReportRevalidationCertAuthorityCorrectionType? type = null,
            ContractReportRevalidationCertAuthorityCorrectionStatus? status = null);

        ContractReportRevalidationCertAuthorityCorrectionInfoVO GetInfo(int contractReportRevalidationCertAuthorityCorrectionId);

        ContractReportRevalidationCertAuthorityCorrectionBasicDataVO GetBasicData(int contractReportRevalidationCertAuthorityCorrectionId);

        IList<ContractReportRevalidationCertAuthorityCorrectionDocumentVO> GetDocuments(int contractReportRevalidationCertAuthorityCorrectionId);

        int GetProgrammeId(int contractReportRevalidationCertAuthorityCorrectionId);

        bool IsIncludedInCertReport(int contractReportRevalidationCertAuthorityCorrectionId);
    }
}
