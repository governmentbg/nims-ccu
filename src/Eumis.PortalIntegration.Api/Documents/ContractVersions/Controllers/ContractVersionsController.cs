using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ContractVersions.Controllers
{
    [RoutePrefix("api/contracts")]
    public class ContractVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractVersionsRepository contractVersionsRepository;

        public ContractVersionsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractVersionsRepository contractVersionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractVersionsRepository = contractVersionsRepository;
        }

        [Route("{contractXmlGid:guid}")]
        public XmlDO GetContractVersion(Guid contractXmlGid)
        {
            var contractVersion = this.contractVersionsRepository.Find(contractXmlGid);
            var contractVersionProjectId = this.contractVersionsRepository.GetProjectId(contractVersion.ContractVersionXmlId);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractVersionActions.View, contractVersion.ContractVersionXmlId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, contractVersionProjectId));

            return new XmlDO
            {
                Xml = contractVersion.Xml,
                Version = contractVersion.Version,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{contractXmlGid:guid}")]
        public XmlDO UpdateContractVersionXml(Guid contractXmlGid, XmlDO contractXmlDO)
        {
            var contractVersionId = this.contractVersionsRepository.GetVersionId(contractXmlGid);
            this.authorizer.AssertCanDo(ContractVersionActions.Edit, contractVersionId);

            var contractVersion = this.contractVersionsRepository.FindForUpdate(contractXmlGid, contractXmlDO.Version);

            contractVersion.SetXml(contractXmlDO.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = contractVersion.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.Versions.UpdateXml),
                contractVersion.ContractId,
                contractVersion.ContractVersionXmlId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractXmlGid:guid}/enter")]
        public XmlDO EnterContractVersion(Guid contractXmlGid, XmlDO contractXmlDO)
        {
            var contractVersionId = this.contractVersionsRepository.GetVersionId(contractXmlGid);
            this.authorizer.AssertCanDo(ContractVersionActions.Edit, contractVersionId);

            var contractVersion = this.contractVersionsRepository.FindForUpdate(contractXmlGid, contractXmlDO.Version);

            contractVersion.ChangeStatusToEntered();

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = contractVersion.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.Versions.ChangeStatusToEntered),
                contractVersion.ContractId,
                contractVersion.ContractVersionXmlId,
                contractXmlDO,
                response);

            return response;
        }
    }
}
