using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/attachedCertReports")]
    public class AnnualAccountReportAttachedCertReportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public AnnualAccountReportAttachedCertReportsController(
            IUnitOfWork unitOfWork,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        [Route("certReports")]
        public IList<CertReportVO> GetCertReportsForAnnualAccountReportAttachedCertReports(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetCertReportsForAnnualAccountReportAttachedCertReports(annualAccountReportId);
        }

        [Route("")]
        public IList<CertReportVO> GetAnnualAccountReportAttachedCertReports(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportAttachedCertReports(annualAccountReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.AttachedCertReports.Create), IdParam = "annualAccountReportId")]
        public void AddAnnualAccountReportAttachedCertReport(int annualAccountReportId, string version, int[] attachedCertReportIds)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            foreach (var attachedCertReportId in attachedCertReportIds)
            {
                annualAccountReport.AttachCertReport(attachedCertReportId);
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{attachedCertReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.AttachedCertReports.Delete), IdParam = "annualAccountReportId", ChildIdParam = "attachedCertReportId")]
        public void DeleteAnnualAccountReportAttachedCertReport(int annualAccountReportId, int attachedCertReportId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasAttachedCertReport(annualAccountReportId, attachedCertReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.RemoveAttachedCertReport(attachedCertReportId);

            this.unitOfWork.Save();
        }
    }
}
