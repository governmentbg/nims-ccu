using Eumis.Authentication.AccessContexts;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Log.Owin;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.ContractRegistrations.DataObjects;
using Microsoft.Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.Controllers
{
    public class ContractRegistrationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractRegistrationsRepository contractRegistrationsRepository;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;

        public ContractRegistrationsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractRegistrationsRepository contractRegistrationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.contractRegistrationsRepository = contractRegistrationsRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/contractregs/canActivate")]
        public bool CanActivateContractRegistration(ContractRegistrationActivationDO activateContractRegistration)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            return this.contractRegistrationsRepository.ActivationCodeExists(activateContractRegistration.ActivationCode);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/contractregs/activate")]
        public object ActivateContractRegistration(ContractRegistrationActivationDO activateContractRegistration)
        {
            IOwinContext owinContext = this.Request.GetOwinContext();

            owinContext.SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogPortalGroups.Registrations.Activate), null, null, activateContractRegistration.ActivationCode, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var contractReg = this.contractRegistrationsRepository.FindByActivationCode(activateContractRegistration.ActivationCode);

                if (contractReg == null)
                {
                    throw new HttpResponseException(
                        this.Request.CreateResponse(
                            HttpStatusCode.BadRequest,
                            new { error = PortalIntegrationErrors.InvalidActivationCode }));
                }

                contractReg.Activate(activateContractRegistration.Password);

                this.unitOfWork.Save();
                transaction.Commit();

                string accessToken = owinContext.CreateOAuthBearerToken(
                    AuthExtensions.CreateContractRegistrationAuthenticationProperties(
                        contractReg.ContractRegistrationId, contractReg.Email));

                return new
                {
                    accessToken = accessToken,
                };
            }
        }

        [HttpGet]
        [Route("api/contractregs/info")]
        public ContractRegistrationDO GetContractRegistrationInfo()
        {
            var contractReg = this.contractRegistrationsRepository.Find(this.accessContext.ContractRegistrationId);

            return new ContractRegistrationDO(contractReg);
        }

        [HttpPost]
        [Route("api/contractregs/info")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogPortalGroups.ContractRegistrations.UpdateRegistrationInfo))]
        public ContractRegistrationDO UpdateContractRegistrationInfo(ContractRegistrationDO updatedContractReg)
        {
            var contractReg = this.contractRegistrationsRepository.FindForUpdate(this.accessContext.ContractRegistrationId, updatedContractReg.Version);

            contractReg.UpdateInfo(updatedContractReg.Phone);

            this.unitOfWork.Save();

            return new ContractRegistrationDO(contractReg);
        }

        [HttpPost]
        [Route("api/contractregs/password")]
        public void ChangeCurrentUserPassword(PasswordsDO passwords)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogPortalGroups.ContractRegistrations.ChangeCurrentUserPassword), null, null, null, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var contractReg = this.contractRegistrationsRepository.Find(this.accessContext.ContractRegistrationId);

                if (!contractReg.TryChangePassword(passwords.OldPassword, passwords.NewPassword))
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

        [HttpPost]
        [AllowAnonymous]
        [Transaction]
        [Route("api/contractregs/startPasswordRecovery")]
        public void StartPasswordRecovery(StartPasswordRecoveryDO startPasswordRecovery)
        {
            if (string.IsNullOrEmpty(startPasswordRecovery.Email))
            {
                throw new Exception("Email field is mandatory");
            }

            var contractReg = this.contractRegistrationsRepository.FindByEmail(startPasswordRecovery.Email);

            if (contractReg == null)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.RegistrationEmailDoesNotExist }));
            }

            contractReg.SetPasswordRecoveryCode();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/contractregs/canRecoverPassword")]
        public bool CanRecoverPassword(RecoverPasswordDO recoverPassword)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            return this.contractRegistrationsRepository.PasswordRecoveryCodeExists(recoverPassword.PasswordRecoveryCode);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/contractregs/recoverPassword")]
        public void RecoverPassword(RecoverPasswordDO recoverPassword)
        {
            this.Request.GetOwinContext().SetContainsSensitiveData();

            this.actionLogger.LogAction(typeof(ActionLogPortalGroups.Registrations.RecoverPassword), null, null, recoverPassword.PasswordRecoveryCode, null);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var contractReg = this.contractRegistrationsRepository.FindByPasswordRecoveryCode(recoverPassword.PasswordRecoveryCode);

                if (contractReg == null)
                {
                    throw new HttpResponseException(
                        this.Request.CreateResponse(
                            HttpStatusCode.BadRequest,
                            new { error = PortalIntegrationErrors.InvalidPasswordRecoveryCode }));
                }

                if (!contractReg.IsActive)
                {
                    // activate the user in case he forgot to use the activation email
                    // and started password recovery instead
                    contractReg.Activate(recoverPassword.NewPassword);
                }

                contractReg.RecoverPassword(recoverPassword.NewPassword);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }
    }
}
