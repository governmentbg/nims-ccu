using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.RioExtensions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using Eumis.Web.Api.Projects.DataObjects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/projects/{projectId:int}/standings")]
    public class EvalSessionProjectStandingsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public EvalSessionProjectStandingsController(
            IUnitOfWork unitOfWork,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProceduresRepository proceduresRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<EvalSessionProjectStandingVO> GetEvalSessionProjectStandings(int evalSessionId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionProjectStandings(evalSessionId, projectId);
        }

        [Route("~/api/projectDossier/{projectId:int}/evalSessionStandings")]
        public IList<EvalSessionProjectStandingVO> GetProjectEvalSessionProjectStandings(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            return this.evalSessionsRepository.GetProjectEvalSessionProjectStandings(projectId);
        }

        [Route("~/api/projectDossier/{projectId:int}/evalSessionStandings/{projectStandingId:int}")]
        public EvalSessionProjectStandingDO GetEvalSessionProjectStanding(int projectId, int projectStandingId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            var evalSessionProjectStanding = this.evalSessionsRepository.GetProjectEvalSessionProjectStanding(projectId, projectStandingId);

            var projectEvaluations = this.evalSessionsRepository.GetEvalSessionProjectStandingEvaluations(evalSessionProjectStanding.EvalSessionId, projectStandingId, projectId);

            var projectVersion = this.projectVersionXmlsRepository.Find(evalSessionProjectStanding.ProjectVersionXmlId);

            return new EvalSessionProjectStandingDO(evalSessionProjectStanding, projectEvaluations, null)
            {
                ProjectVersion = new ProjectVersionDO(projectVersion),
            };
        }

        [Route("{projectStandingId:int}")]
        public EvalSessionProjectStandingDO GetEvalSessionProjectStanding(int evalSessionId, int projectId, int projectStandingId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionProjectStanding = evalSession.FindEvalSessionProjectStanding(projectStandingId);

            var projectEvaluations = this.evalSessionsRepository.GetEvalSessionProjectStandingEvaluations(evalSessionId, projectStandingId, projectId);

            var projectVersion = this.projectVersionXmlsRepository.Find(evalSessionProjectStanding.ProjectVersionXmlId);

            return new EvalSessionProjectStandingDO(evalSessionProjectStanding, projectEvaluations, evalSession.Version)
            {
                ProjectVersion = new ProjectVersionDO(projectVersion),
            };
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionProjectStandingDO NewEvalSessionProjectStanding(int evalSessionId, int projectId, bool isPreliminary)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.FindWithoutIncludes(evalSessionId);

            var projectEvaluations = this.evalSessionsRepository.GetEvalSessionEvaluations(evalSessionId, projectId);

            decimal? grandAmount = null;
            var projectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);
            if (projectVersion != null)
            {
                var versionDoc = projectVersion.GetDocument();
                grandAmount = versionDoc.GetBudget()
                    .Select(b => b.GrandAmount)
                    .Aggregate(0M, (a, b) => a + b);
            }

            return new EvalSessionProjectStandingDO(evalSessionId, projectId, isPreliminary, projectEvaluations, evalSession.Version)
            {
                GrandAmount = grandAmount,
                ProjectVersion = new ProjectVersionDO(projectVersion),
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Standings.Create), IdParam = "evalSessionId")]
        public void AddEvalSessionProjectStanding(int evalSessionId, int projectId, EvalSessionProjectStandingDO evalSessionProjectStanding)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionProjectStanding.Version);

            var hasProjectCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSessionId, projectId);
            var hasPreliminaryEvalTable = this.proceduresRepository.HasPreliminaryEvalTable(evalSession.ProcedureId);
            var projectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);
            var versionDoc = projectVersion.GetDocument();

            var evaluationIds = evalSessionProjectStanding.ProjectEvaluations.Select(t => t.EvalSessionEvaluationId).ToList();

            decimal? actualGrandAmount = null;
            if (projectVersion != null)
            {
                actualGrandAmount = versionDoc.GetBudget()
                    .Select(b => b.GrandAmount)
                    .Aggregate(0M, (a, b) => a + b);
            }

            var esps = evalSession.AddEvalSessionProjectStanding(
                projectId,
                evalSessionProjectStanding.IsPreliminary,
                actualGrandAmount,
                evalSessionProjectStanding.OrderNum,
                evalSessionProjectStanding.Status.Value,
                evalSessionProjectStanding.GrandAmount,
                evalSessionProjectStanding.Notes,
                hasProjectCommunicationInProgress,
                projectVersion.ProjectVersionXmlId,
                evalSessionProjectStanding.RejectionReasonId,
                hasPreliminaryEvalTable,
                null);

            foreach (var evaluationId in evaluationIds)
            {
                evalSession.AddEvalSessionProjectStandingEvaluation(esps, evaluationId);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{projectStandingId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Standings.Delete), IdParam = "evalSessionId", ChildIdParam = "projectStandingId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteEvalSessionProjectStanding(int evalSessionId, int projectId, int projectStandingId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.RemoveEvalSessionProjectStanding(projectStandingId, confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("isOrderNumUnique")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public bool IsOrderNumUnique(int evalSessionId, int projectId, bool isPreliminary, int orderNum, int? budgetComponentId = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            return this.evalSessionsRepository.IsOrderNumUnique(evalSessionId, isPreliminary, orderNum);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateEvalSessionProjectStanding(int evalSessionId, int projectId, bool isPreliminary)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var hasProjectCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSessionId, projectId);
            var projectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);
            var hasPreliminaryEvalTable = this.proceduresRepository.HasPreliminaryEvalTable(evalSession.ProcedureId);

            var errorList = evalSession.CanCreateEvalSessionProjectStanding(projectId, isPreliminary, hasProjectCommunicationInProgress, projectVersion != null, hasPreliminaryEvalTable);

            return new ErrorsDO(errorList);
        }
    }
}
