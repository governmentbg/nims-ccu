using Eumis.Data.Regix.Contracts.Bulstat;
using Eumis.Data.Regix.Contracts.Grao;
using Eumis.Data.Regix.Contracts.Mp;
using Eumis.Data.Regix.Contracts.Mvr;
using Eumis.Data.Regix.Contracts.Tr;

namespace Eumis.ApplicationServices.Communicators
{
    public interface IRegixRestApiCommunicator
    {
        ValidPersonResponse GetValidPerson(string personalBulstat);

        PersonalIdentityInfoResponse GetPersonalIdentity(string personalBulstat, string documentIdNumber);

        ActualStateResponse GetActualState(string uic);

        StateOfPlay GetStateOfPlay(string uic);

        NPODetailsResponse GetNPORegistrationInfo(string uic);
    }
}
