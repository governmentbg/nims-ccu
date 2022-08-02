using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProceduresRepository : IAggregateRepository<Procedure>
    {
        Procedure FindByCode(string code);

        IList<ProcedureProgrammeTreeVO> GetProcedureProgrammesTree();

        IList<ProcedureVO> GetProcedures(int[] programmeIds, int? programmeId = null, int? programmePriorityId = null);

        IList<ProcedureIndicatorsVO> GetProcedureIndicators(int procedureId);

        bool HasAvailableIndicatorsForAttach(int procedureId);

        bool HasProceduresWithIndicator(int indicatorId);

        IList<ProcedureSharesVO> GetProcedureShares(int procedureId);

        IList<ProcedureLocationsVO> GetProcedureLocations(int procedureId);

        IList<ProcedureTimeLimitsVO> GetProcedureTimeLimits(int procedureId);

        DateTime GetProcedureCurrentEndDate(int procedureId);

        bool IsValidProcedureTimeLimitEndTime(int procedureId, DateTime endDateTime, int? procedureTimeLimitId = null);

        bool IsProcedureInTimeLimit(int procedureId);

        IList<int> GetProceduresPassedTimeLimit();

        Guid GetGid(int procedureId);

        int GetId(Guid procedureGid);

        ProcedureBudgetTreeVO GetExpenseBudgetTree(int procedureId);

        byte[] GetProcedureVersion(int procedureId);

        int GetLastProcedureNumber(int programmePriorityId, int year);

        IList<ProcedureAppGuidelinesVO> GetProcedureAppGuidelines(int procedureId);

        IList<ProcedureSpecFieldVO> GetProcedureSpecFields(int procedureId);

        IList<ProcedureDocumentsVO> GetProcedureDocuments(int procedureId);

        IList<ProcedureProgrammeTreePVO> GetPortalActiveProcedureProgrammesTree();

        IList<ProcedureProgrammeTreePVO> GetPortalEndedProcedureProgrammesTree();

        IList<ProcedureProgrammeTreePVO> GetPortalPublicDiscussionProcedureProgrammesTree();

        IList<ProcedureProgrammeTreePVO> GetPortalArchivedPublicDiscussionProcedureProgrammesTree();

        IList<ProcedureAppDocsVO> GetProcedureAppDocs(int procedureId);

        IList<ProcedureContractReportDocumentVO> GetProcedureContractReportDocuments(int procedureId, ProcedureContractReportDocumentType type);

        int GetPrimaryProcedureProgrammeId(int procedureId);

        int GetPrimaryProcedureProgrammePriorityId(int procedureId);

        (int ProgrammeId, int ProgrammePriorityId) GetProcedureParentData(int procedureId);

        int[] GetProcedureProgrammeIds(int procedureId);

        ProcedureStatus GetProcedureStatus(int procedureId);

        int GetProcedureIdByProcedureCode(string code);

        ProcedureInfoVO GetProcedureInfo(int procedureId);

        ProcedureBasicDataVO GetProcedureBasicData(int procedureId);

        IList<ProcedureEvalTablesVO> GetProcedureEvalTables(int procedureId);

        bool HasPreliminaryEvalTable(int procedureId);

        IList<ProcedureQuestionsVO> GetProcedureQuestions(int procedureId);

        ProcedureEvalTable GetProcedureEvalTable(int procedureId, ProcedureEvalTableType evalTableType);

        IList<BudgetLevel2EuPercentPVO> GetBudgetLevel2EuPercent(Guid procedureGid, string programmeCode);

        IList<ProcedureItemVO> GetProcedureItems(int programmeId, int[] exceptIds);

        ProcedureInfoPVO GetPortalProcedureInfo(Guid procedureGid);

        int? GetRelatedProgrammePriority(int programmeId, int procedureId);

        IList<ProcedureDiscussionsInfoPVO> GetPortalProcedureDiscussion(Guid procedureGid);

        List<ProcedureEvalTable> GetProcedureActiveEvalTables(int procedureId);

        int FindProcedureIdByCode(string code);

        IList<ProcedureContractReportDocument> FindProcedureReportDocuments(int procedureId, ProcedureContractReportDocumentType documentType);

        Task<IList<ProcedureContractReportDocument>> FindProcedureReportDocumentsAsync(int procedureId, ProcedureContractReportDocumentType documentType, CancellationToken ct);

        IList<ApplicationSectionVO> GetApplicationSections(int procedureId);

        IList<ProcedureDirectionVO> GetProcedureDirections(int procedureId);

        IList<MapNodeDirectionVO> GetProgrammePriorityDirections(int procedureId);

        ProgrammePriorityCompany GetProgrammePriorityCompany(int procedureId);

        ProcedureApplicationDoc FindProcedureAppDoc(int procedureApplicationDocId);
    }
}
