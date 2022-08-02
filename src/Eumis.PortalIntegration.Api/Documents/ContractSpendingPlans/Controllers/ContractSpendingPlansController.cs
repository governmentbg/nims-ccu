using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ContractSpendingPlans.Controllers
{
    [RoutePrefix("api/contractSpendingPlan")]
    public class ContractSpendingPlansController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;

        public ContractSpendingPlansController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractSpendingPlansRepository contractSpendingPlansRepository)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
        }

        [Route("{spendingPlanXmlGid:guid}")]
        public XmlDO GetContractSpendingPlan(Guid spendingPlanXmlGid)
        {
            var contractSpendingPlan = this.contractSpendingPlansRepository.Find(spendingPlanXmlGid, Source.AdministrativeAuthority);
            var contractSpendingPlanProjectId = this.contractSpendingPlansRepository.GetProjectId(contractSpendingPlan.ContractSpendingPlanXmlId);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractSpendingPlanActions.View, contractSpendingPlan.ContractSpendingPlanXmlId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, contractSpendingPlanProjectId));

            return new XmlDO
            {
                Xml = contractSpendingPlan.Xml,
                Version = contractSpendingPlan.Version,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{spendingPlanXmlGid:guid}")]
        public XmlDO UpdateContractSpendingPlanXml(Guid spendingPlanXmlGid, XmlDO spendingPlanXmlDO)
        {
            var contractSpendingPlanId = this.contractSpendingPlansRepository.GetSpendingPlanId(spendingPlanXmlGid);
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.Edit, contractSpendingPlanId);

            var contractSpendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanXmlGid, Source.AdministrativeAuthority, spendingPlanXmlDO.Version);

            contractSpendingPlan.SetXml(spendingPlanXmlDO.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = contractSpendingPlan.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.UpdateXml),
                contractSpendingPlan.ContractId,
                contractSpendingPlan.ContractSpendingPlanXmlId,
                spendingPlanXmlDO,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{spendingPlanXmlGid:guid}/enter")]
        public XmlDO EnterContractSpendingPlan(Guid spendingPlanXmlGid, XmlDO spendingPlanXmlDO)
        {
            var contractSpendingPlanId = this.contractSpendingPlansRepository.GetSpendingPlanId(spendingPlanXmlGid);
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.Edit, contractSpendingPlanId);

            var contractSpendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanXmlGid, Source.AdministrativeAuthority, spendingPlanXmlDO.Version);

            contractSpendingPlan.ChangeStatusToEntered();

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = contractSpendingPlan.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.ChangeStatusToEntered),
                contractSpendingPlan.ContractId,
                contractSpendingPlan.ContractSpendingPlanXmlId,
                spendingPlanXmlDO,
                response);

            return response;
        }
    }
}
