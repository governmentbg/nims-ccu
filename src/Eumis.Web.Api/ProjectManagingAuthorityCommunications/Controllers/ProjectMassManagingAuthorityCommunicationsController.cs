using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.ProjectManagingAuthorityCommunications.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectMassManagingAuthorityCommunications")]
    public class ProjectMassManagingAuthorityCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;

        public ProjectMassManagingAuthorityCommunicationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
            this.projectMassManagingAuthorityCommunicationsRepository = projectMassManagingAuthorityCommunicationsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
        }

        [Route("")]
        public IList<ProjectMassManagingAuthorityCommunicationVO> GetProjectMassManagingAuthorityCommunications()
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            return this.projectMassManagingAuthorityCommunicationsRepository.GetProjectMassManagingAuthorityCommunications(programmeIds);
        }

        [Route("{projectMassManagingAuthorityCommunicationId:int}")]
        public ProjectMassManagingAuthorityCommunicationDO GetProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.View, projectMassManagingAuthorityCommunicationId);

            var projectMassManagingAuthorityCommunication = this.projectMassManagingAuthorityCommunicationsRepository.Find(projectMassManagingAuthorityCommunicationId);

            return new ProjectMassManagingAuthorityCommunicationDO(projectMassManagingAuthorityCommunication);
        }

        [Route("{projectMassManagingAuthorityCommunicationId:int}/info")]
        public ProjectMassManagingAuthorityCommunicationInfoVO GetProjectMassManagingAuthorityCommunicationInfo(int projectMassManagingAuthorityCommunicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.View, projectMassManagingAuthorityCommunicationId);

            return this.projectMassManagingAuthorityCommunicationsRepository.GetInfo(projectMassManagingAuthorityCommunicationId);
        }

        [HttpGet]
        [Route("new")]
        public ProjectMassManagingAuthorityCommunicationDO NewProjectMassManagingAuthorityCommunication()
        {
            return new ProjectMassManagingAuthorityCommunicationDO()
            {
                Status = ProjectMassManagingAuthorityCommunicationStatus.Draft,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Create))]
        public object CreateProjectMassManagingAuthorityCommunication(ProjectMassManagingAuthorityCommunicationDO communication)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Create, communication.ProcedureId.Value);

            var newCommunication = new ProjectMassManagingAuthorityCommunication(
                communication.ProgrammeId.Value,
                communication.ProcedureId.Value,
                this.projectMassManagingAuthorityCommunicationsRepository.GetNextOrderNum(communication.ProgrammeId.Value),
                communication.Subject,
                communication.Message,
                communication.EndingDate);

            this.projectMassManagingAuthorityCommunicationsRepository.Add(newCommunication);

            this.unitOfWork.Save();

            return new { newCommunication.ProjectMassManagingAuthorityCommunicationId };
        }

        [HttpDelete]
        [Route("{projectMassManagingAuthorityCommunicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Delete), IdParam = "projectMassManagingAuthorityCommunicationId")]
        public void DeleteProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId, string version)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, projectMassManagingAuthorityCommunicationId);

            byte[] vers = Convert.FromBase64String(version);

            this.projectMassManagingAuthorityCommunicationsRepository.DeleteProjectMassManagingAuthorityCommunication(projectMassManagingAuthorityCommunicationId, vers);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{projectMassManagingAuthorityCommunicationId:int}/canDelete")]
        public ErrorsDO CanDeleteProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, projectMassManagingAuthorityCommunicationId);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindWithoutIncludes(projectMassManagingAuthorityCommunicationId);
            var errorList = communication.CanDelete();

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{projectMassManagingAuthorityCommunicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Edit), IdParam = "projectMassManagingAuthorityCommunicationId")]
        public void UpdateProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId, ProjectMassManagingAuthorityCommunicationDO communication)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, projectMassManagingAuthorityCommunicationId);

            var massCommunication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(projectMassManagingAuthorityCommunicationId, communication.Version);

            massCommunication.AssertIsDraft();

            int? newOrderNum = null;
            if (massCommunication.ProgrammeId != communication.ProgrammeId)
            {
                newOrderNum = this.projectMassManagingAuthorityCommunicationsRepository.GetNextOrderNum(communication.ProgrammeId.Value);
            }

            massCommunication.UpdateAttributes(
                communication.ProgrammeId.Value,
                communication.ProcedureId.Value,
                communication.Subject,
                communication.Message,
                communication.EndingDate,
                newOrderNum);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{projectMassManagingAuthorityCommunicationId:int}/canSend")]
        public ErrorsDO CanSendProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, projectMassManagingAuthorityCommunicationId);

            var massCommunication = this.projectMassManagingAuthorityCommunicationsRepository.Find(projectMassManagingAuthorityCommunicationId);

            return new ErrorsDO(massCommunication.CanSend());
        }

        [HttpPost]
        [Route("{projectMassManagingAuthorityCommunicationId:int}/send")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Send), IdParam = "projectMassManagingAuthorityCommunicationId")]
        public void SendProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId, string version)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, projectMassManagingAuthorityCommunicationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.projectManagingAuthorityCommunicationService.SendProjectMassManagingAuthorityCommunication(projectMassManagingAuthorityCommunicationId, vers);
        }
    }
}
