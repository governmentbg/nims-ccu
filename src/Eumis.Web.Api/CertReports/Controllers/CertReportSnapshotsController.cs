using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Domain.CertReports.DataObjects;
using System;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReportSnapshots")]
    public class CertReportSnapshotsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICertReportSnapshotsRepository certReportSnapshotsRepository;

        public CertReportSnapshotsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICertReportSnapshotsRepository certReportSnapshotsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.certReportSnapshotsRepository = certReportSnapshotsRepository;
        }

        [Route("{certReportId:int}")]
        public CertReportSnapshotDO GetCertReportSnapshot(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReportSnapshot = this.certReportSnapshotsRepository.FindByCertReport(certReportId);

            return new CertReportSnapshotDO(certReportSnapshot);
        }
    }
}
