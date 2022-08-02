using Eumis.ApplicationServices.Services.CertReport;
using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports/{certReportId:int}/revalidations")]
    public class CertReportRevalidationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportCheckService certReportCheckService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportRevalidationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICertReportService certReportService,
            ICertReportCheckService certReportCheckService,
            ICertReportsRepository certReportsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportService = certReportService;
            this.certReportCheckService = certReportCheckService;
            this.certReportsRepository = certReportsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("contractReportsRevalidations")]
        public IList<CertReportRevalidationVO> GetContractReportRevalidationsForCertReportRevalidations(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportRevalidationsForCertReportRevalidations(certReportId);
        }

        [Route("")]
        public IList<CertReportRevalidationVO> GetCertReportRevalidations(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportRevalidations(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Revalidations.Create), IdParam = "certReportId")]
        public void CreateCertReportRevalidation(int certReportId, string version, int[] contractReportRevalidationIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportRevalidation(certReportId, vers, contractReportRevalidationIds);
        }

        [HttpDelete]
        [Route("{contractReportRevalidationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Revalidations.Create), IdParam = "certReportId", ChildIdParam = "contractReportRevalidationId")]
        public void DeleteCertReportRevalidation(int certReportId, int contractReportRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportRevalidation(certReportId, vers, contractReportRevalidationId);
        }

        [HttpPost]
        [Route("certifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Revalidations.Certify), IdParam = "certReportId")]
        public void CertifyAllCertReportRevalidationCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportRevalidations(certReportId);
        }

        [HttpPost]
        [Route("uncertifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Revalidations.Certify), IdParam = "certReportId")]
        public void UncertifyAllCertReportRevalidationCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportRevalidations(certReportId);
        }
    }
}
