using System;
using System.Net.Http;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Data.Messages.Repositories;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Messages.Controllers
{
    [RoutePrefix("api/messages/{id:int}/files")]
    public class MessagesBlobsController : BlobsController
    {
        private IMessagesRepository messagesRepository;

        public MessagesBlobsController(IMessagesRepository messagesRepository, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.messagesRepository = messagesRepository;
        }

        public override void AssertPermissions(int id)
        {
        }

        public override HttpResponseMessage Download(int id, Guid fileKey)
        {
            if (!this.messagesRepository.IsMessageFileVisible(id, fileKey))
            {
                throw new InvalidOperationException("No file with the given file key was found.");
            }

            return base.Download(id, fileKey);
        }
    }
}
