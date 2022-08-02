using System.Collections.Generic;
using Eumis.Data.ContractRegistrations.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.ContractRegistrations.Repositories
{
    public interface IContractRegistrationNomsRepository : IEntityNomsRepository<ContractRegistration, ContractRegistrationNomVO>
    {
        IEnumerable<ContractRegistrationNomVO> GetNotAttachedContractRegistrations(
            int contractId,
            string term,
            int offset = 0,
            int? limit = null);
    }
}