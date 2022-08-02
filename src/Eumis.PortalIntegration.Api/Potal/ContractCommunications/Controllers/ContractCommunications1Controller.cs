using System;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using Eumis.Data.Counters;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.ContractCommunications.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.ContractCommunications.Controllers
{
    [RoutePrefix("api/contractregs/contracts/{contractGid:guid}/communications")]
    public class ContractCommunications1Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private IContractsRepository contractsRepository;
        private IContractCommunicationService contractCommunicationService;
        private ICountersRepository countersRepository;

        public ContractCommunications1Controller(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IContractsRepository contractsRepository,
            IContractCommunicationService contractCommunicationService,
            ICountersRepository countersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.contractsRepository = contractsRepository;
            this.contractCommunicationService = contractCommunicationService;
            this.countersRepository = countersRepository;
        }

        [Route("")]
        public PagePVO<ContractCommunicationPVO> GetContractCommunications(Guid contractGid, ContractCommunicationType type, int offset = 0, int? limit = null)
        {
            return this.contractCommunicationXmlsRepository.GetPortalContractCommunications(contractGid, type, offset, limit);
        }

        [Route("{communicationGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractCommunicationDO GetContractCommunication(Guid contractGid, Guid communicationGid)
        {
            var communication = this.contractCommunicationXmlsRepository.Find(communicationGid);

            this.AssertIsNotDraftFromInternalAuthority(communication);

            if (communication.Source == Source.AdministrativeAuthority && !communication.ReadDate.HasValue)
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    communication.SetReadDate();

                    this.unitOfWork.Save();
                    transaction.Commit();
                }
            }

            return new ContractCommunicationDO(communication);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        public ContractCommunicationDO CreateContractCommunications(
            Guid contractGid,
            ContractCommunicationType type,
            ContractCommunicationDO contractCommunication)
        {
            var contract = this.contractsRepository.Find(contractGid);

            ContractCommunicationXml newContractCommunication = new ContractCommunicationXml(
                contract.ContractId,
                type,
                Source.Beneficiary,
                contractCommunication.Xml);

            this.contractCommunicationXmlsRepository.Add(newContractCommunication);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractCommunications.Create),
                newContractCommunication.ContractCommunicationXmlId,
                null,
                null,
                null);

            return new ContractCommunicationDO(newContractCommunication);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO UpdateContractCommunication(Guid contractGid, Guid communicationGid, ContractCommunicationDO contractCommunication)
        {
            if (!this.contractCommunicationService.CanUpdateCommunication(communicationGid))
            {
                throw new InvalidOperationException("Cannot update communication.");
            }

            var communication = this.contractCommunicationXmlsRepository.FindForUpdate(communicationGid, contractCommunication.Version);

            this.AssertIsNotDraftFromInternalAuthority(communication);

            communication.SetXml(contractCommunication.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractCommunications.UpdateXml),
                communication.ContractCommunicationXmlId,
                null,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/send")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO SendContractCommunication(Guid contractGid, Guid communicationGid)
        {
            if (!this.contractCommunicationService.CanActivateCommunication(communicationGid))
            {
                throw new InvalidOperationException("Cannot activate communication.");
            }

            var communication = this.contractCommunicationXmlsRepository.Find(communicationGid);

            this.AssertIsNotDraftFromInternalAuthority(communication);

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
                typeof(ActionLogPortalGroups.ContractCommunications.ChangeStatusToSent),
                communication.ContractCommunicationXmlId,
                null,
                null,
                response);

            return response;
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO DeleteContractCommunication(Guid contractGid, Guid communicationGid)
        {
            var communication = this.contractCommunicationXmlsRepository.Find(communicationGid);

            if (!this.contractCommunicationService.CanDelete(communication.ContractCommunicationXmlId))
            {
                throw new InvalidOperationException("Cannot delete communication.");
            }

            this.AssertIsNotDraftFromInternalAuthority(communication);

            this.contractCommunicationXmlsRepository.Remove(communication);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractCommunications.Delete),
                communication.ContractCommunicationXmlId,
                null,
                null,
                response);

            return response;
        }

        private void AssertIsNotDraftFromInternalAuthority(ContractCommunicationXml communication)
        {
            if (communication.Status == ContractCommunicationStatus.Draft && communication.Source == Source.AdministrativeAuthority)
            {
                throw new UnauthorizedAccessException("Cannot get/edit/delete ContractCommunicationXml with status 'Draft' and source 'AdministrativeAuthority'");
            }
        }
    }
}
