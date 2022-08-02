using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.ProjectManagingAuthorityCommunications.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectMassManagingAuthorityCommunications/{communicationId}/documents")]
    [ActionLogPrefix(typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Edit.Documents))]
    public class ProjectMassManagingAuthorityCommunicationDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository;
        private IAuthorizer authorizer;

        public ProjectMassManagingAuthorityCommunicationDocumentsController(
            IUnitOfWork unitOfWork,
            IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.projectMassManagingAuthorityCommunicationsRepository = projectMassManagingAuthorityCommunicationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProjectMassManagingAuthorityCommunicationDocumentVO> GetProjectMassManagingAuthorityCommunicationDocuments(int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.View, communicationId);

            return this.projectMassManagingAuthorityCommunicationsRepository.GetCommunicationDocuments(communicationId);
        }

        [Route("{documentId:int}")]
        public ProjectMassManagingAuthorityCommunicationDocumentDO GetProjectMassManagingAuthorityCommunicationDocument(int communicationId, int documentId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.View, communicationId);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.Find(communicationId);
            var document = communication.FindDocument(documentId);

            return new ProjectMassManagingAuthorityCommunicationDocumentDO(document, communication.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProjectMassManagingAuthorityCommunicationDocumentDO NewProjectMassManagingAuthorityCommunicationDocument(int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.Find(communicationId);

            return new ProjectMassManagingAuthorityCommunicationDocumentDO(communication.ProjectMassManagingAuthorityCommunicationId, communication.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Edit", IdParam = "communicationId", ChildIdParam = "documentId")]
        public void UpdateProjectMassManagingAuthorityCommunicationDocument(int communicationId, int documentId, ProjectMassManagingAuthorityCommunicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, document.Version);

            communication.UpdateDocument(
                documentId,
                document.Name,
                document.Description,
                document.File?.Name,
                document.File?.Key);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Create", IdParam = "communicationId")]
        public virtual void AddProjectMassManagingAuthorityCommunicationDocument(int communicationId, ProjectMassManagingAuthorityCommunicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, document.Version);

            communication.AddDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null,
                document.File != null ? document.File.Name : string.Empty);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Delete", IdParam = "communicationId", ChildIdParam = "documentId")]
        public virtual void DeleteProjectMassManagingAuthorityCommunicationDocument(int communicationId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
