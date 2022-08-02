using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportCorrections.Controllers
{
    [RoutePrefix("api/contractReportCorrections/{id:int}/files")]
    public class ContractReportCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.View, id);
        }
    }
}
