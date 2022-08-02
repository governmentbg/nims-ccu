using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/contractCommunications")]
    public class ContractContractCommunicationsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private ContractCommunicationType communicationType;
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private IContractCommunicationService contractCommunicationService;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IAuthorizer authorizer;

        public ContractContractCommunicationsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IContractCommunicationService contractCommunicationService,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IRelationsRepository relationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.contractCommunicationService = contractCommunicationService;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.relationsRepository = relationsRepository;
            this.authorizer = authorizer;
            this.communicationType = ContractCommunicationType.Administrative;
        }

        [Route("")]
        public IList<ContractCommunicationVO> GetContractCommunications(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.SearchAdminAuthorityCommunications, contractId);

            return this.contractCommunicationXmlsRepository.GetContractCommunications(contractId, this.communicationType);
        }

        [Route("{communicationId:int}")]
        public ContractCommunicationDO GetContractCommunication(int contractId, int communicationId)
        {
            this.authorizer.AssertCanDo(ContractCommunicationActions.View, communicationId);

            this.relationsRepository.AssertContractHasCommunication(contractId, communicationId);

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
            this.authorizer.AssertCanDo(ContractActions.CreateAdminAuthorityCommunication, contractId);

            var xml = this.documentRestApiCommunicator.CreateContractCommunicationXml(this.communicationType);
            ContractCommunicationXml newCommunication = new ContractCommunicationXml(
                contractId,
                this.communicationType,
                Source.AdministrativeAuthority,
                xml);

            this.contractCommunicationXmlsRepository.Add(newCommunication);

            this.unitOfWork.Save();

            var response = new { CommunicationId = newCommunication.ContractCommunicationXmlId };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.Communications.Create),
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
            this.authorizer.AssertCanDo(ContractCommunicationActions.Delete, communicationId);

            this.relationsRepository.AssertContractHasCommunication(contractId, communicationId);

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
                typeof(ActionLogGroups.Contracts.Edit.Communications.Delete),
                contractId,
                communicationId,
                null,
                null);
        }

        private void AssertIsNotDraftFromBeneficiary(ContractCommunicationXml communication)
        {
            if (communication.Status == ContractCommunicationStatus.Draft && communication.Source == Source.Beneficiary)
            {
                throw new UnauthorizedAccessException("Cannot get/edit/delete ContractCommunicationXml with status 'Draft' and source 'Beneficiary'");
            }
        }

        private void AssertIsCorrectType(ContractCommunicationXml communication)
        {
            if (communication.Type != this.communicationType)
            {
                throw new InvalidOperationException("Invalid communication message type!");
            }
        }
    }
}
