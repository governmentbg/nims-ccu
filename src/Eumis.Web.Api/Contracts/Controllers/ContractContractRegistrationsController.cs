using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Contracts.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/registrations")]
    public class ContractContractRegistrationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractRegistrationsRepository contractRegistrationsRepository;

        public ContractContractRegistrationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractRegistrationsRepository contractRegistrationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractRegistrationsRepository = contractRegistrationsRepository;
        }

        [HttpGet]
        [Route("")]
        public IList<ContractContractRegistrationsVO> GetContractContractRegistrations(int contractId)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.View, contractId);

            return this.contractsRepository.GetContractContractRegistrations(contractId);
        }

        [HttpGet]
        [Route("{contractsContractRegistrationId:int}")]
        public ContractContractRegistrationDO GetContractRegistration(int contractId, int contractsContractRegistrationId)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.View, contractId);

            var contract = this.contractsRepository.Find(contractId);

            var contractContractRegistration = contract.FindContractContractRegistration(contractsContractRegistrationId);

            var contractRegistration = this.contractRegistrationsRepository.Find(contractContractRegistration.ContractRegistrationId);

            return new ContractContractRegistrationDO(contractRegistration, contractContractRegistration, contract.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractContractRegistrationDO NewContractRegistration(int contractId)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.CreateOrAttachRegistration, contractId);

            var version = this.contractsRepository.GetVersion(contractId);

            return new ContractContractRegistrationDO(version);
        }

        [HttpPost]
        [Route("create")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Registrations.CreateAndAttach), IdParam = "contractId")]
        public void CreateAndAttachContractRegistration(int contractId, ContractContractRegistrationDO contractRegistration)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.CreateOrAttachRegistration, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, contractRegistration.Version);

            ContractRegistration newContractRegistration = new ContractRegistration(
                contractRegistration.ContractRegistration.Email,
                contractRegistration.ContractRegistration.Uin,
                contractRegistration.ContractRegistration.UinType.Value,
                contractRegistration.ContractRegistration.FirstName,
                contractRegistration.ContractRegistration.LastName,
                contractRegistration.ContractRegistration.Phone);

            this.contractRegistrationsRepository.Add(newContractRegistration);

            this.unitOfWork.Save();

            contract.AddContractRegistration(
                newContractRegistration.ContractRegistrationId,
                this.accessContext.UserId,
                contractRegistration.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Registrations.Attach), IdParam = "contractId", ChildIdParam = "contractRegistration.ContractRegistration.ContractRegistrationId")]
        public void AttachContractRegistration(int contractId, ContractContractRegistrationDO contractRegistration)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.CreateOrAttachRegistration, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, contractRegistration.Version);

            contract.AddContractRegistration(
                contractRegistration.ContractRegistration.ContractRegistrationId,
                this.accessContext.UserId,
                contractRegistration.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{contractsContractRegistrationId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Registrations.Activate), IdParam = "contractId", ChildIdParam = "contractsContractRegistrationId")]
        public void ActivateContractRegistration(int contractId, int contractsContractRegistrationId, string version)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.Activate, contractId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contract = this.contractsRepository.FindForUpdate(contractId, vers);

            contract.ActivateContractRegistration(contractsContractRegistrationId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{contractsContractRegistrationId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Registrations.Deactivate), IdParam = "contractId", ChildIdParam = "contractsContractRegistrationId")]
        public void DeactivateContractRegistration(int contractId, int contractsContractRegistrationId, string version)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.Deactivate, contractId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contract = this.contractsRepository.FindForUpdate(contractId, vers);

            contract.DeactivateContractRegistration(contractsContractRegistrationId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{contractsContractRegistrationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Registrations.Edit), IdParam = "contractId", ChildIdParam = "contractsContractRegistrationId")]
        public void UpdateContractRegistration(int contractId, int contractsContractRegistrationId, string version, ContractContractRegistrationDO contractsContractRegistration)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.Edit, contractId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contract = this.contractsRepository.FindForUpdate(contractId, vers);

            contract.UpdateContractRegistration(
                contractsContractRegistrationId,
                contractsContractRegistration.File.Key);

            this.unitOfWork.Save();
        }
    }
}
