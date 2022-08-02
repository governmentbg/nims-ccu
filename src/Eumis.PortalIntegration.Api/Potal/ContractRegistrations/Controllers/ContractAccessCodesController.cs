using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.ContractRegistrations.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/accesscodes")]
    public class ContractAccessCodesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractAccessCodesRepository contractAccessCodesRepository;
        private IContractsRepository contractsRepository;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;

        public ContractAccessCodesController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractAccessCodesRepository contractAccessCodesRepository,
            IContractsRepository contractsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.contractAccessCodesRepository = contractAccessCodesRepository;
            this.contractsRepository = contractsRepository;
        }

        [Route("")]
        public PagePVO<ContractAccessCodePVO> GetContractAccessCodes(Guid contractGid, int offset = 0, int? limit = null)
        {
            // TODO check permissions for contractGid
            if (!this.accessContext.IsContractRegistration)
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            return this.contractAccessCodesRepository.GetContractAccessCodes(contractGid, offset, limit);
        }

        [Route("{accessCodeGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractAccessCodeDO GetContractAccessCode(Guid contractGid, Guid accessCodeGid)
        {
            // TODO check permissions for contractGid
            if (!this.accessContext.IsContractRegistration)
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var accessCode = this.contractAccessCodesRepository.Find(accessCodeGid);

            return new ContractAccessCodeDO(accessCode);
        }

        [HttpPost]
        [Route("")]
        public ContractAccessCodeDO CreateContractAccessCode(Guid contractGid, ContractAccessCodeDO accessCodeDO)
        {
            // TODO check permissions for contractGid
            if (!this.accessContext.IsContractRegistration)
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var usedEmails = this.contractsRepository.GetContractAccessCodesEmails(contractGid);

            if (usedEmails.Contains(accessCodeDO.Email))
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.AccessCodeEmailNotUnique }));
            }

            var contractId = this.contractsRepository.GetContractId(contractGid);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                ContractAccessCode newAccessCode = new ContractAccessCode(
                    contractId,
                    accessCodeDO.FirstName,
                    accessCodeDO.LastName,
                    accessCodeDO.Position,
                    accessCodeDO.Email,
                    accessCodeDO.Identifier,
                    accessCodeDO.IsActive,
                    accessCodeDO.Permissions.CanReadContracts,
                    accessCodeDO.Permissions.CanReadProcurements,
                    accessCodeDO.Permissions.CanWriteProcurements,
                    accessCodeDO.Permissions.CanReadTechnicalPlan,
                    accessCodeDO.Permissions.CanWriteTechnicalPlan,
                    accessCodeDO.Permissions.CanReadFinancialPlan,
                    accessCodeDO.Permissions.CanWriteFinancialPlan,
                    accessCodeDO.Permissions.CanReadMicrodata,
                    accessCodeDO.Permissions.CanWriteMicrodata,
                    accessCodeDO.Permissions.CanReadPaymentRequest,
                    accessCodeDO.Permissions.CanWritePaymentRequest,
                    accessCodeDO.Permissions.CanReadCommunication,
                    accessCodeDO.Permissions.CanReadSpendingPlan,
                    accessCodeDO.Permissions.CanWriteSpendingPlan);

                this.contractAccessCodesRepository.Add(newAccessCode);

                this.unitOfWork.Save();

                this.actionLogger.LogAction(
                    typeof(ActionLogPortalGroups.ContractRegistrations.Edit.AccessCodes.Create),
                    this.accessContext.ContractRegistrationId,
                    null,
                    accessCodeDO,
                    newAccessCode);

                transaction.Commit();

                return new ContractAccessCodeDO(newAccessCode);
            }
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{accessCodeGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractAccessCodeDO UpdateContractAccessCode(Guid contractGid, Guid accessCodeGid, ContractAccessCodeDO accessCodeDO)
        {
            // TODO check permissions for contractGid
            if (!this.accessContext.IsContractRegistration)
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var accessCode = this.contractAccessCodesRepository.FindForUpdate(accessCodeGid, accessCodeDO.Version);

            accessCode.UpdateAttributes(
                accessCodeDO.FirstName,
                accessCodeDO.LastName,
                accessCodeDO.Position,
                accessCodeDO.Identifier,
                accessCodeDO.IsActive,
                accessCodeDO.Permissions.CanReadContracts,
                accessCodeDO.Permissions.CanReadProcurements,
                accessCodeDO.Permissions.CanWriteProcurements,
                accessCodeDO.Permissions.CanReadTechnicalPlan,
                accessCodeDO.Permissions.CanWriteTechnicalPlan,
                accessCodeDO.Permissions.CanReadFinancialPlan,
                accessCodeDO.Permissions.CanWriteFinancialPlan,
                accessCodeDO.Permissions.CanReadMicrodata,
                accessCodeDO.Permissions.CanWriteMicrodata,
                accessCodeDO.Permissions.CanReadPaymentRequest,
                accessCodeDO.Permissions.CanWritePaymentRequest,
                accessCodeDO.Permissions.CanReadCommunication,
                accessCodeDO.Permissions.CanReadSpendingPlan,
                accessCodeDO.Permissions.CanWriteSpendingPlan);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractRegistrations.Edit.AccessCodes.Create),
                this.accessContext.ContractRegistrationId,
                accessCode.ContractAccessCodeId,
                accessCodeDO,
                accessCode);

            return new ContractAccessCodeDO(accessCode);
        }
    }
}
