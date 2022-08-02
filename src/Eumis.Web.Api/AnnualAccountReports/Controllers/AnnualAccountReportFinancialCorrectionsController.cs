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
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/financialCorrections")]
    public class AnnualAccountReportFinancialCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAuthorizer authorizer;
        private IPermissionsRepository permissionsRepository;
        private IRelationsRepository relationsRepository;
        private IAccessContext accessContext;

        public AnnualAccountReportFinancialCorrectionsController(
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

        [Route("contractReportsFinancialCorrections")]
        public IList<ContractReportFinancialCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportCorrectionsForAnnualAccountReportFinancialCorrections(annualAccountReportId);
        }

        [Route("")]
        public IList<AnnualAccountReportFinancialCorrectionVO> GetAnnualAccountReportFinancialCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportFinancialCorrections(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCorrections.Create), IdParam = "annualAccountReportId")]
        public void AddAnnualAccountReportFinancialCorrection(int annualAccountReportId, string version, int[] financialCorrectionIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.CreateAnnualAccountReportFinancialCorrection(annualAccountReportId, vers, financialCorrectionIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCorrections.Create), IdParam = "annualAccountReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void DeleteAnnualAccountReportFinancialCorrection(int annualAccountReportId, int contractReportFinancialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasContractReportFinancialCorrection(annualAccountReportId, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.DeleteAnnualAccountReportFinancialCorrection(annualAccountReportId, vers, contractReportFinancialCorrectionId);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCorrections.CSDs.Create), IdParam = "annualAccountReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void CreateAnnualAccountReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId, string version, int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.CreateAnnualAccountReportFinancialCorrectionCSDs(annualAccountReportId, vers, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDIds);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionId:int}/csds")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.FinancialCorrections.CSDs.Delete), IdParam = "annualAccountReportId", ChildIdParam = "contractReportFinancialCorrectionId")]
        public void DeleteAnnualAccountReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId, string version, [FromUri] int[] contractReportFinancialCorrectionCSDIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasContractReportFinancialCorrection(annualAccountReportId, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.DeleteContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionCSDIds);

            this.unitOfWork.Save();
        }
    }
}
