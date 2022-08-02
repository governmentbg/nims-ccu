using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/contractReportRevalidationCertAuthorityCorrections/{id:int}/files")]
    public class ContractReportRevalidationCertAuthorityCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportRevalidationCertAuthorityCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.View, id);
        }
    }
}
