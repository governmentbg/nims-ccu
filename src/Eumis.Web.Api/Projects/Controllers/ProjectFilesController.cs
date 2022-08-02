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
    [RoutePrefix("api/projects/{projectId:int}/versionFiles")]
    public class ProjectFilesController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IProjectFilesRepository projectFilesRepository;

        public ProjectFilesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IProjectFilesRepository projectFilesRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.projectFilesRepository = projectFilesRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public HttpResponseMessage GetProjectVersionFile(int projectId, int projectFileId)
        {
            this.authorizer.AssertCanDo(ProjectActions.SearchVersions, projectId);
            this.relationsRepository.AssertProjectHasProjectFile(projectId, projectFileId);

            var projectVersionFile = this.projectFilesRepository.Find(projectFileId);

            MemoryStream fileStream = new MemoryStream(projectVersionFile.File);

            var response = this.Request.CreateResponse();
            response.Content = new PushStreamContent(
                async (responseStream, httpContent, transportContext) =>
                {
                    using (responseStream)
                    {
                        await fileStream.CopyToAsync(responseStream);
                    }
                });

            this.SetResponseHeaders(response, projectVersionFile.FileName, projectVersionFile.File.Length);

            return response;
        }

        [Route("")]
        public HttpResponseMessage GetProjectVersionFileSignature(int projectId, int projectFileId, int projectFileSignatureId)
        {
            this.authorizer.AssertCanDo(ProjectActions.SearchVersions, projectId);
            this.relationsRepository.AssertProjectHasProjectFile(projectId, projectFileId);

            var projectVersionFileSignature = this.projectFilesRepository.Find(projectFileId).ProjectFileSignatures.Where(p => p.ProjectFileSignatureId == projectFileSignatureId).Single();

            MemoryStream fileStream = new MemoryStream(projectVersionFileSignature.Signature);

            var response = this.Request.CreateResponse();
            response.Content = new PushStreamContent(
                async (responseStream, httpContent, transportContext) =>
                {
                    using (responseStream)
                    {
                        await fileStream.CopyToAsync(responseStream);
                    }
                });

            this.SetResponseHeaders(response, projectVersionFileSignature.FileName, projectVersionFileSignature.Signature.Length);

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
