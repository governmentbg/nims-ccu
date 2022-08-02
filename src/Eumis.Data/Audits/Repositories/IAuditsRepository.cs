using System.Collections.Generic;
using Eumis.Data.Audits.ViewObjects;
using Eumis.Domain.Audits;

namespace Eumis.Data.Audits.Repositories
{
    public interface IAuditsRepository : IAggregateRepository<Audit>
    {
        IList<AuditVO> GetAudits(int[] programmeIds, AuditLevel? level = null);

        int GetNextOrderNum(int auditId);

        AuditInfoVO GetInfo(int auditId);

        AuditBasicDataVO GetBasicData(int auditId);

        IList<AuditAscertainmentVO> GetAscertainments(int auditId);

        IList<AuditDocVO> GetDocuments(int auditId);

        IList<AuditProgrammePriorityItemVO> GetProgrammePriorityItems(int auditId);

        IList<AuditProgrammePriorityItemVO> GetNotIncludedProgrammePriorities(int auditId);

        IList<AuditProcedureItemVO> GetProcedureItems(int auditId);

        IList<AuditProcedureItemVO> GetNotIncludedProcedures(int auditId);

        IList<AuditContractItemVO> GetContractItems(int auditId);

        IList<AuditContractItemVO> GetNotIncludedContracts(int auditId);

        IList<AuditContractContractItemVO> GetContractContractItems(int auditId);

        IList<AuditContractContractItemVO> GetNotIncludedContractContracts(int auditId);

        int GetProgrammeId(int auditId);

        IList<InternalЕnvironmentAuditVO> GetInternalЕnvironmentAuditsForProjectDossier(int contractId);

        IList<AuditProjectVO> GetNotIncludedProjects(int auditId);

        int? GetContractId(int auditId);
    }
}
