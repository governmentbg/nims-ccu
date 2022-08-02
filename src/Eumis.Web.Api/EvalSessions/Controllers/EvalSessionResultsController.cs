using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/results")]
    public class EvalSessionResultsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IAccessContext accessContext;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public EvalSessionResultsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IAccessContext accessContext,
            IProceduresRepository proceduresRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.accessContext = accessContext;
            this.proceduresRepository = proceduresRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public EvalSessionResultsVO GetEvalSessionResults(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionResults(evalSessionId);
        }

        [Route("{resultId:int}")]
        public EvalSessionResultDO GetEvalSessionResult(int evalSessionId, int resultId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionResult = evalSession.FindEvalSessionResult(resultId);

            var procedureEvalTables = this.proceduresRepository.GetProcedureActiveEvalTables(evalSession.ProcedureId);

            return new EvalSessionResultDO(evalSessionResult, evalSession.Version, procedureEvalTables);
        }

        [Route("{resultId:int}/adminAdmissProjects")]
        public IList<EvalSessionAdminAdmissProjectsVO> GetEvalSessionAdminAdmissProjects(int evalSessionId, int resultId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);
            this.relationsRepository.AssertEvalSessionHasResult(evalSessionId, resultId);

            var projects = this.evalSessionsRepository.GetEvalSessionResultAdminAdmissProjects(evalSessionId, resultId);

            return projects;
        }

        [Route("{resultId:int}/preliminaryProjects")]
        public IList<EvalSessionPreliminaryProjectsVO> GetEvalSessionPreliminaryProjects(int evalSessionId, int resultId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);
            this.relationsRepository.AssertEvalSessionHasResult(evalSessionId, resultId);

            var projects = this.evalSessionsRepository.GetEvalSessionResultPreliminaryProjects(evalSessionId, resultId);

            return projects;
        }

        [Route("{resultId:int}/standingProjects")]
        public IList<EvalSessionStandingProjectsVO> GetEvalSessionStandingProjects(int evalSessionId, int resultId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);
            this.relationsRepository.AssertEvalSessionHasResult(evalSessionId, resultId);

            var projects = this.evalSessionsRepository.GetEvalSessionResultStandingProjects(evalSessionId, resultId);

            return projects;
        }

        [HttpGet]
        [Route("new")]
        public NewEvalSessionResultDO NewEvalSessionResult(int evalSessionId, EvalSessionResultType type)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            if (evalSession.CanCreateResult(type).Any())
            {
                throw new DomainException($"Can't create eval session result of type '{type}', for eval session id: {evalSessionId}");
            }

            return new NewEvalSessionResultDO(evalSessionId, evalSession.Version, evalSession.GetEvalSessionResultNextOrderNum(type), type);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.AdminAdmissResults.Create), IdParam = "evalSessionId")]
        public EvalSessionResultDO AddEvalSessionResult(int evalSessionId, NewEvalSessionResultDO newResultDO)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, newResultDO.Version);

            if (evalSession.CanCreateResult(newResultDO.Type).Any())
            {
                throw new DomainException($"Can't create eval session result of type '{newResultDO.Type}', for eval session id: {evalSessionId}");
            }

            var adminAdmissResult = new EvalSessionResult(evalSession.ProcedureId, newResultDO.Type);
            adminAdmissResult.OrderNum = evalSession.GetEvalSessionResultNextOrderNum(newResultDO.Type);

            evalSession.EvalSessionResults.Add(adminAdmissResult);

            this.unitOfWork.Save();

            return new EvalSessionResultDO(adminAdmissResult, evalSession.Version);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateEvalSessionResultASD(int evalSessionId, EvalSessionResultType type)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var errors = evalSession.CanCreateResult(type);

            if (type == EvalSessionResultType.AdminAdmiss)
            {
                errors.AddRange(this.evalSessionsRepository.ProcedureHasEvalTable(evalSessionId, ProcedureEvalTableType.AdminAdmiss));
            }

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{adminAdmissResultId:int}/canPublish")]
        public ErrorsDO CanPublishEvalSessionResult(int evalSessionId, int adminAdmissResultId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new ErrorsDO(evalSession.EvalSessionResultResultCanPublish(adminAdmissResultId));
        }

        [HttpPost]
        [Transaction]
        [Route("{adminAdmissResultId:int}/publish")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.AdminAdmissResults.Publish), IdParam = "evalSessionId", ChildIdParam = "adminAdmissResultId")]
        public EvalSessionResultDO PublishEvalSessionResult(int evalSessionId, int adminAdmissResultId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.ChangeEvalSessionResultStatusToPublish(adminAdmissResultId, this.accessContext.UserId);

            this.unitOfWork.Save();

            return new EvalSessionResultDO(evalSession.FindEvalSessionResult(adminAdmissResultId), evalSession.Version);
        }

        [HttpPost]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.AdminAdmissResults.Cancel), IdParam = "evalSessionId", ChildIdParam = "adminAdmissResultId")]
        [Route("{adminAdmissResultId:int}/cancel")]
        public void CancelEvalSessionResult(int evalSessionId, int adminAdmissResultId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.ChangeEvalSessionResultStatusToCancel(adminAdmissResultId, confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{adminAdmissResultId:int}/loadProjects")]
        public void LoadEvalSessionResultProjects(int evalSessionId, int adminAdmissResultId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var adminAdmissResult = evalSession.FindEvalSessionResult(adminAdmissResultId);

            if (adminAdmissResult.Projects.Any())
            {
                throw new DomainException("EvalSessionAdminAdmissResult has attached projects");
            }

            switch (adminAdmissResult.Type)
            {
                case EvalSessionResultType.Preliminary:
                    this.evalSessionsRepository.AddPreliminaryStandingProjects(evalSessionId, adminAdmissResult);
                    break;
                case EvalSessionResultType.AdminAdmiss:
                    this.evalSessionsRepository.AddEvaluatedProjects(evalSessionId, adminAdmissResult);
                    break;
                case EvalSessionResultType.Standing:
                    this.evalSessionsRepository.AddStandingProjects(evalSessionId, adminAdmissResult);
                    break;
                default:
                    break;
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{adminAdmissResultId:int}/clearProjects")]
        public void ClearEvalSessionResultProjects(int evalSessionId, int adminAdmissResultId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var adminAdmissResult = evalSession.FindEvalSessionResult(adminAdmissResultId);
            adminAdmissResult.Projects.Clear();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{adminAdmissResultId:int}/canLoadProjects")]
        public ErrorsDO CanLoadEvalSessionResultProjects(int evalSessionId, int adminAdmissResultId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errors = new List<string>();
            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var adminAdmissResult = evalSession.FindEvalSessionResult(adminAdmissResultId);

            if (adminAdmissResult.Type == EvalSessionResultType.AdminAdmiss)
            {
                errors.AddRange(this.evalSessionsRepository.EvalSessionHasUnevaluatedProjects(evalSessionId));
            }

            if (adminAdmissResult.Projects.Any())
            {
                errors.Add(WebApiTexts.EvalSessionResults_CannotLoadProjects_ProjectsAlreadyExists);
            }

            return new ErrorsDO(errors);
        }
    }
}
