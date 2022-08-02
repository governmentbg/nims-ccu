using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId}/grantDocuments")]
    public class ContractGrantDocumentsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IContractsRepository contractsRepository;
        private IAuthorizer authorizer;

        public ContractGrantDocumentsController(
            IUnitOfWork unitOfWork,
            IContractsRepository contractRepository,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractsRepository = contractRepository;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractGrantDocumentsVO> GetContractGrantDocuments(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractsRepository.GetContractGrantDocuments(contractId);
        }

        [Route("{documentId:int}")]
        public ContractGrantDocumentDO GetContractDocumentGrant(int contractId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            var contract = this.contractsRepository.Find(contractId);

            var contractDocument = contract.FindContractGrantDocument(documentId);

            return new ContractGrantDocumentDO(contractDocument, contract.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractGrantDocumentDO NewContractGrantDocument(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.Find(contractId);

            return new ContractGrantDocumentDO(contractId, contract.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.GrantDocuments.Edit), IdParam = "contractId", ChildIdParam = "documentId")]
        public void UpdateContractGrantDocument(int contractId, int documentId, ContractGrantDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, document.Version);

            contract.UpdateContractGrantDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.GrantDocuments.Create), IdParam = "contractId")]
        public object AddContractGrantDocument(int contractId, ContractGrantDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, document.Version);

            var newContractGrantDocument = contract.CreateContractGrantDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { ContractGrantDocumentId = newContractGrantDocument.ContractGrantDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.GrantDocuments.Delete), IdParam = "contractId", ChildIdParam = "documentId")]
        public void DeleteContractGrantDocument(int contractId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contract = this.contractsRepository.FindForUpdate(contractId, vers);

            contract.RemoveContractGrantDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
