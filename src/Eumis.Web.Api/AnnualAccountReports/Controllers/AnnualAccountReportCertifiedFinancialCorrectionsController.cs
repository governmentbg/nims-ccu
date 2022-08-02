using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/certifiedFinancialCorrections")]
    public class AnnualAccountReportCertifiedFinancialCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAuthorizer authorizer;
        private IPermissionsRepository permissionsRepository;
        private IRelationsRepository relationsRepository;
        private IAccessContext accessContext;

        public AnnualAccountReportCertifiedFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IAnnualAccountReportService annualAccountReportService,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IRelationsRepository relationsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.relationsRepository = relationsRepository;
            this.accessContext = accessContext;
            this.annualAccountReportService = annualAccountReportService;
        }

        [Route("certifiedFinancialCorrections")]
        public IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCertFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportCorrectionsForAnnualAccountReportCertFinancialCorrections(annualAccountReportId);
        }

        [Route("")]
        public IList<AnnualAccountReportCertFinancialCorrectionVO> GetAnnualAccountReportCertFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportCertFinancialCorrections(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCertCorrections.Create), IdParam = "annualAccountReportId")]
        public void AddAnnualAccountReportCertifiedFinancialCorrection(int annualAccountReportId, string version, int[] financialCorrectionIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.CreateAnnualAccountReportCertFinancialCorrection(annualAccountReportId, vers, financialCorrectionIds);
        }

        [HttpDelete]
        [Route("{certifiedFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCertCorrections.Create), IdParam = "annualAccountReportId", ChildIdParam = "certifiedFinancialCorrectionId")]
        public void DeleteAnnualAccountReportCertifiedFinancialCorrection(int annualAccountReportId, int certifiedFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedFinancialCorrection(annualAccountReportId, certifiedFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.DeleteAnnualAccountReportCertFinancialCorrection(annualAccountReportId, vers, certifiedFinancialCorrectionId);
        }

        [HttpPost]
        [Route("{contractReportCertFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCertCorrections.CSDs.Create), IdParam = "annualAccountReportId", ChildIdParam = "contractReportCertFinancialCorrectionId")]
        public void CreateAnnualAccountReportCertifiedFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertFinancialCorrectionId, string version, int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.CreateAnnualAccountReportCertFinancialCorrectionCSDs(annualAccountReportId, vers, contractReportCertFinancialCorrectionId, contractReportFinancialCorrectionCSDIds);
        }

        [HttpDelete]
        [Route("{contractReportCertFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCertCorrections.CSDs.Delete), IdParam = "annualAccountReportId", ChildIdParam = "contractReportCertFinancialCorrectionId")]
        public void DeleteAnnualAccountReportCertifiedFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertFinancialCorrectionId, string version, [FromUri] int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedFinancialCorrection(annualAccountReportId, contractReportCertFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.DeleteCertFinancialCorrectionCSDs(contractReportFinancialCorrectionCSDIds);

            this.unitOfWork.Save();
        }
    }
}
