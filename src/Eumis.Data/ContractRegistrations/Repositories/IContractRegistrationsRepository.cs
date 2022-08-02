using System.Collections.Generic;
using System.Threading.Tasks;
using Eumis.Data.ContractRegistrations.ViewObjects;
using Eumis.Domain.Contracts;

namespace Eumis.Data.ContractRegistrations.Repositories
{
    public interface IContractRegistrationsRepository : IAggregateRepository<ContractRegistration>
    {
        IList<ContractRegistrationsVO> GetContractRegistrations(string email, string uin, string firstName, string lastName, string phone, int? contractId);

        bool IsUnique(string email);

        bool ActivationCodeExists(string activationCode);

        ContractRegistration FindByActivationCode(string activationCode);

        ContractRegistration FindByEmail(string email);

        ContractRegistration FindByPasswordRecoveryCode(string passwordRecoveryCode);

        bool PasswordRecoveryCodeExists(string passwordRecoveryCode);

        string GetRegistrationEmail(int contractRegistrationId);

        Task<ContractRegistration> FindByEmailAsync(string email);

        int GetContractId(int contractRegistrationId);
    }
}
