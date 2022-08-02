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
    [RoutePrefix("api/certReports/{certReportId:int}/payments")]
    public class CertReportPaymentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportCheckService certReportCheckService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportPaymentsController(
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
        public IList<CertReportPaymentVO> GetContractReportsForCertReportPayments(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportsForCertReportPayments(certReportId);
        }

        [Route("")]
        public IList<CertReportPaymentVO> GetCertReportPayments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportPayments(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.Create), IdParam = "certReportId")]
        public void CreateCertReportPayment(int certReportId, string version, int[] contractReportIds)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportPayment(certReportId, vers, contractReportIds);
        }

        [HttpDelete]
        [Route("{contractReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.Create), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void DeleteCertReportPayment(int certReportId, int contractReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportPayment(certReportId, vers, contractReportId);
        }

        [HttpPost]
        [Route("{contractReportId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.CSDs.Create), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void CreateCertReportPaymentCSDs(int certReportId, int contractReportId, string version, int[] contractReportFinancialCSDBudgetItemIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportPaymentCSDs(certReportId, vers, contractReportId, contractReportFinancialCSDBudgetItemIds);
        }

        [HttpDelete]
        [Route("{contractReportId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.CSDs.Delete), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void DeleteCertReportPaymentCSDs(int certReportId, int contractReportId, string version, [FromUri] int[] contractReportFinancialCSDBudgetItemIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportPaymentCSDs(certReportId, vers, contractReportId, contractReportFinancialCSDBudgetItemIds);
        }

        [HttpPost]
        [Route("{contractReportId:int}/certify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.CSDs.Certify), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void CertifyCertReportPaymentCSDs(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportFinancialCSDBudgetItems(certReportId, contractReportId);
        }

        [HttpPost]
        [Route("{contractReportId:int}/uncertify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.CSDs.Certify), IdParam = "certReportId", ChildIdParam = "contractReportId")]
        public void UncertifyCertReportPaymentCSDs(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportFinancialCSDBudgetItems(certReportId, contractReportId);
        }

        [HttpPost]
        [Route("certifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.Certify), IdParam = "certReportId")]
        public void CertifyAllCertReportPaymentCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportFinancialCSDBudgetItems(certReportId);
        }

        [HttpPost]
        [Route("uncertifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Payments.Certify), IdParam = "certReportId")]
        public void UncertifyAllCertReportPaymentCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportFinancialCSDBudgetItems(certReportId);
        }
    }
}
