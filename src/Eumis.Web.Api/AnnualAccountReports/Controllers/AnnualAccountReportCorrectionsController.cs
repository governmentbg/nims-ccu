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
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId:int}/corrections")]
    public class AnnualAccountReportCorrectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IRelationsRepository relationsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IUsersRepository usersRepository;

        public AnnualAccountReportCorrectionsController(
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

        [Route("contractReportsCorrections")]
        public IList<AnnualAccountReportCorrectionVO> GetContractReportCorrectionsForAnnualAccountReportCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportCorrectionsForAnnualAccountReportCorrections(annualAccountReportId);
        }

        [Route("")]
        public IList<AnnualAccountReportCorrectionVO> GetAnnualAccountReportCorrections(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportCorrections(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.Corrections.Create), IdParam = "annualAccountReportId")]
        public void CreateAnnualAccountReportCorrection(int annualAccountReportId, string version, int[] contractReportCorrectionIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.AddContractReportCorrections(contractReportCorrectionIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractReportCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.Corrections.Delete), IdParam = "annualAccountReportId", ChildIdParam = "contractReportCorrectionId")]
        public void DeleteAnnualAccountReportCorrection(int annualAccountReportId, int contractReportCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.relationsRepository.AssertAnnualAccountReportHasContractReportCorrection(annualAccountReportId, contractReportCorrectionId);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.RemoveContractReportCorrection(contractReportCorrectionId);

            this.unitOfWork.Save();
        }
    }
}
