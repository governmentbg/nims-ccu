using System.Collections.Generic;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Domain.CertAuthorityChecks;

namespace Eumis.Data.CertAuthorityChecks.Repositories
{
    public interface ICertAuthorityChecksRepository : IAggregateRepository<CertAuthorityCheck>
    {
        IList<CertAuthorityCheckVO> GetCertAuthorityCheks(
            CertAuthorityCheckStatus? status = null,
            CertAuthorityCheckType? type = null);

        int GetNextOrderNum(int programmeId);

        CertAuthorityCheckInfoVO GetInfo(int certAuthorityCheckId);

        IList<CertAuthorityCheckAscertainmentVO> GetCertAuthorityCheckAscertainments(int certAuthorityCheckId);

        IList<CertAuthorityCheckProgrammeItemVO> GetProgrammeItems(int certAuthorityCheckId);

        IList<CertAuthorityCheckProgrammeItemVO> GetNotIncludedProgrammes(int certAuthorityCheckId);

        IList<CertAuthorityCheckProgrammePriorityItemVO> GetProgrammePriorityItems(int certAuthorityCheckId);

        IList<CertAuthorityCheckProgrammePriorityItemVO> GetNotIncludedProgrammePriorities(int certAuthorityCheckId);

        IList<CertAuthorityCheckProcedureItemVO> GetProcedureItems(int certAuthorityCheckId);

        IList<CertAuthorityCheckProcedureItemVO> GetNotIncludedProcedures(int certAuthorityCheckId);

        IList<CertAuthorityCheckContractItemVO> GetContractItems(int certAuthorityCheckId);

        IList<CertAuthorityCheckContractItemVO> GetNotIncludedContracts(int certAuthorityCheckId);

        IList<CertAuthorityCheckDocumentVO> GetCertAuthorityCheckDocuments(int certAuthorityCheckId);

        IList<CertAuthorityCheckProjectVO> GetCertAuthorityCheckProjects(int certAuthorityCheckId);

        IList<CertAuthorityCheckProjectVO> GetNotIncludedProjects(int certAuthorityCheckId);
    }
}
