using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportRevalidations.Repositories
{
    public interface IContractReportRevalidationsRepository : IAggregateRepository<ContractReportRevalidation>
    {
        IList<ContractReportRevalidation> FindAllByCertReport(int certReportId);

        IList<ContractReportRevalidationVO> GetContractReportRevalidations(
            int[] programmeIds,
            ContractReportRevalidationType? type = null,
            ContractReportRevalidationStatus? status = null);

        ContractReportRevalidationInfoVO GetInfo(int contractReportRevalidationId);

        ContractReportRevalidationBasicDataVO GetBasicData(int contractReportRevalidationId);

        IList<ContractReportRevalidationDocumentVO> GetDocuments(int contractReportRevalidationId);

        int GetProgrammeId(int contractReportRevalidationId);

        bool HasCertDraftContractReportRevalidations(int certReportId);

        bool HasCertContractReportRevalidations(int certReportId);

        bool IsIncludedInCertReport(int contractReportRevalidationId);

        IList<ContractReportRevalidationVO> GetContractReportRevalidationsForProjectDossier(int contractId);
    }
}
