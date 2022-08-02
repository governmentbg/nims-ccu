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
    [RoutePrefix("api/certReports/{certReportId}/documents")]
    public class CertReportDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICertReportsRepository certReportsRepository;
        private ICertReportService certReportService;
        private IAuthorizer authorizer;

        public CertReportDocumentsController(
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
        public IList<CertReportDocumentsVO> GetCertReportDocuments(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetCertReportDocuments(certReportId);
        }

        [Route("{documentId:int}")]
        public CertReportDocumentDO GetCertReportDocument(int certReportId, int documentId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReport = this.certReportsRepository.Find(certReportId);

            var certReportDocument = certReport.FindCertReportDocument(documentId);

            return new CertReportDocumentDO(certReportDocument, certReport.Version);
        }

        [HttpGet]
        [Route("new")]
        public CertReportDocumentDO NewCertReportDocument(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            var certReport = this.certReportsRepository.Find(certReportId);

            return new CertReportDocumentDO(certReportId, certReport.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Documents.Edit), IdParam = "certReportId", ChildIdParam = "documentId")]
        public void UpdateCertReportDocument(int certReportId, int documentId, CertReportDocumentDO document)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, document.Version);

            certReport.UpdateCertReportDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Documents.Create), IdParam = "certReportId")]
        public object AddCertReportDocument(int certReportId, CertReportDocumentDO document)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, document.Version);

            var newCertReportDocument = certReport.CreateCertReportDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { CertReportDocumentId = newCertReportDocument.CertReportDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.Documents.Delete), IdParam = "certReportId", ChildIdParam = "documentId")]
        public void DeleteCertReportDocument(int certReportId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var certReport = this.certReportsRepository.FindForUpdate(certReportId, vers);

            certReport.RemoveCertReportDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
