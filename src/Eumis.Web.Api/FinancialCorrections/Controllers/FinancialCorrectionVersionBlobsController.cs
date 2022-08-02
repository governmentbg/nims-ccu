using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/financialCorrectionVersions/{id:int}/files")]
    public class FinancialCorrectionVersionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public FinancialCorrectionVersionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, id);
        }
    }
}
