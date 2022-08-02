using System;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/certReports/{id:int}/files")]
    public class CertReportBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public CertReportBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, id),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, id));
        }
    }
}
