using System;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Counters;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;

namespace Eumis.PortalIntegration.Api.Documents.ContractCommunications.Controllers
{
    [RoutePrefix("api/contractCommunications")]
    public class ContractCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private ICountersRepository countersRepository;
        private IContractCommunicationService contractCommunicationService;

        public ContractCommunicationsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            ICountersRepository countersRepository,
            IContractCommunicationService contractCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.countersRepository = countersRepository;
            this.contractCommunicationService = contractCommunicationService;
        }

        [Route("{communicationGid:guid}")]
        public XmlDO GetContractCommunication(Guid communicationGid)
        {
            var communicationData = this.contractCommunicationXmlsRepository.GetCommunicationIdAndType(communicationGid);
            var communicationProjectId = this.contractCommunicationXmlsRepository.GetProjectId(communicationData.Item1);

            this.AssertViewPermissions(communicationData.Item1, communicationData.Item2, communicationProjectId);

            var communication = this.contractCommunicationXmlsRepository.Find(communicationGid);

            this.AssertIsNotDraftFromBeneficiary(communication);

            if (communication.Source == Source.Beneficiary && !communication.ReadDate.HasValue)
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    communication.SetReadDate();

                    this.unitOfWork.Save();
                    transaction.Commit();
                }
            }

            return new XmlDO
            {
                Xml = communication.Xml,
                Version = communication.Version,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}")]
        public XmlDO UpdateContractCommunication(Guid communicationGid, XmlDO contractCommunication)
        {
            var communicationData = this.contractCommunicationXmlsRepository.GetCommunicationIdAndType(communicationGid);
            this.AssertEditPermissions(communicationData.Item1, communicationData.Item2);

            if (!this.contractCommunicationService.CanUpdateCommunication(communicationGid))
            {
                throw new InvalidOperationException("Cannot update communication.");
            }

            var communication = this.contractCommunicationXmlsRepository.FindForUpdate(communicationGid, contractCommunication.Version);

            this.AssertIsNotDraftFromBeneficiary(communication);

            communication.SetXml(contractCommunication.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                this.GetUpdateLogAction(communication.Type),
                communication.ContractId,
                communication.ContractCommunicationXmlId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/send")]
        public XmlDO SendContractCommunication(Guid communicationGid, XmlDO contractCommunication)
        {
            var communicationData = this.contractCommunicationXmlsRepository.GetCommunicationIdAndType(communicationGid);
            this.AssertEditPermissions(communicationData.Item1, communicationData.Item2);

            if (!this.contractCommunicationService.CanActivateCommunication(communicationGid))
            {
                throw new InvalidOperationException("Cannot activate communication.");
            }

            var communication = this.contractCommunicationXmlsRepository.FindForUpdate(communicationGid, contractCommunication.Version);

            this.AssertIsNotDraftFromBeneficiary(communication);

            this.countersRepository.CreateContractCommunicationCounter(communication.ContractId);

            var regNumber = this.countersRepository.GetNextContractCommunicationNumber(communication.ContractId);

            communication.Activate(regNumber);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                this.GetSentLogAction(communication.Type),
                communication.ContractId,
                communication.ContractCommunicationXmlId,
                contractCommunication,
                response);

            return response;
        }

        private void AssertViewPermissions(int contractCommunicationId, ContractCommunicationType type, int? projectId = null)
        {
            switch (type)
            {
                case ContractCommunicationType.Administrative:
                    this.authorizer.AssertCanDoAny(
                        Tuple.Create<Enum, int?>(ContractCommunicationActions.View, contractCommunicationId),
                        Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectId));
                    break;
                case ContractCommunicationType.Cert:
                    this.authorizer.AssertCanDo(CertAuthorityCommunicationActions.View, contractCommunicationId);
                    break;
                case ContractCommunicationType.Audit:
                    this.authorizer.AssertCanDo(AuditAuthorityCommunicationActions.View, contractCommunicationId);
                    break;
                default:
                    throw new DomainValidationException("Invalid contract communciation type.");
            }
        }

        private void AssertEditPermissions(int contractCommunicationId, ContractCommunicationType type)
        {
            switch (type)
            {
                case ContractCommunicationType.Administrative:
                    this.authorizer.AssertCanDo(ContractCommunicationActions.Edit, contractCommunicationId);
                    break;
                case ContractCommunicationType.Cert:
                    this.authorizer.AssertCanDo(CertAuthorityCommunicationActions.Edit, contractCommunicationId);
                    break;
                case ContractCommunicationType.Audit:
                    this.authorizer.AssertCanDo(AuditAuthorityCommunicationActions.Edit, contractCommunicationId);
                    break;
                default:
                    throw new DomainValidationException("Invalid contract communciation type.");
            }
        }

        private Type GetUpdateLogAction(ContractCommunicationType type)
        {
            switch (type)
            {
                case ContractCommunicationType.Administrative:
                    return typeof(ActionLogGroups.Contracts.Edit.Communications.UpdateXml);
                case ContractCommunicationType.Cert:
                    return typeof(ActionLogGroups.CertAuthorityCommunications.UpdateXml);
                case ContractCommunicationType.Audit:
                    return typeof(ActionLogGroups.AuditAuthorityCommunications.UpdateXml);
                default:
                    throw new DomainValidationException("Invalid contract communciation type.");
            }
        }

        private Type GetSentLogAction(ContractCommunicationType type)
        {
            switch (type)
            {
                case ContractCommunicationType.Administrative:
                    return typeof(ActionLogGroups.Contracts.Edit.Communications.ChangeStatusToSent);
                case ContractCommunicationType.Cert:
                    return typeof(ActionLogGroups.CertAuthorityCommunications.ChangeStatusToSent);
                case ContractCommunicationType.Audit:
                    return typeof(ActionLogGroups.AuditAuthorityCommunications.ChangeStatusToSent);
                default:
                    throw new DomainValidationException("Invalid contract communciation type.");
            }
        }

        private void AssertIsNotDraftFromBeneficiary(ContractCommunicationXml communication)
        {
            if (communication.Status == ContractCommunicationStatus.Draft && communication.Source == Source.Beneficiary)
            {
                throw new UnauthorizedAccessException("Cannot get/edit/delete ContractCommunicationXml with status 'Draft' and source 'Beneficiary'");
            }
        }
    }
}
