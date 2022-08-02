using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/flatFinancialCorrections/{id:int}/files")]
    public class FlatFinancialCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public FlatFinancialCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(FlatFinancialCorrectionActions.View, id);
        }
    }
}
