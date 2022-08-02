using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ProcedureVersion;
using Eumis.Authentication.AccessContexts;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractRegistrations.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.Controllers
{
    [RoutePrefix("api/contractreg/contracts")]
    public class ContractsController : ApiController
    {
        private IContractsRepository contractsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;
        private IAccessContext accessContext;
        private IProcedureVersionService procedureVersionService;

        public ContractsController(
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractSpendingPlansRepository contractSpendingPlansRepository,
            IProcedureVersionService procedureVersionService)
        {
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
            this.procedureVersionService = procedureVersionService;
        }

        [Route("")]
        public PagePVO<ContractPVO> GetContracts(int offset = 0, int? limit = null)
        {
            if (this.accessContext.IsContractRegistration)
            {
                return this.contractsRepository.GetPortalContractsForRegistration(this.accessContext.ContractRegistrationId, offset, limit);
            }
            else if (this.accessContext.IsContractAccessCode)
            {
                int contractId = ((ContractAccessCodeAccessContext)this.accessContext).ContractId;
                return this.contractsRepository.GetPortalContractsForAccessCode(contractId, offset, limit);
            }
            else
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
        }

        [Route("{contractGid:guid}")]
        public ContractPVO GetContract(Guid contractGid)
        {
            if (this.accessContext.IsContractRegistration)
            {
                return this.contractsRepository.GetPortalContractForRegistration(contractGid, this.accessContext.ContractRegistrationId);
            }
            else if (this.accessContext.IsContractAccessCode)
            {
                int contractId = ((ContractAccessCodeAccessContext)this.accessContext).ContractId;
                return this.contractsRepository.GetPortalContractForAccessCode(contractGid, contractId);
            }
            else
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
        }

        [Route("{contractGid:guid}/actual")]
        public ActualContractDO GetContractVersion(Guid contractGid)
        {
            string versionXml = this.contractVersionsRepository.GetActualVersionXml(contractGid);
            if (string.IsNullOrEmpty(versionXml))
            {
                throw new HttpResponseException(
                this.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { error = PortalIntegrationErrors.ContractVersionInProgress }));
            }

            string procurementXml = this.contractProcurementsRepository.GetActualProcurementXml(contractGid);
            if (string.IsNullOrEmpty(procurementXml))
            {
                throw new HttpResponseException(
                this.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { error = PortalIntegrationErrors.ExistsProcurementInProgress }));
            }

            string spendingPlanXml = this.contractSpendingPlansRepository.GetActualSpendingPlanXml(contractGid);
            if (string.IsNullOrEmpty(spendingPlanXml))
            {
                throw new HttpResponseException(
                this.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { error = PortalIntegrationErrors.ExistsSpendingPlanInProgress }));
            }

            return new ActualContractDO
            {
                ContractVersionXml = versionXml,
                ContractProcurementXml = procurementXml,
                ContractSpendingPlanXml = spendingPlanXml,
            };
        }

        [Route("{contractGid:guid}/applicationSections")]
        public IList<ProcedureApplicationSectionPVO> GetContractApplicationSections(Guid contractGid)
        {
            var contract = this.contractsRepository.Find(contractGid);

            var procedureVersion = this.procedureVersionService.CreateProcedureVersion(contract.ProcedureId);

            return procedureVersion.ProcedureVersionJson.ApplicationSections
                .Select(p => new ProcedureApplicationSectionPVO(p))
                .ToList();
        }
    }
}
