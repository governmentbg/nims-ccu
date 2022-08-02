using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId:int}/certifiedCorrections")]
    public class AnnualAccountReportCertifiedCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IRelationsRepository relationsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IUsersRepository usersRepository;

        public AnnualAccountReportCertifiedCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IRelationsRepository relationsRepository,
            IAnnualAccountReportService annualAccountReportService,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.relationsRepository = relationsRepository;
            this.annualAccountReportService = annualAccountReportService;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("contractReportsCertifiedCorrections")]
        public IList<ContractReportCertAuthorityCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportCorrectionsForAnnualAccountReportCertCorrections(annualAccountReportId);
        }

        [Route("")]
        public IList<ContractReportCertAuthorityCorrectionVO> GetAnnualAccountReportCertCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportCertCorrections(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertCorrections.Create), IdParam = "annualAccountReportId")]
        public void CreateAnnualAccountReportCertifiedCorrection(int annualAccountReportId, string version, int[] contractReportCertifiedCorrectionIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.AddContractReportCertCorrections(contractReportCertifiedCorrectionIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCertifiedCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.Corrections.Create), IdParam = "annualAccountReportId", ChildIdParam = "contractReportCertifiedCorrectionId")]
        public void DeleteAnnualAccountReportCertifiedCorrection(int annualAccountReportId, int contractReportCertifiedCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedCorrection(annualAccountReportId, contractReportCertifiedCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.RemoveContractReportCertCorrection(contractReportCertifiedCorrectionId);

            this.unitOfWork.Save();
        }
    }
}
