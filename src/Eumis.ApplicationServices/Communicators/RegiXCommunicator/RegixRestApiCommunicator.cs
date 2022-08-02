using Eumis.Authentication.TokenProviders;
using Eumis.Common.Auth;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.Regix.Contracts.Bulstat;
using Eumis.Data.Regix.Contracts.Grao;
using Eumis.Data.Regix.Contracts.Mp;
using Eumis.Data.Regix.Contracts.Mvr;
using Eumis.Data.Regix.Contracts.Tr;
using Eumis.Data.Users.Repositories;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.ApplicationServices.Communicators
{
    public class RegixRestApiCommunicator : IRegixRestApiCommunicator
    {
        private readonly string token;
        private readonly IActionLogger actionLogger;

        public RegixRestApiCommunicator(
            IEumisTokenProvider eumisTokenProvider,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IActionLogger actionLogger,
            IRegistrationsRepository registrationsRepository,
            IContractRegistrationsRepository contractRegistrationsRepository)
        {
            if (accessContext.IsUser)
            {
                var user = usersRepository.FindWithoutIncludes(accessContext.UserId);
                this.token = eumisTokenProvider.GenerateToken(user.Email, user.Fullname, user.UserId, user.Position ?? string.Empty);
            }

            if (accessContext.IsRegistration)
            {
                var user = registrationsRepository.FindWithoutIncludes(accessContext.RegistrationId);
                this.token = eumisTokenProvider.GenerateToken(user.Email, $"{user.FirstName} {user.LastName}", user.RegistrationId, "Кандидат" ?? string.Empty);
            }

            if (accessContext.IsContractRegistration)
            {
                var user = contractRegistrationsRepository.FindWithoutIncludes(accessContext.ContractRegistrationId);
                this.token = eumisTokenProvider.GenerateToken(user.Email, $"{user.FirstName} {user.LastName}", user.ContractRegistrationId, "Бенефициент" ?? string.Empty);
            }

            this.actionLogger = actionLogger;
        }

        public ValidPersonResponse GetValidPerson(string personalBulstat)
        {
            this.actionLogger.LogAction(typeof(ActionLogGroups.RegiXService.ValidPerson), null, null, personalBulstat, null);

            return RegixAPI.GetValidPerson(personalBulstat, this.token).ToObject<ValidPersonResponse>();
        }

        public PersonalIdentityInfoResponse GetPersonalIdentity(string personalBulstat, string documentIdNumber)
        {
            this.actionLogger.LogAction(typeof(ActionLogGroups.RegiXService.PersonalIdentity), null, null, personalBulstat, null);

            return RegixAPI.GetPersonalIdentity(personalBulstat, documentIdNumber, this.token).ToObject<PersonalIdentityInfoResponse>();
        }

        public ActualStateResponse GetActualState(string uin)
        {
            this.actionLogger.LogAction(typeof(ActionLogGroups.RegiXService.ActualState), null, null, uin, null);

            return RegixAPI.GetActualState(uin, this.token).ToObject<ActualStateResponse>();
        }

        public StateOfPlay GetStateOfPlay(string uin)
        {
            this.actionLogger.LogAction(typeof(ActionLogGroups.RegiXService.StateOfPlay), null, null, uin, null);

            return RegixAPI.GetStateOfPlay(uin, this.token).ToObject<StateOfPlay>();
        }

        public NPODetailsResponse GetNPORegistrationInfo(string uin)
        {
            this.actionLogger.LogAction(typeof(ActionLogGroups.RegiXService.NpoRegistration), null, null, uin, null);

            return RegixAPI.GetNPORegistrationInfo(uin, this.token).ToObject<NPODetailsResponse>();
        }
    }
}
