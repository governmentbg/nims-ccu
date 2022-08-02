using System;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReportMicros/{id:int}/files")]
    public class ContractReportMicroBlobsController : BlobsController
    {
        private IAuthorizer authorizer;
        private IContractReportMicrosRepository contractReportMicrosRepository;

        public ContractReportMicroBlobsController(
            IAuthorizer authorizer,
            IBlobServerCommunicator blobServerCommunicator,
            IContractReportMicrosRepository contractReportMicrosRepository)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
            this.contractReportMicrosRepository = contractReportMicrosRepository;
        }

        public override void AssertPermissions(int id)
        {
            var contractReportId = this.contractReportMicrosRepository.GetContractReportId(id);

            this.authorizer.AssertCanDoAny(
                System.Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                System.Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));
        }
    }
}
