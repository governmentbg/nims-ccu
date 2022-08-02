using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.AnnualAccountReports.DataObjects;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/appendices8")]
    public class AnnualAccountReportAppendices8Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public AnnualAccountReportAppendices8Controller(
            IUnitOfWork unitOfWork,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IAnnualAccountReportService annualAccountReportService,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.annualAccountReportService = annualAccountReportService;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<AnnualAccountReportAppendixVO> GetAnnualAccountReportAppendices8(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportAppendices(annualAccountReportId, AnnualAccountReportAppendixType.Appendix8);
        }

        [Route("{appendixId:int}")]
        public AnnualAccountReportAppendixDO GetAnnualAccountReportAppendix8(int annualAccountReportId, int appendixId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasAppendix(annualAccountReportId, appendixId);

            return this.annualAccountReportService.GetAnnualAccountReportAppendix(annualAccountReportId, appendixId);
        }

        [HttpPut]
        [Route("{appendixId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.Appendices8.Edit), IdParam = "annualAccountReportId", ChildIdParam = "appendixId")]
        public void UpdateAnnualAccountReportAppendix8(int annualAccountReportId, int appendixId, AnnualAccountReportAppendixDO appendix)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasAppendix(annualAccountReportId, appendixId);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, appendix.Version);

            annualAccountReport.UpdateAnnualAccountReportAppendix(appendixId, appendix.Comment);

            this.unitOfWork.Save();
        }
    }
}
