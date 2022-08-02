using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Projects.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projects/{projectId:int}/communications/{communicationId:int}/versionFiles")]
    public class ProjectCommunicationFilesController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IProjectCommunicationFilesRepository projectCommunicationFilesRepository;

        public ProjectCommunicationFilesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IProjectCommunicationFilesRepository projectCommunicationFilesRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.projectCommunicationFilesRepository = projectCommunicationFilesRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public HttpResponseMessage GetProjectCommunicationFile(int projectId, int communicationId, int projectCommunicationFileId)
        {
            this.authorizer.AssertCanDo(ProjectActions.SearchCommunication, projectId);
            this.relationsRepository.AssertProjectHasProjectCommunication(projectId, communicationId);
            this.relationsRepository.AssertProjectCommunicationHasProjectCommunicationFile(communicationId, projectCommunicationFileId);

            var projectCommunicationFile = this.projectCommunicationFilesRepository.Find(projectCommunicationFileId);

            MemoryStream fileStream = new MemoryStream(projectCommunicationFile.File);

            var response = this.Request.CreateResponse();
            response.Content = new PushStreamContent(
                async (responseStream, httpContent, transportContext) =>
                {
                    using (responseStream)
                    {
                        await fileStream.CopyToAsync(responseStream);
                    }
                });

            this.SetResponseHeaders(response, projectCommunicationFile.FileName, projectCommunicationFile.File.Length);

            return response;
        }

        [Route("")]
        public HttpResponseMessage GetProjectCommunicationFileSignature(int projectId, int communicationId, int projectCommunicationFileId, int projectCommunicationFileSignatureId)
        {
            this.authorizer.AssertCanDo(ProjectActions.SearchCommunication, projectId);
            this.relationsRepository.AssertProjectHasProjectCommunication(projectId, communicationId);
            this.relationsRepository.AssertProjectCommunicationHasProjectCommunicationFile(communicationId, projectCommunicationFileId);

            var projectCommunicationFileSignature = this.projectCommunicationFilesRepository.Find(projectCommunicationFileId).ProjectCommunicationFileSignatures
                .Where(p => p.ProjectCommunicationFileSignatureId == projectCommunicationFileSignatureId).Single();

            MemoryStream fileStream = new MemoryStream(projectCommunicationFileSignature.Signature);

            var response = this.Request.CreateResponse();
            response.Content = new PushStreamContent(
                async (responseStream, httpContent, transportContext) =>
                {
                    using (responseStream)
                    {
                        await fileStream.CopyToAsync(responseStream);
                    }
                });

            this.SetResponseHeaders(response, projectCommunicationFileSignature.FileName, projectCommunicationFileSignature.Signature.Length);

            return response;
        }

        private void SetResponseHeaders(HttpResponseMessage response, string fileName, int fileLength)
        {
            response.StatusCode = HttpStatusCode.OK;
            response.Content.Headers.ContentLength = fileLength;

            response.Headers.AcceptRanges.Add("bytes");

            // do not use the class version of the ContentDisposition as it incorrectly implements UTF8 filenames
            response.Content.Headers.Add(
                "Content-Disposition",
                "attachment; filename=\"" + fileName + "\"; filename*=UTF-8''" + Uri.EscapeDataString(fileName));

            string mimeType = MimeTypeHelper.GetFileMimeTypeByExtenstion(Path.GetExtension(fileName));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrEmpty(mimeType) ? MimeTypeHelper.MIME_APPLICATION_OCTET_STREAM : mimeType);
        }
    }
}
