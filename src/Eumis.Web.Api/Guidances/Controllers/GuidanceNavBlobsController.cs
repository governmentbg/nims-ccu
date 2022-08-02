using System;
using System.Net.Http;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Data.Guidances.Repositories;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Guidances.Controllers
{
    [RoutePrefix("api/navGuidances/{id:int}/files")]
    public class GuidanceNavBlobsController : BlobsController
    {
        private IGuidancesRepository guidancesRepository;

        public GuidanceNavBlobsController(IGuidancesRepository guidancesRepository, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.guidancesRepository = guidancesRepository;
        }

        public override void AssertPermissions(int id)
        {
        }

        public override HttpResponseMessage Download(int id, Guid fileKey)
        {
            if (!this.guidancesRepository.ExistsGuidanceWithKey(fileKey))
            {
                throw new InvalidOperationException("The given file key does not belong to guidance.");
            }

            return base.Download(id, fileKey);
        }
    }
}
