using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.Regix.Contracts.Bulstat;
using Eumis.Data.Regix.Contracts.Grao;
using Eumis.Data.Regix.Contracts.Mp;
using Eumis.Data.Regix.Contracts.Mvr;
using Eumis.Data.Regix.Contracts.Tr;
using Eumis.Web.Api.Regix.DataObjects;

namespace Eumis.Web.Api.Regix
{
    [RoutePrefix("api/regix")]
    public class RegixController : ApiController
    {
        private IRegixRestApiCommunicator regixRestApiCommunicator;
        private IAuthorizer authorizer;

        public RegixController(
            IRegixRestApiCommunicator regixRestApiCommunicator,
            IAuthorizer authorizer)
        {
            this.regixRestApiCommunicator = regixRestApiCommunicator;
            this.authorizer = authorizer;
        }

        [HttpPost]
        [Route("personValid")]
        public ValidPersonResponse GetGraoNBDPersonValid(PersonDO person)
        {
            this.authorizer.AssertCanDo(RegixActions.View);

            return this.regixRestApiCommunicator.GetValidPerson(person.PersonalBulstat);
        }

        [HttpPost]
        [Route("personalIdentity")]
        public PersonalIdentityInfoResponse GetPersonalIdentity(PersonalIdentityDO identity)
        {
            this.authorizer.AssertCanDo(RegixActions.View);

            return this.regixRestApiCommunicator.GetPersonalIdentity(identity.PersonalBulstat, identity.IdentityDocumentNumber);
        }

        [HttpPost]
        [Route("actualState")]
        public ActualStateResponse GetActualState(CompanyDO company)
        {
            this.authorizer.AssertCanDo(RegixActions.View);

            return this.regixRestApiCommunicator.GetActualState(company.Uic);
        }

        [HttpPost]
        [Route("stateOfPlay")]
        public StateOfPlay GetStateOfPlay(CompanyDO company)
        {
            this.authorizer.AssertCanDo(RegixActions.View);

            return this.regixRestApiCommunicator.GetStateOfPlay(company.Uic);
        }

        [HttpPost]
        [Route("npoRegistrationInfo")]
        public NPODetailsResponse GetNPORegistrationInfo(CompanyDO company)
        {
            this.authorizer.AssertCanDo(RegixActions.View);

            return this.regixRestApiCommunicator.GetNPORegistrationInfo(company.Uic);
        }
    }
}
