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
    [RoutePrefix("api/certReports/{certReportId:int}/certCorrections")]
    public class CertReportCertCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportCertCorrectionsController(
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

        [Route("contractReportsCertCorrections")]
        public IList<CertReportCertCorrectionVO> GetContractReportCertCorrectionsForCertReportCertCorrections(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportCertCorrectionsForCertReportCertCorrections(certReportId);
        }

        [Route("")]
        public IList<CertReportCertCorrectionVO> GetCertReportCertCorrections(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportCertCorrections(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.CertCorrections.Create), IdParam = "certReportId")]
        public void CreateCertReportCertCorrection(int certReportId, string version, int[] contractReportCertCorrectionIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportCertCorrection(certReportId, vers, contractReportCertCorrectionIds);
        }

        [HttpDelete]
        [Route("{contractReportCertCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.CertCorrections.Create), IdParam = "certReportId", ChildIdParam = "contractReportCertCorrectionId")]
        public void DeleteCertReportCertCorrection(int certReportId, int contractReportCertCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportCertCorrection(certReportId, vers, contractReportCertCorrectionId);
        }
    }
}
