using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidations.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidations.Controllers
{
    [RoutePrefix("api/contractReportRevalidations/{contractReportRevalidationId:int}/documents")]
    public class ContractReportRevalidationDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractReportRevalidationsRepository contractReportRevalidationsRepository;

        public ContractReportRevalidationDocsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IContractReportRevalidationsRepository contractReportRevalidationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractReportRevalidationsRepository = contractReportRevalidationsRepository;
        }

        [Route("")]
        public IList<ContractReportRevalidationDocumentVO> GetSignalDocuments(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.View, contractReportRevalidationId);

            return this.contractReportRevalidationsRepository.GetDocuments(contractReportRevalidationId);
        }

        [Route("{documentId:int}")]
        public ContractReportRevalidationDocumentDO GetSignalDocument(int contractReportRevalidationId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.View, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            var document = contractReportRevalidation.GetDocument(documentId);

            return new ContractReportRevalidationDocumentDO(document, contractReportRevalidation.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractReportRevalidationDocumentDO NewSignalDocument(int contractReportRevalidationId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.Find(contractReportRevalidationId);

            return new ContractReportRevalidationDocumentDO(contractReportRevalidationId, contractReportRevalidation.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.Edit.Documents.Create), IdParam = "contractReportRevalidationId")]
        public void AddSignalDocument(int contractReportRevalidationId, ContractReportRevalidationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, document.Version);

            contractReportRevalidation.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.Edit.Documents.Edit), IdParam = "contractReportRevalidationId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int contractReportRevalidationId, int documentId, ContractReportRevalidationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, document.Version);

            contractReportRevalidation.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidations.Edit.Documents.Delete), IdParam = "contractReportRevalidationId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int contractReportRevalidationId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.Edit, contractReportRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidation = this.contractReportRevalidationsRepository.FindForUpdate(contractReportRevalidationId, vers);

            contractReportRevalidation.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
