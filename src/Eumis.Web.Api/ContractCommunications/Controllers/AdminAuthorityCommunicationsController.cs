using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    [RoutePrefix("api/contracts/adminAuthorityCommunications")]
    public class AdminAuthorityCommunicationsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IAuthorizer authorizer;
        private IContractCommunicationXmlsRepository contractCommunicationXmlsRepository;
        private IContractCommunicationService contractCommunicationService;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public AdminAuthorityCommunicationsController(
            IAuthorizer authorizer,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IContractCommunicationService contractCommunicationService,
            IRelationsRepository relationsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.relationsRepository = relationsRepository;
            this.authorizer = authorizer;
            this.contractCommunicationXmlsRepository = contractCommunicationXmlsRepository;
            this.contractCommunicationService = contractCommunicationService;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("")]
        public IList<AdminAuthorityContractCommunicationVO> GetContractCommunications(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Source? source = null)
        {
            this.authorizer.AssertCanDo(ContractCommunicationListActions.Search);
            if (!programmeId.HasValue)
            {
                // Missing programmeId could led to a lot of results, empty list is returned instead.
                return new List<AdminAuthorityContractCommunicationVO>();
            }

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractCommunicationPermissions.CanRead);

            return this.contractCommunicationXmlsRepository.GetAllCommunications(
                programmeIds,
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                source);
        }

        [Route("{communicationId:int}")]
        public ContractCommunicationDO GetContractCommunication(int communicationId)
        {
            var communication = this.contractCommunicationXmlsRepository.Find(communicationId);

            this.authorizer.AssertCanDo(ContractCommunicationActions.View, communicationId);
            this.relationsRepository.AssertContractHasCommunication(communication.ContractId, communicationId);

            this.AssertIsNotDraftFromBeneficiary(communication);
            this.AssertIsCorrectType(communication);

            return new ContractCommunicationDO(communication);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/communications")]
        public IList<ContractCommunicationVO> GetContractCommunications(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractCommunicationXmlsRepository.GetContractCommunications(contractId, ContractCommunicationType.Administrative);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/communications/{communicationId:int}")]
        public ContractCommunicationDO GetContractCommunication(int projectId, int contractId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);
            this.relationsRepository.AssertContractHasCommunication(contractId, communicationId);

            var communication = this.contractCommunicationXmlsRepository.Find(communicationId);

            this.AssertIsNotDraftFromBeneficiary(communication);
            this.AssertIsCorrectType(communication);

            return new ContractCommunicationDO(communication);
        }

        protected void AssertIsNotDraftFromBeneficiary(ContractCommunicationXml communication)
        {
            if (communication.Status == ContractCommunicationStatus.Draft && communication.Source == Source.Beneficiary)
            {
                throw new UnauthorizedAccessException("Cannot access ContractCommunicationXml with status 'Draft' and source 'Beneficiary'");
            }
        }

        protected void AssertIsCorrectType(ContractCommunicationXml communication)
        {
            if (communication.Type != ContractCommunicationType.Administrative)
            {
                throw new InvalidOperationException("Invalid communication message type!");
            }
        }
    }
}
