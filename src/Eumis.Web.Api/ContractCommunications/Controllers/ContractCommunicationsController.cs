using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Log.ActionLogger;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    public abstract class ContractCommunicationsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private ContractCommunicationType type;
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private IContractCommunicationService contractCommunicationService;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public ContractCommunicationsController(
            ContractCommunicationType type,
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IContractCommunicationService contractCommunicationService,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IRelationsRepository relationsRepository)
        {
            this.type = type;
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.contractCommunicationService = contractCommunicationService;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.relationsRepository = relationsRepository;
        }

        protected abstract Type CreateLogAction { get; }

        protected abstract Type DeleteLogAction { get; }

        protected abstract void AssertSearchPermissions(int contractId);

        protected abstract void AssertCreatePermissions(int contractId);

        protected abstract void AssertViewPermissions(int contractId, int communicationId);

        protected abstract void AssertDeletePermissions(int contractId, int communicationId);

        [Route("")]
        public IList<ContractCommunicationVO> GetContractCommunications(int contractId)
        {
            this.AssertSearchPermissions(contractId);

            return this.contractCommunicationXmlsRepository.GetContractCommunications(contractId, this.type);
        }

        [Route("{communicationId:int}")]
        public ContractCommunicationDO GetContractCommunication(int contractId, int communicationId)
        {
            this.AssertViewPermissions(contractId, communicationId);

            var communication = this.contractCommunicationXmlsRepository.Find(communicationId);

            this.AssertIsNotDraftFromBeneficiary(communication);
            this.AssertIsCorrectType(communication);

            return new ContractCommunicationDO(communication);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        public object CreateContractCommunication(int contractId)
        {
            this.AssertCreatePermissions(contractId);

            var xml = this.documentRestApiCommunicator.CreateContractCommunicationXml(this.type);
            ContractCommunicationXml newCommunication = new ContractCommunicationXml(
                contractId,
                this.type,
                Source.AdministrativeAuthority,
                xml);

            this.contractCommunicationXmlsRepository.Add(newCommunication);

            this.unitOfWork.Save();

            var response = new { CommunicationId = newCommunication.ContractCommunicationXmlId };

            this.actionLogger.LogAction(
                this.CreateLogAction,
                contractId,
                null,
                null,
                response);

            return response;
        }

        [HttpDelete]
        [Route("{communicationId:int}")]
        [Transaction]
        public void DeleteContractCommunication(int contractId, int communicationId, string version)
        {
            this.AssertDeletePermissions(contractId, communicationId);

            if (!this.contractCommunicationService.CanDelete(communicationId))
            {
                throw new InvalidOperationException("Cannot delete communication.");
            }

            byte[] vers = System.Convert.FromBase64String(version);

            ContractCommunicationXml communication = this.contractCommunicationXmlsRepository.FindForUpdate(communicationId, vers);

            this.AssertIsNotDraftFromBeneficiary(communication);
            this.AssertIsCorrectType(communication);

            this.contractCommunicationXmlsRepository.Remove(communication);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                this.DeleteLogAction,
                contractId,
                communicationId,
                null,
                null);
        }

        protected void AssertIsNotDraftFromBeneficiary(ContractCommunicationXml communication)
        {
            if (communication.Status == ContractCommunicationStatus.Draft && communication.Source == Source.Beneficiary)
            {
                throw new UnauthorizedAccessException("Cannot get/edit/delete ContractCommunicationXml with status 'Draft' and source 'Beneficiary'");
            }
        }

        protected void AssertIsCorrectType(ContractCommunicationXml communication)
        {
            if (communication.Type != this.type)
            {
                throw new InvalidOperationException("Invalid communication message type!");
            }
        }
    }
}
