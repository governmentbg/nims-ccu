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
    [RoutePrefix("api/annualAccountReports/{annualAccountReportId}/auditDocuments")]
    public class AnnualAccountReportAuditDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public AnnualAccountReportAuditDocumentsController(
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
        public IList<AnnualAccountReportAuditDocumentVO> GetAnnualAccountReportAuditDocuments(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetAnnualAccountReportAuditDocuments(annualAccountReportId);
        }

        [Route("{documentId:int}")]
        public AnnualAccountReportAuditDocumentDO GetAnnualAccountReportAuditDocument(int annualAccountReportId, int documentId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.ViewAuditDcument, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasAuditDocument(annualAccountReportId, documentId);

            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            var annualAccountReportAuditDocument = annualAccountReport.FindAnnualAccountReportAuditDocument(documentId);

            return new AnnualAccountReportAuditDocumentDO(annualAccountReportAuditDocument, annualAccountReport.Version);
        }

        [HttpGet]
        [Route("new")]
        public AnnualAccountReportAuditDocumentDO NewAnnualAccountReportAuditDocument(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditAuditDcument, annualAccountReportId);

            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            return new AnnualAccountReportAuditDocumentDO(annualAccountReportId, annualAccountReport.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.AuditDocuments.Edit), IdParam = "annualAccountReportId", ChildIdParam = "documentId")]
        public void UpdateAnnualAccountReportAuditDocument(int annualAccountReportId, int documentId, AnnualAccountReportAuditDocumentDO document)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditAuditDcument, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasAuditDocument(annualAccountReportId, documentId);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, document.Version);

            annualAccountReport.UpdateAnnualAccountReportAuditDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.AuditDocuments.Create), IdParam = "annualAccountReportId")]
        public object AddAnnualAccountReportAuditDocument(int annualAccountReportId, AnnualAccountReportAuditDocumentDO document)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditAuditDcument, annualAccountReportId);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, document.Version);

            var newAnnualAccountReportAuditDocument = annualAccountReport.CreateAnnualAccountReportAuditDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { AnnualAccountReportAuditDocumentId = newAnnualAccountReportAuditDocument.AnnualAccountReportAuditDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.AuditDocuments.Delete), IdParam = "annualAccountReportId", ChildIdParam = "documentId")]
        public void DeleteAnnualAccountReportAuditDocument(int annualAccountReportId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.EditAuditDcument, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasAuditDocument(annualAccountReportId, documentId);

            byte[] vers = System.Convert.FromBase64String(version);

            var annualAccountReport = this.annualAccountReportsRepository.FindForUpdate(annualAccountReportId, vers);

            annualAccountReport.RemoveAnnualAccountReportAuditDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
