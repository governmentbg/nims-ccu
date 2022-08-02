using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.ContractRegistrations.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractRegistrations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractRegistrations")]
    public class ContractRegistrationNomsController : EntityNomsController<ContractRegistration, ContractRegistrationNomVO>
    {
        private IContractRegistrationNomsRepository contractRegistrationNomsRepository;

        public ContractRegistrationNomsController(IContractRegistrationNomsRepository contractRegistrationNomsRepository)
            : base(contractRegistrationNomsRepository)
        {
            this.contractRegistrationNomsRepository = contractRegistrationNomsRepository;
        }

        [Route("")]
        public IEnumerable<ContractRegistrationNomVO> GetNotAttachedContractRegistrations(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractRegistrationNomsRepository.GetNotAttachedContractRegistrations(contractId, term, offset, limit);
        }
    }
}
