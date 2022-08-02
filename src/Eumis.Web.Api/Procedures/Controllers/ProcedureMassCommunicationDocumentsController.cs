using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedureMassCommunications/{communicationId}/documents")]
    [ActionLogPrefix(typeof(ActionLogGroups.ProcedureMassCommunication.Edit.Documents))]
    public class ProcedureMassCommunicationDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProcedureMassCommunicationsRepository procedureMassCommunicationsRepository;
        private IAuthorizer authorizer;

        public ProcedureMassCommunicationDocumentsController(
            IUnitOfWork unitOfWork,
            IProcedureMassCommunicationsRepository procedureMassCommunicationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.procedureMassCommunicationsRepository = procedureMassCommunicationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureMassCommunicationDocumentVO> GetProcedureMassCommunicationDocuments(int communicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.View, communicationId);

            return this.procedureMassCommunicationsRepository.GetCommunicationDocuments(communicationId);
        }

        [Route("{documentId:int}")]
        public ProcedureMassCommunicationDocumentDO GetProcedureMassCommunicationDocument(int communicationId, int documentId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.View, communicationId);

            var communication = this.procedureMassCommunicationsRepository.Find(communicationId);
            var document = communication.FindDocument(documentId);

            return new ProcedureMassCommunicationDocumentDO(document, communication.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureMassCommunicationDocumentDO NewProcedureMassCommunicationDocument(int communicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);

            var communication = this.procedureMassCommunicationsRepository.Find(communicationId);

            return new ProcedureMassCommunicationDocumentDO(communication.ProcedureMassCommunicationId, communication.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Edit", IdParam = "communicationId", ChildIdParam = "documentId")]
        public void UpdateActuallyPaidAmountDocument(int communicationId, int documentId, ProcedureMassCommunicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);

            var communication = this.procedureMassCommunicationsRepository.FindForUpdate(communicationId, document.Version);

            communication.UpdateDocument(
                documentId,
                document.Name,
                document.Description,
                document.File?.Name,
                document.File?.Key);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Create", IdParam = "communicationId")]
        public virtual void AddActuallyPaidAmountDocument(int communicationId, ProcedureMassCommunicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);

            var communication = this.procedureMassCommunicationsRepository.FindForUpdate(communicationId, document.Version);

            communication.AddDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null,
                document.File != null ? document.File.Name : string.Empty);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Delete", IdParam = "communicationId", ChildIdParam = "documentId")]
        public virtual void DeleteActuallyPaidAmountDocument(int communicationId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var communication = this.procedureMassCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
