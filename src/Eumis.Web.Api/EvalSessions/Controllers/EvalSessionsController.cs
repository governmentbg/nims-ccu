using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Eumis.Domain.Users.ProgrammePermissions;
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
    [RoutePrefix("api/evalSessions")]
    public class EvalSessionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICacheManager cacheManager;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private ICountersRepository countersRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository;

        public EvalSessionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICacheManager cacheManager,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            ICountersRepository countersRepository,
            IProceduresRepository proceduresRepository,
            IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.cacheManager = cacheManager;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;

            this.evalSessionsRepository = evalSessionsRepository;
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.countersRepository = countersRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureMonitorstatRequestsRepository = procedureMonitorstatRequestsRepository;
        }

        [Route("")]
        public IList<EvalSessionsVO> GetEvalSessions(int? procedureId = null)
        {
            this.authorizer.AssertCanDo(EvalSessionListActions.Search);

            var programmeIdsCanAdministrate = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, EvalSessionPermissions.CanAdministrate);
            var programmeIdsCanRead = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, EvalSessionPermissions.CanRead);

            return this.evalSessionsRepository.GetEvalSessions(this.accessContext.UserId, programmeIdsCanAdministrate, programmeIdsCanRead, procedureId);
        }

        [Route("{evalSessionId:int}")]
        public EvalSessionDO GetEvalSession(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSession, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new EvalSessionDO(evalSession);
        }

        [Route("{evalSessionId:int}/info")]
        public EvalSessionInfoDO GetEvalSessionInfo(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSession, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var procedure = this.proceduresRepository.FindWithoutIncludes(evalSession.ProcedureId);

            var evalSessionActions = this.evalSessionsRepository.GetEvalSessionAvailableActions(evalSession.ProcedureId, evalSessionId);

            var procedureHasMonitorstatInquiries = this.procedureMonitorstatRequestsRepository.ProcedureHasMonitorstatRequests(evalSession.ProcedureId);

            return new EvalSessionInfoDO(evalSession, SystemLocalization.GetDisplayName(procedure.Name, procedure.NameAlt), procedureHasMonitorstatInquiries, evalSessionActions);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionDO NewEvalSession()
        {
            this.authorizer.AssertCanDo(EvalSessionListActions.Create);

            return new EvalSessionDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Create))]
        public EvalSessionDO CreateEvalSession(EvalSessionDO evalSession)
        {
            this.authorizer.AssertCanDo(EvalSessionListActions.Create);

            this.countersRepository.CreateEvalSessionCounter(evalSession.ProcedureId.Value);

            var sessionNum = this.countersRepository.GetNextEvalSessionNumber(evalSession.ProcedureId.Value);

            EvalSession newEvalSession = new EvalSession(
                evalSession.ProcedureId.Value,
                evalSession.EvalSessionType.Value,
                sessionNum,
                evalSession.OrderNum,
                evalSession.OrderDate);

            this.evalSessionsRepository.Add(newEvalSession);

            this.unitOfWork.Save();

            return new EvalSessionDO(newEvalSession);
        }

        [HttpPut]
        [Route("{evalSessionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.BasicData), IdParam = "evalSessionId")]
        public void UpdateEvalSession(int evalSessionId, EvalSessionDO evalSession)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            EvalSession oldEvalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSession.Version);

            oldEvalSession.UpdateAttributes(
                evalSession.SessionNum,
                evalSession.SessionDate,
                evalSession.OrderNum,
                evalSession.OrderDate);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{evalSessionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.ChangeStatusToDraft), IdParam = "evalSessionId")]
        public void ChangeStatusToDraft(int evalSessionId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.SetDraft, evalSessionId);

            var projects = this.projectsRepository.GetProjectsForSession(evalSessionId);

            this.ChangeStatus(evalSessionId, version, EvalSessionStatus.Draft);

            foreach (var project in projects)
            {
                project.UpdateEvalStatus(ProjectEvalStatus.Evaluation);
            }

            this.unitOfWork.BulkUpdate<Project>(projects, p => p.EvalStatus);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{evalSessionId:int}/changeStatusToActive")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.ChangeStatusToActive), IdParam = "evalSessionId")]
        public void ChangeStatusToActive(int evalSessionId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.SetActive, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var evalSession = this.evalSessionsRepository.FindWithoutIncludes(evalSessionId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var evalSessionOldStatus = evalSession.EvalSessionStatus;
                this.ChangeStatus(evalSessionId, version, EvalSessionStatus.Active);

                if (evalSessionOldStatus == EvalSessionStatus.EndedByLAG)
                {
                    var projects = this.projectsRepository.GetActiveProjectsForSession(evalSessionId);

                    foreach (var project in projects)
                    {
                        project.UpdateEvalStatus(ProjectEvalStatus.Evaluation);
                    }

                    this.unitOfWork.BulkUpdate<Project>(projects, p => p.EvalStatus);
                    this.unitOfWork.Save();
                }

                transaction.Commit();
            }

            var evalSessionUsers = this.evalSessionsRepository.GetEvalSessionUsers(evalSessionId);

            foreach (var user in evalSessionUsers)
            {
                this.cacheManager.ClearCache(ClaimsCaches.User, user.UserId);
            }
        }

        [HttpPost]
        [Route("{evalSessionId:int}/changeStatusToEnded")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.ChangeStatusToEnded), IdParam = "evalSessionId")]
        public void ChangeStatusToEnded(int evalSessionId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.SetEnded, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var projectsInfo = this.evalSessionsRepository.GetEvalSessionProjects(evalSessionId)
                .Select(p => new Tuple<ProjectRegistrationStatus, bool, int?>(p.ProjectRegistrationStatusName.Value, p.IsDeleted, p.EvalSessionProjectStandingId))
                .ToList();
            var projects = this.projectsRepository.GetActiveProjectsForSession(evalSessionId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                evalSession.ChangeStatusToEnded(projectsInfo);
                this.unitOfWork.Save();

                foreach (var project in projects)
                {
                    project.UpdateEvalStatus(ProjectEvalStatus.Evaluated);
                }

                this.unitOfWork.BulkUpdate<Project>(projects, p => p.EvalStatus);
                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("{evalSessionId:int}/changeStatusToEndedByLAG")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.ChangeStatusToEndedByLAG), IdParam = "evalSessionId")]
        public void ChangeStatusToEndedByLAG(int evalSessionId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.SetEndedByLAG, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var projectsInfo = this.evalSessionsRepository.GetEvalSessionProjects(evalSessionId)
                .Select(p => new Tuple<ProjectRegistrationStatus, bool, int?>(p.ProjectRegistrationStatusName.Value, p.IsDeleted, p.EvalSessionProjectStandingId))
                .ToList();
            var projects = this.projectsRepository.GetActiveProjectsForSession(evalSessionId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                evalSession.ChangeStatusToEndedByLAG(projectsInfo);

                foreach (var project in projects)
                {
                    project.UpdateEvalStatus(ProjectEvalStatus.PendingApproval);
                }

                this.unitOfWork.BulkUpdate<Project>(projects, p => p.EvalStatus);
                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpPost]
        [Route("{evalSessionId:int}/changeStatusToCanceled")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.ChangeStatusToCanceled), IdParam = "evalSessionId")]
        public void ChangeStatusToCanceled(int evalSessionId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.SetCanceled, evalSessionId);

            this.ChangeStatus(evalSessionId, version, EvalSessionStatus.Canceled);
        }

        private void ChangeStatus(int evalSessionId, string version, EvalSessionStatus status)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.ChangeEvalSessionStatus(status);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{evalSessionId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeStatusToDraft(int evalSessionId)
        {
            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            IList<string> errorList = new List<string>();

            if (evalSession.EvalSessionStatus == EvalSessionStatus.Canceled)
            {
                errorList = this.evalSessionsRepository.CanChangeStatusToDraft(evalSessionId);
            }

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{evalSessionId:int}/canChangeStatusToActive")]
        public ErrorsDO CanChangeStatusToActive(int evalSessionId)
        {
            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var errorList = evalSession.CanChangeStatusToActive();

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{evalSessionId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeStatusToEnded(int evalSessionId)
        {
            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var projectsInfo = this.evalSessionsRepository.GetEvalSessionProjects(evalSessionId)
                .Select(p => new Tuple<ProjectRegistrationStatus, bool, int?>(p.ProjectRegistrationStatusName.Value, p.IsDeleted, p.EvalSessionProjectStandingId))
                .ToList();

            var errorList = evalSession.CanChangeStatusToEnded(projectsInfo);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{evalSessionId:int}/canChangeStatusToEndedByLAG")]
        public ErrorsDO CanChangeStatusToEndedByLAG(int evalSessionId)
        {
            EvalSession evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var projectsInfo = this.evalSessionsRepository.GetEvalSessionProjects(evalSessionId)
                .Select(p => new Tuple<ProjectRegistrationStatus, bool, int?>(p.ProjectRegistrationStatusName.Value, p.IsDeleted, p.EvalSessionProjectStandingId))
                .ToList();

            var errorList = evalSession.CanChangeStatusToEnded(projectsInfo);

            return new ErrorsDO(errorList);
        }

        [Route("{evalSessionId:int}/getProjects")]
        public IList<ProjectRegistrationsVO> GetProjects(
            int evalSessionId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? companySizeTypeId = null,
            int? companyKidCodeId = null,
            int? projectKidCodeId = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSession, evalSessionId);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, EvalSessionPermissions.CanAdministrate);

            return this.evalSessionsRepository.GetProjectRegistrations(
                programmeIds,
                evalSessionId,
                fromDate,
                toDate,
                companySizeTypeId,
                companyKidCodeId,
                projectKidCodeId);
        }
    }
}
