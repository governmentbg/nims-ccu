using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports/{certReportId}/attachedCertReports")]
    public class CertReportAttachedCertReportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICertReportsRepository certReportsRepository;
        private IAuthorizer authorizer;

        public CertReportAttachedCertReportsController(
            IUnitOfWork unitOfWork,
            ICertReportsRepository certReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.certReportsRepository = certReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("certReports")]
        public IList<CertReportVO> GetCertReportsForAttachedCertReports(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            return this.certReportsRepository.GetCertReportsForAttachedCertReports(certReportId);
        }

        [Route("")]
        public IList<CertReportVO> GetCertReportAttachedCertReports(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportAttachedCertReports(certReportId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AttachedCertReports.Create), IdParam = "certReportId")]
        public void AddCertReportAttachedCertReport(int certReportId, string version, int[] attachedCertReportIds)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, vers);

            foreach (var attachedCertReportId in attachedCertReportIds)
            {
                certReport.AttachCertReport(attachedCertReportId);
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{attachedCertReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.AttachedCertReports.Delete), IdParam = "certReportId", ChildIdParam = "attachedCertReportId")]
        public void DeleteCertReportAttachedCertReport(int certReportId, int attachedCertReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, vers);

            certReport.RemoveAttachedCertReport(attachedCertReportId);

            this.unitOfWork.Save();
        }
    }
}
