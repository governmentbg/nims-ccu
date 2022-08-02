using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.CompensationDocuments.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.CompensationDocuments.DataObjects;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/compensationDocuments/{compensationDocumentId:int}/documents")]
    public class CompensationDocumentDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private ICompensationDocumentsRepository compensationDocumentsRepository;

        public CompensationDocumentDocsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            ICompensationDocumentsRepository compensationDocumentsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.compensationDocumentsRepository = compensationDocumentsRepository;
        }

        [Route("")]
        public IList<CompensationDocumentDocVO> GetSignalDocuments(int compensationDocumentId)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.View, compensationDocumentId);

            return this.compensationDocumentsRepository.GetDocuments(compensationDocumentId);
        }

        [Route("{documentId:int}")]
        public CompensationDocumentDocDO GetSignalDocument(int compensationDocumentId, int documentId)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.View, compensationDocumentId);

            var compensationDocument = this.compensationDocumentsRepository.Find(compensationDocumentId);

            var document = compensationDocument.GetDocument(documentId);

            return new CompensationDocumentDocDO(document, compensationDocument.Version);
        }

        [HttpGet]
        [Route("new")]
        public CompensationDocumentDocDO NewSignalDocument(int compensationDocumentId)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            var compensationDocument = this.compensationDocumentsRepository.Find(compensationDocumentId);

            return new CompensationDocumentDocDO(compensationDocumentId, compensationDocument.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.Edit.Documents.Create), IdParam = "compensationDocumentId")]
        public void AddSignalDocument(int compensationDocumentId, CompensationDocumentDocDO document)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, document.Version);

            compensationDocument.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.Edit.Documents.Edit), IdParam = "compensationDocumentId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int compensationDocumentId, int documentId, CompensationDocumentDocDO document)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, document.Version);

            compensationDocument.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.Edit.Documents.Delete), IdParam = "compensationDocumentId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int compensationDocumentId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, vers);

            compensationDocument.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
