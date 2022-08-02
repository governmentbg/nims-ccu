using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{id:int}/programmeProcedureManualFiles")]
    public class ProgrammeProcedureManualBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProgrammeProcedureManualBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, id);
        }
    }
}
