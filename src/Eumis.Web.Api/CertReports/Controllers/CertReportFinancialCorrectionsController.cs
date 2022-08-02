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
    [RoutePrefix("api/certReports/{certReportId:int}/financialCorrections")]
    public class CertReportFinancialCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportCheckService certReportCheckService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportFinancialCorrectionsController(
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

        [Route("contractReportsFinancialCorrections")]
        public IList<CertReportFinancialCorrectionVO> GetContractReportFinancialCorrectionsForCertReportFinancialCorrections(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportFinancialCorrectionsForCertReportFinancialCorrections(certReportId);
        }

        [Route("")]
        public IList<CertReportFinancialCorrectionVO> GetCertReportFinancialCorrections(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportFinancialCorrections(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.Create), IdParam = "certReportId")]
        public void CreateCertReportFinancialCorrection(int certReportId, string version, int[] contractReportFinancialCorrectionIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportFinancialCorrection(certReportId, vers, contractReportFinancialCorrectionIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.Create), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void DeleteCertReportFinancialCorrection(int certReportId, int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportFinancialCorrection(certReportId, vers, contractReportFinancialCorrectionId);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.CSDs.Create), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void CreateCertReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId, string version, int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportFinancialCorrectionCSDs(certReportId, vers, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.CSDs.Delete), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void DeleteCertReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId, string version, [FromUri] int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportFinancialCorrectionCSDs(certReportId, vers, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDIds);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/certify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.CSDs.Certify), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void CertifyCertReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportFinancialCorrectionCSDs(certReportId, contractReportFinancialCorrectionId);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/uncertify")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.CSDs.Certify), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void UncertifyCertReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportFinancialCorrectionCSDs(certReportId, contractReportFinancialCorrectionId);
        }

        [HttpPost]
        [Route("certifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.Certify), IdParam = "certReportId")]
        public void CertifyAllCertReportFinancialCorrectionCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportFinancialCorrectionCSDs(certReportId);
        }

        [HttpPost]
        [Route("uncertifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCorrections.Certify), IdParam = "certReportId")]
        public void UncertifyAllCertReportFinancialCorrectionCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportFinancialCorrectionCSDs(certReportId);
        }
    }
}
