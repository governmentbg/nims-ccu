using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId:int}/certifiedRevalidationCorrections")]
    public class AnnualAccountReportCertifiedRevalidationCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;

        public AnnualAccountReportCertifiedRevalidationCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository,
            IAnnualAccountReportsRepository annualAccountReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
        }

        [Route("contractReportsCertifiedRevalidationCorrections")]
        public IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetContractReportRevalidationCorrectionsForAnnualAccountReportCertCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetUnattachedContractReportRevalidationCorrections(annualAccountReportId);
        }

        [Route("")]
        public IList<ContractReportRevalidationCertAuthorityCorrectionVO> GetAnnualAccountReportCertCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportCertRevalidationCorrections(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertRevalidationCorrections.Create), IdParam = "annualAccountReportId")]
        public void CreateAnnualAccountReportCertifiedRevalidationCorrection(int annualAccountReportId, string version, int[] contractReportCertifiedRevalidationCorrectionIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.AddContractReportCertRevalidationCorrections(contractReportCertifiedRevalidationCorrectionIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCertifiedRevalidationCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertRevalidationCorrections.Delete), IdParam = "annualAccountReportId", ChildIdParam = "contractReportCertifiedRevalidationCorrectionId")]
        public void DeleteAnnualAccountReportCertifiedRevalidationCorrection(int annualAccountReportId, int contractReportCertifiedRevalidationCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedRevalidationCorrection(annualAccountReportId, contractReportCertifiedRevalidationCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.RemoveContractReportCertRevalidationCorrection(contractReportCertifiedRevalidationCorrectionId);

            this.unitOfWork.Save();
        }
    }
}
