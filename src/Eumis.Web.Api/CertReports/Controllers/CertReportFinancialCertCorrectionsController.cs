using Eumis.ApplicationServices.Services.CertReport;
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
    [RoutePrefix("api/certReports/{certReportId:int}/financialCertCorrections")]
    public class CertReportFinancialCertCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportFinancialCertCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICertReportService certReportService,
            ICertReportsRepository certReportsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportService = certReportService;
            this.certReportsRepository = certReportsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("contractReportsFinancialCertCorrections")]
        public IList<CertReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrectionsForCertReportFinancialCertCorrections(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportFinancialCertCorrectionsForCertReportFinancialCertCorrections(certReportId);
        }

        [Route("")]
        public IList<CertReportFinancialCertCorrectionVO> GetCertReportFinancialCertCorrections(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportFinancialCertCorrections(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCertCorrections.Create), IdParam = "certReportId")]
        public void CreateCertReportFinancialCertCorrection(int certReportId, string version, int[] contractReportFinancialCertCorrectionIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportFinancialCertCorrection(certReportId, vers, contractReportFinancialCertCorrectionIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCertCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCertCorrections.Delete), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCertCorrectionId")]
        public void DeleteCertReportFinancialCertCorrection(int certReportId, int contractReportFinancialCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportFinancialCertCorrection(certReportId, vers, contractReportFinancialCertCorrectionId);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCertCorrections.CSDs.Create), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCertCorrectionId")]
        public void CreateCertReportFinancialCertCorrectionCSDs(int certReportId, int contractReportFinancialCertCorrectionId, string version, int[] contractReportFinancialCertCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportFinancialCertCorrectionCSDs(certReportId, vers, contractReportFinancialCertCorrectionId, contractReportFinancialCertCorrectionCSDIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCertCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.FinancialCertCorrections.CSDs.Delete), IdParam = "certReportId", ChildIdParam = "contractReportFinancialCertCorrectionId")]
        public void DeleteCertReportFinancialCertCorrectionCSDs(int certReportId, int contractReportFinancialCertCorrectionId, string version, [FromUri] int[] contractReportFinancialCertCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportFinancialCertCorrectionCSDs(certReportId, vers, contractReportFinancialCertCorrectionId, contractReportFinancialCertCorrectionCSDIds);
        }
    }
}
