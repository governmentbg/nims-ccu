using Eumis.ApplicationServices.Services.ContractSpendingPlan;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractSpendingPlans.DataObjects;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.ContractSpendingPlans.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/spendingPlan")]
    public class ContractSpendingPlanXmlsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;
        private IContractsRepository contractsRepository;
        private IContractSpendingPlanService contractSpendingPlanService;

        public ContractSpendingPlanXmlsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractSpendingPlansRepository contractSpendingPlansRepository,
            IContractsRepository contractsRepository,
            IContractSpendingPlanService contractSpendingPlanService)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
            this.contractsRepository = contractsRepository;
            this.contractSpendingPlanService = contractSpendingPlanService;
        }

        [Route("")]
        public SpendingPlanPagePVO GetContractSpendingPlans(Guid contractGid, int offset = 0, int? limit = null)
        {
            var result = this.contractSpendingPlansRepository.GetPortalContractSpendingPlans(contractGid, offset, limit);
            var canCreate = !this.contractSpendingPlanService.CanCreateSpendingPlan(contractGid).Any();

            return new SpendingPlanPagePVO(result, canCreate);
        }

        [Route("{spendingPlanGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO GetContractSpendingPlan(Guid contractGid, Guid spendingPlanGid)
        {
            var spendingPlan = this.contractSpendingPlansRepository.Find(spendingPlanGid, Source.Beneficiary);

            return new XmlDO()
            {
                Xml = spendingPlan.Xml,
                Version = spendingPlan.Version,
                ModifyDate = spendingPlan.ModifyDate,
            };
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        public XmlDO CreateContractSpendingPlan(Guid contractGid)
        {
            if (this.contractSpendingPlanService.CanCreateSpendingPlan(contractGid).Any())
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.ExistsSpendingPlanInProgress }));
            }

            var contractId = this.contractsRepository.GetContractId(contractGid);

            ContractSpendingPlanXml newContractSpendingPlan = this.contractSpendingPlanService.CreateSpendingPlanFromBeneficiary(
                contractId,
                "Създаден от бенефициента план за разходване на средствата.");

            this.unitOfWork.Save();

            var result = new XmlDO
            {
                Gid = newContractSpendingPlan.Gid,
                Xml = newContractSpendingPlan.Xml,
                Version = newContractSpendingPlan.Version,
                ModifyDate = newContractSpendingPlan.ModifyDate,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractSpendingPlans.Create),
                contractId,
                newContractSpendingPlan.ContractSpendingPlanXmlId,
                null,
                result);

            return result;
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{spendingPlanGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO UpdateContractSpendingPlan(Guid contractGid, Guid spendingPlanGid, XmlDO spendingPlanXml)
        {
            var spendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanGid, Source.Beneficiary, spendingPlanXml.Version);

            spendingPlan.SetXml(spendingPlanXml.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = spendingPlan.ModifyDate,
                Version = spendingPlan.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractSpendingPlans.UpdateXml),
                spendingPlan.ContractId,
                spendingPlan.ContractSpendingPlanXmlId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{spendingPlanGid:guid}/submit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void SubmitContractSpendingPlan(Guid contractGid, Guid spendingPlanGid)
        {
            var spendingPlan = this.contractSpendingPlansRepository.Find(spendingPlanGid, Source.Beneficiary);

            spendingPlan.ChangeStatusToEntered();

            this.contractSpendingPlanService.ActivateSpendingPlan(
                spendingPlan.ContractSpendingPlanXmlId,
                spendingPlan.Version);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractSpendingPlans.Activate),
                spendingPlan.ContractId,
                spendingPlan.ContractSpendingPlanXmlId,
                null,
                null);
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{spendingPlanGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteContractSpendingPlan(Guid contractGid, Guid spendingPlanGid)
        {
            var spendingPlan = this.contractSpendingPlansRepository.Find(spendingPlanGid, Source.Beneficiary);

            this.contractSpendingPlansRepository.Remove(spendingPlan);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractSpendingPlans.Delete),
                spendingPlan.ContractId,
                spendingPlan.ContractSpendingPlanXmlId,
                null,
                null);
        }
    }
}
