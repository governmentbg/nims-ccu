using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.ContractRegistrations.ViewObjects;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractRegistrations.Controllers
{
    [RoutePrefix("api/contractRegistrations")]
    public class ContractRegistrationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractRegistrationsRepository contractRegistrationsRepository;
        private IAuthorizer authorizer;

        public ContractRegistrationsController(
            IUnitOfWork unitOfWork,
            IContractRegistrationsRepository contractRegistrationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.contractRegistrationsRepository = contractRegistrationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ContractRegistrationsVO> GetContractRegistrations(
            string email = null,
            string uin = null,
            string firstName = null,
            string lastName = null,
            string phone = null,
            int? contractId = null)
        {
            this.authorizer.AssertCanDo(ContractRegistrationListActions.Search);

            return this.contractRegistrationsRepository.GetContractRegistrations(email, uin, firstName, lastName, phone, contractId);
        }

        [Route("{contractRegistrationId:int}")]
        public ContractRegistrationDO GetContractRegistration(int contractRegistrationId)
        {
            this.authorizer.AssertCanDo(ContractRegistrationListActions.Search);

            var contractRegistration = this.contractRegistrationsRepository.Find(contractRegistrationId);

            return new ContractRegistrationDO(contractRegistration);
        }

        [HttpPut]
        [Route("{contractRegistrationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractRegistrations.Edit), IdParam = "contractRegistrationId")]
        public void UpdateContractRegistration(int contractRegistrationId, ContractRegistrationDO contractRegistration)
        {
            var contractId = this.contractRegistrationsRepository.GetContractId(contractRegistrationId);

            this.authorizer.AssertCanDo(ContractRegistrationActions.Edit, contractId);

            var registration = this.contractRegistrationsRepository.Find(contractRegistrationId);

            registration.UpdateContractRegistration(
                contractRegistration.FirstName,
                contractRegistration.LastName,
                contractRegistration.Phone);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("isUnique")]
        public bool IsUnique(string email)
        {
            this.authorizer.AssertCanDo(ContractRegistrationListActions.Search);

            return this.contractRegistrationsRepository.IsUnique(email);
        }
    }
}
