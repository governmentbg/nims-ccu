using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.AnnualAccountReports.DataObjects;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/certificationDocuments")]
    public class AnnualAccountReportCertificationDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public AnnualAccountReportCertificationDocumentsController(
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
        public IList<AnnualAccountReportCertificationDocumentVO> GetAnnualAccountReportCertificationDocuments(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportCertificationDocuments(annualAccountReportId);
        }

        [Route("{documentId:int}")]
        public AnnualAccountReportCertificationDocumentDO GetAnnualAccountReportCertificationDocument(int annualAccountReportId, int documentId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.ViewCertificationDocument, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertificationDocument(annualAccountReportId, documentId);

            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            var annualAccountReportCertificationDocument = annualAccountReport.FindAnnualAccountReportCertificationDocument(documentId);

            return new AnnualAccountReportCertificationDocumentDO(annualAccountReportCertificationDocument, annualAccountReport.Version);
        }

        [HttpGet]
        [Route("new")]
        public AnnualAccountReportCertificationDocumentDO NewAnnualAccountReportCertificationDocument(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.ViewCertificationDocument, annualAccountReportId);

            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            return new AnnualAccountReportCertificationDocumentDO(annualAccountReportId, annualAccountReport.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertificationDocuments.Edit), IdParam = "annualAccountReportId", ChildIdParam = "documentId")]
        public void UpdateAnnualAccountReportCertificationDocument(int annualAccountReportId, int documentId, AnnualAccountReportCertificationDocumentDO document)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditCertificationDocument, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertificationDocument(annualAccountReportId, documentId);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, document.Version);

            annualAccountReport.UpdateAnnualAccountReportCertificationDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertificationDocuments.Create), IdParam = "annualAccountReportId")]
        public object AddAnnualAccountReportCertificationDocument(int annualAccountReportId, AnnualAccountReportCertificationDocumentDO document)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditCertificationDocument, annualAccountReportId);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, document.Version);

            var newAnnualAccountReportCertificationDocument = annualAccountReport.CreateAnnualAccountReportCertificationDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { AnnualAccountReportCertificationDocumentId = newAnnualAccountReportCertificationDocument.AnnualAccountReportCertificationDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.CertificationDocuments.Delete), IdParam = "annualAccountReportId", ChildIdParam = "documentId")]
        public void DeleteAnnualAccountReportCertificationDocument(int annualAccountReportId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditCertificationDocument, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertificationDocument(annualAccountReportId, documentId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.RemoveAnnualAccountReportCertificationDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
