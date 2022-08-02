using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Web.Api.Projects.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectManagingAuthorityCommunications")]
    public class ProjectManagingAuthorityCommunicationsController : ApiController
    {
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IRelationsRepository relationsRepository;
        private IProjectsRepository projectsRepository;
        private IPermissionsRepository permissionsRepository;

        public ProjectManagingAuthorityCommunicationsController(
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IRelationsRepository relationsRepository,
            IProjectsRepository projectsRepository,
            IPermissionsRepository permissionsRepository)
        {
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.relationsRepository = relationsRepository;
            this.projectsRepository = projectsRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("")]
        public IList<ProjectManagingAuthorityCommunicationVO> GetProjectManagingAuthorityCommunications(
           int? programmeId = null,
           int? programmePriorityId = null,
           int? procedureId = null,
           DateTime? fromDate = null,
           DateTime? toDate = null,
           ProjectManagingAuthorityCommunicationSource? source = null)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanRead);

            return this.projectManagingAuthorityCommunicationsRepository.GetAllCommunications(
                programmeIds,
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                source);
        }

        [Route("{communicationId:int}")]
        public ProjectManagingAuthorityCommunicationDO GetProjectManagingAuthorityCommunication(int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communicationId);

            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationId);

            var project = this.projectsRepository.FindWithoutIncludes(communication.ProjectId);

            return new ProjectManagingAuthorityCommunicationDO(communication, project.RegNumber, project.CompanyName);
        }
    }
}
