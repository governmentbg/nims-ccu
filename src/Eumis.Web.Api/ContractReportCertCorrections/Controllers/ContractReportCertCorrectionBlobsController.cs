using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertCorrections/{id:int}/files")]
    public class ContractReportCertCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportCertCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.View, id);
        }
    }
}
