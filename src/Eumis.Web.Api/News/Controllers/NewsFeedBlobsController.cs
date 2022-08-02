using System;
using System.Net.Http;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Data.News.Repositories;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.News.Controllers
{
    [RoutePrefix("api/newsFeed/{id:int}/files")]
    public class NewsFeedBlobsController : BlobsController
    {
        private INewsRepository newsRepository;

        public NewsFeedBlobsController(INewsRepository newsRepository, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.newsRepository = newsRepository;
        }

        public override void AssertPermissions(int id)
        {
        }

        public override HttpResponseMessage Download(int id, Guid fileKey)
        {
            if (!this.newsRepository.IsNewsFileVisible(id, fileKey))
            {
                throw new InvalidOperationException("No file with the given file key was found.");
            }

            return base.Download(id, fileKey);
        }
    }
}
