using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eumis.Data.Registrations.ViewObjects;
using Eumis.Domain.Registrations;

namespace Eumis.Data.Registrations.Repositories
{
    public interface IRegistrationsRepository : IAggregateRepository<Registration>
    {
        Registration FindByActivationCode(string activationCode);

        Registration FindByPasswordRecoveryCode(string passwordRecoveryCode);

        Registration FindByEmail(string email);

        Task<Registration> FindByEmailAsync(string email);

        IList<RegistrationsVO> GetRegistrations();

        string GetRegistrationEmailForProject(int projectId);

        IList<Tuple<int, string>> GetRegistrationEmailsForProjects(int[] projectIds);

        bool RegistrationExists(string email);

        bool ActivationCodeExists(string activationCode);

        bool PasswordRecoveryCodeExists(string passwordRecoveryCode);

        string GetEmail(int registrationId);

        string GetEmailByProject(int projectId);
    }
}
