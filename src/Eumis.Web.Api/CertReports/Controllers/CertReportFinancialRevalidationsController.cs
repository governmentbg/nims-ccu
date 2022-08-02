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
    [RoutePrefix("api/certReports/{certReportId:int}/financialRevalidations")]
    public class CertReportFinancialRevalidationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportCheckService certReportCheckService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportFinancialRevalidationsController(
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

        [Route("contractReportsFinancialRevalidations")]
        public IList<CertReportFinancialRevalidationVO> GetContractReportFinancialRevalidationsForCertReportFinancialRevalidations(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportFinancialRevalidationsForCertReportFinancialRevalidations(certReportId);
        }

        [Route("")]
        public IList<CertReportFinancialRevalidationVO> GetCertReportFinancialRevalidations(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportFinancialRevalidations(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.Create), IdParam = "certReportId")]
        public void CreateCertReportFinancialRevalidation(int certReportId, string version, int[] contractReportFinancialRevalidationIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportFinancialRevalidation(certReportId, vers, contractReportFinancialRevalidationIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialRevalidationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.Delete), IdParam = "certReportId", ChildIdParam = "contractReportFinancialRevalidationId")]
        public void DeleteCertReportFinancialRevalidation(int certReportId, int contractReportFinancialRevalidationId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportFinancialRevalidation(certReportId, vers, contractReportFinancialRevalidationId);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.CSDs.Create), IdParam = "certReportId", ChildIdParam = "contractReportFinancialRevalidationId")]
        public void CreateCertReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId, string version, int[] contractReportFinancialRevalidationCSDIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportFinancialRevalidationCSDs(certReportId, vers, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialRevalidationId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.CSDs.Delete), IdParam = "certReportId", ChildIdParam = "contractReportFinancialRevalidationId")]
        public void DeleteCertReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId, string version, [FromUri] int[] contractReportFinancialRevalidationCSDIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportFinancialRevalidationCSDs(certReportId, vers, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDIds);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/certify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.CSDs.Certify), IdParam = "certReportId", ChildIdParam = "contractReportFinancialRevalidationId")]
        public void CertifyCertReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportFinancialRevalidationCSDs(certReportId, contractReportFinancialRevalidationId);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationId:int}/uncertify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.CSDs.Certify), IdParam = "certReportId", ChildIdParam = "contractReportFinancialRevalidationId")]
        public void UncertifyCertReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportFinancialRevalidationCSDs(certReportId, contractReportFinancialRevalidationId);
        }

        [HttpPost]
        [Route("certifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.Certify), IdParam = "certReportId")]
        public void CertifyAllCertReportFinancialRevalidationCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportFinancialRevalidationCSDs(certReportId);
        }

        [HttpPost]
        [Route("uncertifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialRevalidations.Certify), IdParam = "certReportId")]
        public void UncertifyAllCertReportFinancialRevalidationCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportFinancialRevalidationCSDs(certReportId);
        }
    }
}
