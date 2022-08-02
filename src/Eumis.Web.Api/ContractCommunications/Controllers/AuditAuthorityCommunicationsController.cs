using System;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/auditAuthorityCommunications")]
    public class AuditAuthorityCommunicationsController : ContractCommunicationsController
    {
        private IRelationsRepository relationsRepository;
        private IAuthorizer authorizer;

        public AuditAuthorityCommunicationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IActionLogger actionLogger,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository,
            IContractCommunicationService contractCommunicationService,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IRelationsRepository relationsRepository)
            : base(
                ContractCommunicationType.Audit,
                unitOfWork,
                actionLogger,
                contractCommunicationXmlsRepository,
                contractCommunicationService,
                documentRestApiCommunicator,
                relationsRepository)
        {
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        protected override Type CreateLogAction
        {
            get { return typeof(ActionLogGroups.AuditAuthorityCommunications.Create); }
        }

        protected override Type DeleteLogAction
        {
            get { return typeof(ActionLogGroups.AuditAuthorityCommunications.Delete); }
        }

        protected override void AssertSearchPermissions(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.SearchAuditAuthorityCommunications, contractId);
        }

        protected override void AssertCreatePermissions(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.CreateAuditAuthorityCommunication, contractId);
        }

        protected override void AssertViewPermissions(int contractId, int communicationId)
        {
            this.authorizer.AssertCanDo(AuditAuthorityCommunicationActions.View, communicationId);
            this.relationsRepository.AssertContractHasCommunication(contractId, communicationId);
        }

        protected override void AssertDeletePermissions(int contractId, int communicationId)
        {
            this.authorizer.AssertCanDo(AuditAuthorityCommunicationActions.Delete, communicationId);
            this.relationsRepository.AssertContractHasCommunication(contractId, communicationId);
        }
    }
}
