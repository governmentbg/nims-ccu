using Eumis.ApplicationServices.Communicators;
using Eumis.Data.Declarations.Repositories;
using Eumis.Web.Api.Core;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Declarations.Controllers
{
    [RoutePrefix("api/declarationsFeed/{id:int}/files")]
    public class DeclarationFeedBlobsController : BlobsController
    {
        private IDeclarationsRepository declarationsRepository;

        public DeclarationFeedBlobsController(IDeclarationsRepository declarationsRepository, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.declarationsRepository = declarationsRepository;
        }

        public override void AssertPermissions(int id)
        {
        }

        public override HttpResponseMessage Download(int id, Guid fileKey)
        {
            if (!this.declarationsRepository.IsDeclarationFileAccessible(id, fileKey))
            {
                throw new InvalidOperationException("No file with the given file key was found.");
            }

            return base.Download(id, fileKey);
        }
    }
}
