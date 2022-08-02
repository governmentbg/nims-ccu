using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/evaluations")]
    public class EvalSessionEvaluationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProceduresRepository proceduresRepository;
        private IProjectsRepository projectsRepository;
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public EvalSessionEvaluationsController(
            IUnitOfWork unitOfWork,
            IEvalSessionsRepository evalSessionsRepository,
            IProceduresRepository proceduresRepository,
            IProjectsRepository projectsRepository,
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.proceduresRepository = proceduresRepository;
            this.projectsRepository = projectsRepository;
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<EvalSessionEvaluationVO> GetEvalSessionEvaluations(int evalSessionId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionEvaluations(evalSessionId, projectId);
        }

        [Route("~/api/projectDossier/{projectId:int}/evalSessionEvaluations")]
        public IList<EvalSessionEvaluationVO> GetProjectEvalSessionEvaluations(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            return this.evalSessionsRepository.GetProjectEvalSessionEvaluations(projectId);
        }

        [Route("~/api/projectDossier/{projectId:int}/evalSessionEvaluations/{evaluationId:int}")]
        public EvalSessionEvaluationDO GetProjecEvalSessionEvaluation(int projectId, int evaluationId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            var evalSessionEvaluation = this.evalSessionsRepository.GetProjectEvalSessionEvaluation(projectId, evaluationId);

            var sheets = this.evalSessionsRepository.GetEvalSessionEvaluationSheets(
                evalSessionEvaluation.EvalSessionId,
                evaluationId,
                evalSessionEvaluation.ProjectId);

            return new EvalSessionEvaluationDO(evalSessionEvaluation, sheets, null);
        }

        [Route("{evaluationId:int}")]
        public EvalSessionEvaluationDO GetEvalSessionEvaluation(int evalSessionId, int evaluationId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionEvaluation = evalSession.FindEvalSessionEvaluation(evaluationId);

            var sheets = this.evalSessionsRepository.GetEvalSessionEvaluationSheets(
                evalSessionId,
                evaluationId,
                evalSessionEvaluation.ProjectId);

            return new EvalSessionEvaluationDO(evalSessionEvaluation, sheets, evalSession.Version);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionEvaluationDO NewEvalSessionEvaluation(int evalSessionId, int projectId, ProcedureEvalTableType evalTableType)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalTable = this.proceduresRepository.GetProcedureEvalTable(evalSession.ProcedureId, evalTableType);
            var evalTableXml = this.procedureEvalTableXmlsRepository.FindByProcedureEvalTableId(evalTable.ProcedureEvalTableId);
            decimal maxEvalTableXmlPoints = 0m;

            if (evalTable.EvalType == ProcedureEvalType.Weight)
            {
                var evalTableXmlDocument = evalTableXml.GetDocument();

                foreach (var group in evalTableXmlDocument.EvalTableGroupCollection)
                {
                    foreach (var criteria in group.EvalTableCriteriaCollection)
                    {
                        maxEvalTableXmlPoints += criteria.Weight;
                    }
                }
            }

            var sheets = this.evalSessionsRepository.GetEvalSessionSheets(
                evalSessionId,
                projectId,
                evalTableType);

            return new EvalSessionEvaluationDO(evalSessionId, projectId, evalTableType, evalTable.EvalType, maxEvalTableXmlPoints, sheets, evalSession.Version);
        }

        [HttpPost]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Evaluations.Create), IdParam = "evalSessionId", DisablePostData = true)]
        public void AddEvalSessionEvaluation(int evalSessionId, EvalSessionEvaluationDO evalSessionEvaluation)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var actualSheets = this.evalSessionsRepository.GetEvalSessionSheets(evalSessionId, evalSessionEvaluation.ProjectId.Value, evalSessionEvaluation.EvalTableType.Value);

            var oldSheetStatuses = evalSessionEvaluation.Sheets.Select(t => t.Status).ToList();
            var actualSheetStatuses = actualSheets.Select(t => t.Status).ToList();
            var actualSheetIds = actualSheets.Select(p => p.EvalSessionSheetId).ToArray();

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionEvaluation.Version);

                var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(evalSessionEvaluation.ProjectId.Value);

                var ese = evalSession.AddEvalSessionEvaluation(
                    oldSheetStatuses,
                    actualSheetStatuses,
                    actualSheetIds,
                    evalSessionEvaluation.ProjectId.Value,
                    evalSessionEvaluation.EvalTableType.Value,
                    evalSessionEvaluation.CalculationType.Value,
                    evalSessionEvaluation.EvalType.Value,
                    evalSessionEvaluation.EvalIsPassed.Value,
                    evalSessionEvaluation.EvalPoints,
                    evalSessionEvaluation.EvalNote,
                    projectStatus);

                foreach (var sheetId in actualSheetIds)
                {
                    evalSession.AddEvalSessionEvaluationSheet(ese, sheetId);
                }

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("{evaluationId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Evaluations.Delete), IdParam = "evalSessionId", ChildIdParam = "evaluationId")]
        public void CancelEvalSessionEvaluation(int evalSessionId, int evaluationId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.RemoveEvalSessionEvaluation(evaluationId, confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("getEvaluativeProjects")]
        public IList<EvalSessionProjectsVO> GetEvaluativeProjects(int evalSessionId, ProcedureEvalTableType evalTableType)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvaluativeProjects(evalSessionId, evalTableType);
        }

        [HttpGet]
        [Route("canEvaluateProject")]
        public ErrorsDO GetEvaluatableProjects(int evalSessionId, ProcedureEvalTableType evalTableType, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errors = this.evalSessionsRepository.CanEvaluateProject(evalSessionId, evalTableType, projectId);
            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{evaluationId:int}/canCancel")]
        public ErrorsDO CanCancelEvalSessionEvaluation(int evalSessionId, int evaluationId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);
            this.relationsRepository.AssertЕvalSessionHasЕvaluation(evalSessionId, evaluationId);

            var errorsList = this.evalSessionsRepository.CanDeleteEvalSessionEvaluation(evaluationId, projectId);

            return new ErrorsDO(errorsList);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateEvalSessionEvaluation(int evalSessionId, int projectId, ProcedureEvalTableType evalTableType)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            Project project = this.projectsRepository.Find(projectId);

            List<string> errors = this.evalSessionsRepository.CanEvaluateProject(evalSessionId, evalTableType, projectId).ToList();

            errors.AddRange(evalSession.CanCreateEvalSessionEvaluation(project, evalTableType));

            return new ErrorsDO(errors);
        }

        [Transaction]
        [HttpPost]
        [Route("bulkEvaluation")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Evaluations.BulkCreate), IdParam = "evalSessionId")]
        public IList<EvalSessionProjectsVO> BulkEvalSessionEvaluation(int evalSessionId, ProcedureEvalTableType evalTableType, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var evaluatableProjects = this.evalSessionsRepository.GetEvaluativeProjects(evalSessionId, evalTableType);

            var evalTable = this.proceduresRepository.GetProcedureEvalTable(evalSession.ProcedureId, evalTableType);
            var evalTableXml = this.procedureEvalTableXmlsRepository.FindByProcedureEvalTableId(evalTable.ProcedureEvalTableId);
            decimal maxEvalTableXmlPoints = 0m;

            if (evalTable.EvalType == ProcedureEvalType.Weight)
            {
                var evalTableXmlDocument = evalTableXml.GetDocument();

                foreach (var group in evalTableXmlDocument.EvalTableGroupCollection)
                {
                    foreach (var criteria in group.EvalTableCriteriaCollection)
                    {
                        maxEvalTableXmlPoints += criteria.Weight;
                    }
                }
            }

            foreach (var project in evaluatableProjects)
            {
                if (this.evalSessionsRepository.CanEvaluateProject(evalSessionId, evalTableType, project.ProjectId).Count > 0)
                {
                    continue;
                }

                var sheets = this.evalSessionsRepository.GetEvalSessionSheets(
                evalSessionId,
                project.ProjectId,
                evalTableType);

                EvalSessionEvaluationDO evaluationDO = new EvalSessionEvaluationDO(evalSessionId, project.ProjectId, evalTableType, evalTable.EvalType, maxEvalTableXmlPoints, sheets, evalSession.Version);

                if ((evaluationDO.CannotEvaluate.HasValue && evaluationDO.CannotEvaluate.Value) ||
                    (evaluationDO.OriginalEvalIsPassed.HasValue && !evaluationDO.OriginalEvalIsPassed.Value))
                {
                    continue;
                }

                var oldSheetStatuses = evaluationDO.Sheets.Select(t => t.Status).ToList();
                var actualSheetStatuses = evaluationDO.Sheets.Select(t => t.Status).ToList();
                var actualSheetIds = evaluationDO.Sheets.Select(p => p.EvalSessionSheetId).ToArray();

                var ese = evalSession.AddEvalSessionEvaluation(
                    oldSheetStatuses,
                    actualSheetStatuses,
                    actualSheetIds,
                    evaluationDO.ProjectId.Value,
                    evaluationDO.EvalTableType.Value,
                    evaluationDO.CalculationType.Value,
                    evaluationDO.EvalType.Value,
                    evaluationDO.EvalIsPassed.Value,
                    evaluationDO.EvalPoints,
                    evaluationDO.EvalNote,
                    project.ProjectRegistrationStatus.Value);

                foreach (var sheetId in actualSheetIds)
                {
                    evalSession.AddEvalSessionEvaluationSheet(ese, sheetId);
                }
            }

            this.unitOfWork.Save();

            return this.evalSessionsRepository.GetEvaluativeProjects(evalSessionId, evalTableType);
        }
    }
}
