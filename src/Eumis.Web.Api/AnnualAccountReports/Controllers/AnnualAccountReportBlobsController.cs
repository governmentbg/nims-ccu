using System;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReport/{id:int}/files")]
    public class AnnualAccountReportBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public AnnualAccountReportBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, id);
        }
    }
}
