using Eumis.IntegrationRegiX.Host.Auth;
using Eumis.IntegrationRegiX.Host.Helpers;
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
    public class RegixCommunicator : BaseCommunicator, IRegixCommunicator
    {
        public RegixCommunicator(IRegixCallContext regixCallContext)
            : base(regixCallContext)
        {
        }

        public SearchByIdentifierResponse SearchByIdentifier(string id, string procedureCode = null)
        {
            SearchByIdentifierRequest sr = new SearchByIdentifierRequest()
            {
                DateFrom = DateTime.Now.AddYears(-1),
                DateTo = DateTime.Now.AddYears(1),
                Identifier = id,
                IdentifierType = IdentifierType.EGN,
            };

            var xmlResponse = this.ExecuteRequest(RegixAction.SearchByIdentifier.Value, sr, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<SearchByIdentifierResponse>();
        }

        public ValidPersonResponse ValidPersonSearch(string personalBulstat, string procedureCode = null)
        {
            ValidPersonRequest request = new ValidPersonRequest() { EGN = personalBulstat };

            var xmlResponse = this.ExecuteRequest(RegixAction.ValidPersonSearch.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<ValidPersonResponse>();
        }

        public ValidUICResponse ValidUicInfo(string uic, string procedureCode = null)
        {
            var request = new ValidUICRequest() { UIC = uic };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetValidUICInfo.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<ValidUICResponse>();
        }

        public StateOfPlay GetStateOfPlay(string uic, string procedureCode = null)
        {
            var request = new GetStateOfPlayRequest() { UIC = uic };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetStateOfPlay.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<StateOfPlay>();
        }

        public ObligationResponse GetObligatedPersons(string value, EikTypeTypeRequest type, ushort treshold, string procedureCode = null)
        {
            var request = new ObligationRequest() { Identity = new IdentityTypeRequest() { ID = value, TYPE = type }, Threshold = treshold };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetObligatedPersons.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<ObligationResponse>();
        }

        public PersonDataResponse PersonDataSearch(string personalBulstat, string procedureCode = null)
        {
            var request = new PersonDataRequest() { EGN = personalBulstat };

            var xmlResponse = this.ExecuteRequest(RegixAction.PersonDataSearch.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<PersonDataResponse>();
        }

        public PersonalIdentityInfoResponse GetPersonalIdentity(string personalBulstat, string identityDocumentNumber, string procedureCode = null)
        {
            var request = new PersonalIdentityInfoRequest() { EGN = personalBulstat, IdentityDocumentNumber = identityDocumentNumber };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetPersonalIdentity.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<PersonalIdentityInfoResponse>();
        }

        public ActualStateResponse GetActualState(string uic, string procedureCode = null)
        {
            var request = new ActualStateRequest() { UIC = uic };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetActualState.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<ActualStateResponse>();
        }

        public NPODetailsResponse GetNPORegistrationInfo(string uic, string procedureCode = null)
        {
            var request = new NPODetailsRequest() { Bulstat = uic };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetNPORegistrationInfo.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<NPODetailsResponse>();
        }

        public CheckObligationsResponse CheckObligations(string id, string procedureCode)
        {
            var request = new CheckObligationsRequest() { Identifier = id };

            var xmlResponse = this.ExecuteRequest(RegixAction.CheckObligations.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<CheckObligationsResponse>();
        }

        public RegisteredAnimalsByCategoryResponse GetRegisteredAnimalsByCategory(string personalBulstat, string procedureCode)
        {
            var request = new RegisteredAnimalsByCategoryRequest() { EGN = personalBulstat, ValidDate = DateTime.Now };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetRegisteredAnimalsByCategory.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<RegisteredAnimalsByCategoryResponse>();
        }

        public RegisteredAnimalsInOEZByCategoryResponse GetRegisteredAnimalsInOEZByCategory(string id, string procedureCode)
        {
            var request = new RegisteredAnimalsInOEZByCategoryRequest() { Identifier = id, ValidDate = DateTime.Now };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetRegisteredAnimalsInOEZByCategory.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<RegisteredAnimalsInOEZByCategoryResponse>();
        }

        public PenalProvisionForPeriodResponse GetPenalProvisionForPeriod(string id, DateTime dateFrom, DateTime dateTo, string procedureCode)
        {
            var request = new PenalProvisionForPeriodRequest()
            {
                IntruderIdentifier = id,
                DateFrom = dateFrom,
                DateTo = dateTo,
            };

            var xmlResponse = this.ExecuteRequest(RegixAction.GetPenalProvisionForPeriod.Value, request, procedureCode);
            return xmlResponse.DeserializeFromXmlElement<PenalProvisionForPeriodResponse>();
        }
    }
}
