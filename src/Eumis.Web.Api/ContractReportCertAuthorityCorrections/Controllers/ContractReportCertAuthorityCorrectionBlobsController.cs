using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertAuthorityCorrections/{id:int}/files")]
    public class ContractReportCertAuthorityCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportCertAuthorityCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.View, id);
        }
    }
}
