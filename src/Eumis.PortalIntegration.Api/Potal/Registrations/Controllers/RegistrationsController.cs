using Eumis.Authentication.AccessContexts;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Registrations;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Log.Owin;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects;
using Microsoft.Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.Controllers
{
    public class RegistrationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRegistrationsRepository registrationsRepository;
        private IEmailsRepository emailsRepository;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;

        public RegistrationsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IRegistrationsRepository registrationsRepository,
            IEmailsRepository emailsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.registrationsRepository = registrationsRepository;
            this.emailsRepository = emailsRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/registrations")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogPortalGroups.Registrations.Create))]
        public void CreateRegistration(RegistrationDO newRegistration)
        {
            if (string.IsNullOrEmpty(newRegistration.Email))
            {
                throw new Exception("Email field is mandatory");
            }

            if (this.registrationsRepository.RegistrationExists(newRegistration.Email))
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.RegistrationEmailExists }));
            }

            Registration reg = new Registration(newRegistration.Email, newRegistration.FirstName, newRegistration.LastName, newRegistration.Phone);

            this.registrationsRepository.Add(reg);

            this.unitOfWork.Save();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/registrations/canActivate")]
        public bool CanActivateRegistration(RegistrationActivationDO activateRegistration)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            return this.registrationsRepository.ActivationCodeExists(activateRegistration.ActivationCode);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/registrations/activate")]
        public object ActivateRegistration(RegistrationActivationDO activateRegistration)
        {
            IOwinContext owinContext = this.Request.GetOwinContext();

            owinContext.SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogPortalGroups.Registrations.Activate), null, null, activateRegistration.ActivationCode, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var reg = this.registrationsRepository.FindByActivationCode(activateRegistration.ActivationCode);

                if (reg == null)
                {
                    throw new HttpResponseException(
                        this.Request.CreateResponse(
                            HttpStatusCode.BadRequest,
                            new { error = PortalIntegrationErrors.InvalidActivationCode }));
                }

                reg.Activate(activateRegistration.Password);

                this.unitOfWork.Save();
                transaction.Commit();

                string accessToken = owinContext.CreateOAuthBearerToken(
                    AuthExtensions.CreateRegistrationAuthenticationProperties(reg.RegistrationId, reg.Email));

                return new
                {
                    accessToken = accessToken,
                };
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Transaction]
        [Route("api/registrations/startPasswordRecovery")]
        public void StartPasswordRecovery(StartPasswordRecoveryDO startPasswordRecovery)
        {
            if (string.IsNullOrEmpty(startPasswordRecovery.Email))
            {
                throw new Exception("Email field is mandatory");
            }

            var reg = this.registrationsRepository.FindByEmail(startPasswordRecovery.Email);

            if (reg == null)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.RegistrationEmailDoesNotExist }));
            }

            reg.SetPasswordRecoveryCode();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/registrations/canRecoverPassword")]
        public bool CanRecoverPassword(RecoverPasswordDO recoverPassword)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            return this.registrationsRepository.PasswordRecoveryCodeExists(recoverPassword.PasswordRecoveryCode);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/registrations/recoverPassword")]
        public void RecoverPassword(RecoverPasswordDO recoverPassword)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogPortalGroups.Registrations.RecoverPassword), null, null, recoverPassword.PasswordRecoveryCode, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var reg = this.registrationsRepository.FindByPasswordRecoveryCode(recoverPassword.PasswordRecoveryCode);

                if (reg == null)
                {
                    throw new HttpResponseException(
                        this.Request.CreateResponse(
                            HttpStatusCode.BadRequest,
                            new { error = PortalIntegrationErrors.InvalidPasswordRecoveryCode }));
                }

                if (!reg.IsActive)
                {
                    // activate the user in case he forgot to use the activation email
                    // and started password recovery instead
                    reg.Activate(recoverPassword.NewPassword);
                }

                reg.RecoverPassword(recoverPassword.NewPassword);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        [HttpGet]
        [Route("api/registration/info")]
        public RegistrationDO GetRegistrationInfo()
        {
            var reg = this.registrationsRepository.Find(this.accessContext.RegistrationId);

            return new RegistrationDO(reg);
        }

        [HttpPost]
        [Route("api/registration/info")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogPortalGroups.Registrations.UpdateRegistrationInfo))]
        public RegistrationDO UpdateRegistrationInfo(RegistrationDO updatedReg)
        {
            var reg = this.registrationsRepository.FindForUpdate(this.accessContext.RegistrationId, updatedReg.Version);

            reg.UpdateInfo(updatedReg.FirstName, updatedReg.LastName, updatedReg.Phone);

            this.unitOfWork.Save();

            return new RegistrationDO(reg);
        }

        [HttpPost]
        [Route("api/registration/password")]
        public void ChangeCurrentUserPassword(PasswordsDO passwords)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogPortalGroups.Registrations.ChangeCurrentUserPassword), null, null, null, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var reg = this.registrationsRepository.Find(this.accessContext.RegistrationId);

                if (!reg.TryChangePassword(passwords.OldPassword, passwords.NewPassword))
                {
                    throw new HttpResponseException(
                        this.Request.CreateResponse(
                            HttpStatusCode.BadRequest,
                            new { error = PortalIntegrationErrors.WrongOldPassword }));
                }

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }
    }
}
