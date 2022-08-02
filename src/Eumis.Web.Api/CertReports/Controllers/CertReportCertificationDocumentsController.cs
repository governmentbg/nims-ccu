using Eumis.ApplicationServices.Services.CertReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports/{certReportId}/certificationDocuments")]
    public class CertReportCertificationDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICertReportsRepository certReportsRepository;
        private ICertReportService certReportService;
        private IAuthorizer authorizer;

        public CertReportCertificationDocumentsController(
            IUnitOfWork unitOfWork,
            ICertReportsRepository certReportsRepository,
            ICertReportService certReportService,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.certReportsRepository = certReportsRepository;
            this.certReportService = certReportService;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<CertReportCertificationDocumentsVO> GetCertReportCertificationDocuments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportCertificationDocuments(certReportId);
        }

        [Route("{documentId:int}")]
        public CertReportCertificationDocumentDO GetCertReportCertificationDocument(int certReportId, int documentId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReport = this.certReportsRepository.Find(certReportId);

            var certReportCertificationDocument = certReport.FindCertReportCertificationDocument(documentId);

            return new CertReportCertificationDocumentDO(certReportCertificationDocument, certReport.Version);
        }

        [HttpGet]
        [Route("new")]
        public CertReportCertificationDocumentDO NewCertReportCertificationDocument(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            var certReport = this.certReportsRepository.Find(certReportId);

            return new CertReportCertificationDocumentDO(certReportId, certReport.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.CertificationDocuments.Edit), IdParam = "certReportId", ChildIdParam = "documentId")]
        public void UpdateCertReportCertificationDocument(int certReportId, int documentId, CertReportCertificationDocumentDO document)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, document.Version);

            certReport.UpdateCertReportCertificationDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.CertificationDocuments.Create), IdParam = "certReportId")]
        public object AddCertReportCertificationDocument(int certReportId, CertReportCertificationDocumentDO document)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, document.Version);

            var newCertReportCertificationDocument = certReport.CreateCertReportCertificationDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { CertReportCertificationDocumentId = newCertReportCertificationDocument.CertReportCertificationDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.CertificationDocuments.Delete), IdParam = "certReportId", ChildIdParam = "documentId")]
        public void DeleteCertReportCertificationDocument(int certReportId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, vers);

            certReport.RemoveCertReportCertificationDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
