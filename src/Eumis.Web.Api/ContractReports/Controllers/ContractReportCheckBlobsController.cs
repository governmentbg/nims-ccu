using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReportChecks/{id:int}/files")]
    public class ContractReportCheckBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportCheckBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, id);
        }
    }
}
