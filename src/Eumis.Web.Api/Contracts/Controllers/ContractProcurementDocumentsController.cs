using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId}/procurementDocuments")]
    public class ContractProcurementDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractsRepository contractsRepository;
        private IAuthorizer authorizer;

        public ContractProcurementDocumentsController(
            IUnitOfWork unitOfWork,
            IContractsRepository contractRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.contractsRepository = contractRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ContractProcurementDocumentsVO> GetContractProcurementDocuments(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractsRepository.GetContractProcurementDocuments(contractId);
        }

        [Route("{documentId:int}")]
        public ContractProcurementDocumentDO GetContractProcurementDocument(int contractId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            var contract = this.contractsRepository.Find(contractId);

            var contractProcurementDocument = contract.FindContractProcurementDocument(documentId);

            return new ContractProcurementDocumentDO(contractProcurementDocument, contract.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractProcurementDocumentDO NewContractProcurementDocument(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.Find(contractId);

            return new ContractProcurementDocumentDO(contractId, contract.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.ProcurementDocuments.Edit), IdParam = "contractId", ChildIdParam = "documentId")]
        public void UpdateContractProcurementDocument(int contractId, int documentId, ContractProcurementDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, document.Version);

            contract.UpdateContractProcurementDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.ProcurementDocuments.Create), IdParam = "contractId")]
        public object AddContractProcurementDocument(int contractId, ContractProcurementDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, document.Version);

            var newContractProcurementDocument = contract.CreateContractProcurementDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { ContractProcurementDocumentId = newContractProcurementDocument.ContractProcurementDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.ProcurementDocuments.Delete), IdParam = "contractId", ChildIdParam = "documentId")]
        public void DeleteContractProcurementDocument(int contractId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contract = this.contractsRepository.FindForUpdate(contractId, vers);

            contract.RemoveContractProcurementDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
