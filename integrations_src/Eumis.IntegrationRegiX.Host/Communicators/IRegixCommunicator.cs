using Eumis.RegiX.Rio.AVBULSTAT;
using Eumis.RegiX.Rio.AVTR;
using Eumis.RegiX.Rio.BABHZhS;
using Eumis.RegiX.Rio.DAEU;
using Eumis.RegiX.Rio.GPP;
using Eumis.RegiX.Rio.GRAO;
using Eumis.RegiX.Rio.MP;
using Eumis.RegiX.Rio.MVR;
using Eumis.RegiX.Rio.NRA;
using Eumis.RegiX.Rio.REZMA;
using System;

namespace Eumis.IntegrationRegiX.Host.Communicators
{
    public interface IRegixCommunicator
    {
        ValidPersonResponse ValidPersonSearch(string personalBulstat, string procedureCode);

        ValidUICResponse ValidUicInfo(string uic, string procedureCode);

        SearchByIdentifierResponse SearchByIdentifier(string id, string procedureCode);

        StateOfPlay GetStateOfPlay(string uic, string procedureCode);

        ObligationResponse GetObligatedPersons(string value, EikTypeTypeRequest type, ushort treshold, string procedureCode);

        PersonDataResponse PersonDataSearch(string personalBulstat, string procedureCode);

        PersonalIdentityInfoResponse GetPersonalIdentity(string personalBulstat, string identityDocumentNumber, string procedureCode);

        NPODetailsResponse GetNPORegistrationInfo(string uic, string procedureCode);

        ActualStateResponse GetActualState(string uic, string procedureCode);

        CheckObligationsResponse CheckObligations(string id, string procedureCode = null);

        RegisteredAnimalsByCategoryResponse GetRegisteredAnimalsByCategory(string personalBulstat, string procedureCode);

        RegisteredAnimalsInOEZByCategoryResponse GetRegisteredAnimalsInOEZByCategory(string id, string procedureCode);

        PenalProvisionForPeriodResponse GetPenalProvisionForPeriod(string id, DateTime dateFrom, DateTime dateTo, string procedureCode);
    }
}
