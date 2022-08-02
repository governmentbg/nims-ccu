using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.Registrations.ViewObjects;
using Eumis.Web.Api.Registrations.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Registrations.Controllers
{
    [RoutePrefix("api/registrations")]
    public class RegistrationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRegistrationsRepository registrationsRepository;
        private IAuthorizer authorizer;

        public RegistrationsController(
            IUnitOfWork unitOfWork,
            IRegistrationsRepository registrationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.registrationsRepository = registrationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<RegistrationsVO> GetRegistrations()
        {
            this.authorizer.AssertCanDo(RegistrationListActions.Search);

            return this.registrationsRepository.GetRegistrations();
        }

        [Route("{registrationId:int}")]
        public RegistrationDO GetRegistration(int registrationId)
        {
            this.authorizer.AssertCanDo(RegistrationActions.View, registrationId);

            var registration = this.registrationsRepository.Find(registrationId);

            return new RegistrationDO(registration);
        }
    }
}
