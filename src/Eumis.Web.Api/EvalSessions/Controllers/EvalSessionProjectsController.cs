using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/projects")]
    public class EvalSessionProjectsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IAuthorizer authorizer;
        private IProjectsRepository projectsRepository;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;

        public EvalSessionProjectsController(
            IUnitOfWork unitOfWork,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectsRepository projectsRepository,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectsRepository = projectsRepository;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
        }

        [Route("")]
        public IList<EvalSessionProjectsVO> GetEvalSessionProjects(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSession, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionProjects(evalSessionId);
        }

        [Route("~/api/projectDossier/{projectId:int}/evalSessionProjects")]
        public IList<EvalSessionProjectsVO> GetProjectEvalSessionProjects(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);

            return this.evalSessionsRepository.GetProjectEvalSessionProjects(projectId);
        }

        [HttpGet]
        [Route("{projectId:int}")]
        public EvalSessionProjectDO GetEvalSessionProject(int evalSessionId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionProject = evalSession.FindEvalSessionProject(projectId);

            return new EvalSessionProjectDO(evalSessionProject);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Create), IdParam = "evalSessionId")]
        public object AddEvalSessionProjects(int evalSessionId, string version, int[] projectIds)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProjectPermissions.CanRead);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            foreach (var projectId in projectIds)
            {
                var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(projectId);
                evalSession.AddEvalSessionProject(projectId, projectStatus);
            }

            try
            {
                this.unitOfWork.Save();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is System.Data.Entity.Core.UpdateException &&
                    e.InnerException.InnerException is System.Data.SqlClient.SqlException &&
                    ((System.Data.SqlClient.SqlException)e.InnerException.InnerException).Number == 2601 &&
                    ((System.Data.SqlClient.SqlException)e.InnerException.InnerException).Message.Contains("vwUniqueEvalSessionProjectIndexed"))
                {
                    return new
                    {
                        error = "projectAlreadyIncludedInEvalSession",
                    };
                }
                else
                {
                    throw;
                }
            }

            return null;
        }

        [HttpDelete]
        [Route("{projectId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Delete), IdParam = "evalSessionId", ChildIdParam = "projectId")]
        public void DeleteEvalSessionProject(int evalSessionId, int projectId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.RemoveEvalSessionProject(projectId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{projectId:int}/canDelete")]
        public ErrorsDO CanDeleteEvalSessionProject(int evalSessionId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSession, evalSessionId);

            var errorList = this.evalSessionsRepository.CanDeleteEvalSessionProject(evalSessionId, projectId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{projectId:int}/canCancel")]
        public ErrorsDO CanCancelEvalSessionProject(int evalSessionId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionsRepository.CanCancelEvalSessionProject(evalSessionId, projectId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{projectId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Cancel), IdParam = "evalSessionId", ChildIdParam = "projectId")]
        public void CancelEvalSessionProject(int evalSessionId, int projectId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.CancelEvalSessionProject(projectId, confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{projectId:int}/canRestore")]
        public ErrorsDO CanRestoreEvalSessionProject(int evalSessionId, int projectId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionsRepository.CanRestoreEvalSessionProject(projectId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{projectId:int}/restore")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Restore), IdParam = "evalSessionId", ChildIdParam = "projectId")]
        public void RestoreEvalSessionProject(int evalSessionId, int projectId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.RestoreEvalSessionProject(projectId);

            this.unitOfWork.Save();
        }
    }
}
