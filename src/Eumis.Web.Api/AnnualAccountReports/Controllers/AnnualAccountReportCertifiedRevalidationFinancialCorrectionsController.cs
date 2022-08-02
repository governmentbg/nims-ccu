using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/certifiedRevalidationFinancialCorrections")]
    public class AnnualAccountReportCertifiedRevalidationFinancialCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public AnnualAccountReportCertifiedRevalidationFinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IAnnualAccountReportService annualAccountReportService,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
            this.annualAccountReportService = annualAccountReportService;
        }

        [Route("certifiedRevalidationFinancialCorrections")]
        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionVO> GetContractReportRevalidationCorrectionsForAnnualAccountReportCertFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportCorrectionsForAnnualAccountReportCertRevalidationFinancialCorrections(annualAccountReportId);
        }

        [Route("")]
        public IList<AnnualAccountReportCertRevalidationFinancialCorrectionVO> GetAnnualAccountReportCertFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportCertRevalidationFinancialCorrections(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertRevalidationFinancialCorrections.Create), IdParam = "annualAccountReportId")]
        public void AddAnnualAccountReportCertifiedRevalidationFinancialCorrection(int annualAccountReportId, string version, int[] financialCorrectionIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.CreateAnnualAccountReportCertRevalidationFinancialCorrection(annualAccountReportId, vers, financialCorrectionIds);
        }

        [HttpDelete]
        [Route("{certifiedRevalidationFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertRevalidationFinancialCorrections.Delete), IdParam = "annualAccountReportId", ChildIdParam = "certifiedRevalidationFinancialCorrectionId")]
        public void DeleteAnnualAccountReportCertifiedRevalidationFinancialCorrection(int annualAccountReportId, int certifiedRevalidationFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.DeleteAnnualAccountReportCertRevalidationFinancialCorrection(annualAccountReportId, vers, certifiedRevalidationFinancialCorrectionId);
        }

        [HttpPost]
        [Route("csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertRevalidationFinancialCorrections.CSDs.Create), IdParam = "annualAccountReportId")]
        public void CreateAnnualAccountReportCertifiedRevalidationFinancialCorrectionCSDs(int annualAccountReportId, string version, int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.AddCertRevalidationFinnancialCorrectionsCSDs(contractReportFinancialCorrectionCSDIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCertRevalidationFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertRevalidationFinancialCorrections.CSDs.Delete), IdParam = "annualAccountReportId", ChildIdParam = "contractReportCertRevalidationFinancialCorrectionId")]
        public void DeleteAnnualAccountReportCertifiedRevalidationFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertRevalidationFinancialCorrectionId, string version, [FromUri] int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(annualAccountReportId, contractReportRevalidationCertAuthorityFinancialCorrectionId: contractReportCertRevalidationFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.DeleteCertRevalidationFinancialCorrectionCSDs(contractReportFinancialCorrectionCSDIds);

            this.unitOfWork.Save();
        }
    }
}
