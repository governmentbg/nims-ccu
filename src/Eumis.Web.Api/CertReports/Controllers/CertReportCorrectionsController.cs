using Eumis.ApplicationServices.Services.CertReport;
using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.ContractReportCorrections.Repositories;
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
    [RoutePrefix("api/certReports/{certReportId:int}/corrections")]
    public class CertReportCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportCheckService certReportCheckService;
        private IContractReportCorrectionService contractReportCorrectionService;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICertReportService certReportService,
            ICertReportCheckService certReportCheckService,
            IContractReportCorrectionService contractReportCorrectionService,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository,
            ICertReportsRepository certReportsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportService = certReportService;
            this.certReportCheckService = certReportCheckService;
            this.contractReportCorrectionService = contractReportCorrectionService;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
            this.certReportsRepository = certReportsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("contractReportsCorrections")]
        public IList<CertReportCorrectionVO> GetContractReportCorrectionsForCertReportCorrections(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetContractReportCorrectionsForCertReportCorrections(certReportId);
        }

        [Route("")]
        public IList<CertReportCorrectionVO> GetCertReportCorrections(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReportCorrections = this.certReportsRepository.GetCertReportCorrections(certReportId);

            foreach (var correction in certReportCorrections)
            {
                var contractReportCorrection = this.contractReportCorrectionsRepository.FindWithoutIncludes(correction.ContractReportCorrectionId);
                correction.ElementNumber = this.contractReportCorrectionService.GetContractReportCorrectionElementNumber(contractReportCorrection);
            }

            return certReportCorrections;
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Corrections.Create), IdParam = "certReportId")]
        public void CreateCertReportCorrection(int certReportId, string version, int[] contractReportCorrectionIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.CreateCertReportCorrection(certReportId, vers, contractReportCorrectionIds);
        }

        [HttpDelete]
        [Route("{contractReportCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Corrections.Create), IdParam = "certReportId", ChildIdParam = "contractReportCorrectionId")]
        public void DeleteCertReportCorrection(int certReportId, int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReportCorrection(certReportId, vers, contractReportCorrectionId);
        }

        [HttpPost]
        [Route("certifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Corrections.Certify), IdParam = "certReportId")]
        public void CertifyAllCertReportCorrectionCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.CertifyAllContractReportCorrections(certReportId);
        }

        [HttpPost]
        [Route("uncertifyAll")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Corrections.Certify), IdParam = "certReportId")]
        public void UncertifyAllCertReportCorrectionCSDs(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            this.certReportCheckService.UncertifyAllContractReportCorrections(certReportId);
        }
    }
}
