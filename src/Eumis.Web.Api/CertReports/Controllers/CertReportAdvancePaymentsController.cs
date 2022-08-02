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
    [RoutePrefix("api/certReports/{certReportId:int}/advancePayments")]
    public class CertReportAdvancePaymentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportCheckService certReportCheckService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportAdvancePaymentsController(
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

        [Route("contractReports")]
        public IList<CertReportAdvancePaymentVO> GetContractReportsForCertReportAdvancePayments(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportsForCertReportAdvancePayments(certReportId);
        }

        [Route("")]
        public IList<CertReportAdvancePaymentVO> GetCertReportAdvancePayments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportAdvancePayments(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Create), IdParam = "certReportId")]
        public void CreateCertReportAdvancePayment(int certReportId, string version, int[] contractReportIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportAdvancePayment(certReportId, vers, contractReportIds);
        }

        [HttpDelete]
        [Route("{contractReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Create), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void DeleteCertReportAdvancePayment(int certReportId, int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportAdvancePayment(certReportId, vers, contractReportId);
        }

        [HttpPost]
        [Route("{contractReportId:int}/amounts")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Amounts.Create), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void CreateCertReportAdvancePaymentAmounts(int certReportId, int contractReportId, string version, int[] contractReportAdvancePaymentAmountIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportAdvancePaymentAmounts(certReportId, vers, contractReportId, contractReportAdvancePaymentAmountIds);
        }

        [HttpDelete]
        [Route("{contractReportId:int}/amounts")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Amounts.Create), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void DeleteCertReportAdvancePaymentAmounts(int certReportId, int contractReportId, string version, [FromUri] int[] contractReportAdvancePaymentAmountIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportAdvancePaymentAmounts(certReportId, vers, contractReportId, contractReportAdvancePaymentAmountIds);
        }

        [HttpPost]
        [Route("{contractReportId:int}/certify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Amounts.Certify), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void CertifyCertReportAdvancePaymentAmounts(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportAdvancePaymentAmounts(certReportId, contractReportId);
        }

        [HttpPost]
        [Route("{contractReportId:int}/uncertify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Amounts.Certify), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void UncertifyCertReportAdvancePaymentAmounts(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportAdvancePaymentAmounts(certReportId, contractReportId);
        }

        [HttpPost]
        [Route("certifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Certify), IdParam = "certReportId")]
        public void CertifyAllCertReportAdvancePaymentAmounts(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportAdvancePaymentAmounts(certReportId);
        }

        [HttpPost]
        [Route("uncertifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AdvancePayments.Certify), IdParam = "certReportId")]
        public void UncertifyAllCertReportAdvancePaymentAmounts(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportAdvancePaymentAmounts(certReportId);
        }
    }
}
