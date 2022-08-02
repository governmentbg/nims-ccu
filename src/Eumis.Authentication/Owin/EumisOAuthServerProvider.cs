using Autofac;
using Autofac.Integration.Owin;
using Eumis.Authentication.AccessContexts;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Registrations;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eumis.Authentication.Owin
{
    internal class EumisOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private const string REGISTRATION_WITH_EMAIL_SCOPE_PREFIX = "reg:";
        private const string CONTRACT_REGISTRATION_WITH_EMAIL_SCOPE_PREFIX = "contractreg:";
        private const string ACCESS_CODE_SCOPE_PREFIX = "accesscode:";
        private const string EXTERNAL_SYSTEM_SCOPE_PREFIX = "external:";

        private IDictionary<string, string> clients;
        private IDictionary<string, string> externalClients;
        private IDictionary<string, string> externalClientProperties;

        public EumisOAuthServerProvider(string clientsString, string externalClientsString)
        {
            if (string.IsNullOrEmpty(clientsString))
            {
                this.clients = new Dictionary<string, string>();
            }
            else
            {
                this.clients = clientsString
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Split(','))
                    .ToDictionary(c => c[0], c => c[1]);
            }

            if (string.IsNullOrEmpty(externalClientsString))
            {
                this.externalClients = new Dictionary<string, string>();
                this.externalClientProperties = new Dictionary<string, string>();
            }
            else
            {
                var externalClients = externalClientsString
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Split(','));

                this.externalClients = externalClients.ToDictionary(c => c[0], c => c[1]);
                this.externalClientProperties = externalClients.ToDictionary(c => c[0], c => c[2]);
            }

            if (this.clients.Keys
                .Intersect(this.externalClients.Keys)
                .Any())
            {
                throw new ArgumentException("internal and external clients should not have duplicated clientIds");
            }
        }

        public override async Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            if (context.ClientId == null ||
                (!this.clients.ContainsKey(context.ClientId) &&
                !this.externalClients.ContainsKey(context.ClientId)))
            {
                // only known clients(the portal) can use ClientCredentials
                context.SetError(EumisOAuthErrors.InvalidClientId);
                return;
            }

            if (context.Scope.Count != 1)
            {
                context.SetError(EumisOAuthErrors.UnknownScopeFormat);
                return;
            }

            bool isRegistration = context.Scope[0].StartsWith(REGISTRATION_WITH_EMAIL_SCOPE_PREFIX);
            bool isContractRegistration = context.Scope[0].StartsWith(CONTRACT_REGISTRATION_WITH_EMAIL_SCOPE_PREFIX);
            bool isAccessCode = context.Scope[0].StartsWith(ACCESS_CODE_SCOPE_PREFIX);
            bool isExternalSystem = context.Scope[0].StartsWith(EXTERNAL_SYSTEM_SCOPE_PREFIX);

            bool isInternalClient = this.clients.ContainsKey(context.ClientId);
            bool isExternalClient = this.externalClients.ContainsKey(context.ClientId);

            if (!(isInternalClient && (isRegistration || isContractRegistration || isAccessCode)) &&
                !(isExternalClient && isExternalSystem))
            {
                context.SetError(EumisOAuthErrors.ClientScopeMismatch);
                return;
            }

            AuthenticationProperties properties = null;
            if (isRegistration || isContractRegistration)
            {
                string regString = context.Scope[0].Substring(isRegistration ?
                    REGISTRATION_WITH_EMAIL_SCOPE_PREFIX.Length :
                    CONTRACT_REGISTRATION_WITH_EMAIL_SCOPE_PREFIX.Length);

                int colonIndex = regString.IndexOf(':');

                if (colonIndex <= 0 ||
                    colonIndex == regString.Length - 1)
                {
                    context.SetError(EumisOAuthErrors.UnknownScopeFormat);
                    return;
                }

                string email = regString.Substring(0, colonIndex);

                string escapedPassword = regString.Substring(colonIndex + 1, regString.Length - (colonIndex + 1));

                // the password may contain characters outside the range allowed for the scope parameter and should be Escaped/Unescaped
                string password = Uri.UnescapeDataString(escapedPassword);

                if (isRegistration)
                {
                    // do not dispose the AutofacLifetimeScope here
                    // it will be disposed at the end of the request
                    var registrationsRepository = context.OwinContext.GetAutofacLifetimeScope().Resolve<IRegistrationsRepository>();
                    Registration registration = await registrationsRepository.FindByEmailAsync(email);

                    if (registration == null)
                    {
                        context.SetError(EumisOAuthErrors.Unauthorized);
                        return;
                    }

                    if (!registration.VerifyPassword(password))
                    {
                        context.SetError(EumisOAuthErrors.Unauthorized);
                        return;
                    }

                    if (!registration.IsActive)
                    {
                        context.SetError(EumisOAuthErrors.RegistrationNotActivated);
                        return;
                    }

                    properties = AuthExtensions.CreateRegistrationAuthenticationProperties(registration.RegistrationId, registration.Email);
                }
                else
                {
                    // do not dispose the AutofacLifetimeScope here
                    // it will be disposed at the end of the request
                    var contractRegistrationsRepository = context.OwinContext.GetAutofacLifetimeScope().Resolve<IContractRegistrationsRepository>();
                    ContractRegistration contractRegistration = await contractRegistrationsRepository.FindByEmailAsync(email);

                    if (contractRegistration == null)
                    {
                        context.SetError(EumisOAuthErrors.Unauthorized);
                        return;
                    }

                    if (!contractRegistration.VerifyPassword(password))
                    {
                        context.SetError(EumisOAuthErrors.Unauthorized);
                        return;
                    }

                    if (!contractRegistration.IsActive)
                    {
                        context.SetError(EumisOAuthErrors.RegistrationNotActivated);
                        return;
                    }

                    properties = AuthExtensions.CreateContractRegistrationAuthenticationProperties(contractRegistration.ContractRegistrationId, contractRegistration.Email);
                }
            }
            else if (isAccessCode)
            {
                string regString = context.Scope[0].Substring(ACCESS_CODE_SCOPE_PREFIX.Length);

                int firstColonIndex = regString.IndexOf(':');

                if (firstColonIndex <= 0 ||
                    firstColonIndex == regString.Length - 1)
                {
                    context.SetError(EumisOAuthErrors.UnknownScopeFormat);
                    return;
                }

                int secondColonIndex = regString.IndexOf(':', firstColonIndex + 1);

                if (secondColonIndex <= 0 ||
                    secondColonIndex == regString.Length - 1)
                {
                    context.SetError(EumisOAuthErrors.UnknownScopeFormat);
                    return;
                }

                string email = regString.Substring(0, firstColonIndex);
                string code = regString.Substring(firstColonIndex + 1, secondColonIndex - firstColonIndex - 1);
                string escapedRegNumber = regString.Substring(secondColonIndex + 1, regString.Length - (secondColonIndex + 1));

                // the regNumber may contain characters outside the range allowed for the scope parameter and should be Escaped/Unescaped
                string regNumber = Uri.UnescapeDataString(escapedRegNumber);

                // do not dispose the AutofacLifetimeScope here
                // it will be disposed at the end of the request
                var contractsRepository = context.OwinContext.GetAutofacLifetimeScope().Resolve<IContractsRepository>();
                var accessCode = await contractsRepository.GetContractAccessCodeAsync(email, regNumber, code);

                if (accessCode == null)
                {
                    context.SetError(EumisOAuthErrors.Unauthorized);
                    return;
                }

                if (!accessCode.IsActive)
                {
                    context.SetError(EumisOAuthErrors.AccessCodeNotActive);
                    return;
                }

                properties = AuthExtensions.CreateContractAccessCodeAuthenticationProperties(accessCode.ContractAccessCodeId, accessCode.ContractId, accessCode.Email);
            }
            else if (isExternalSystem)
            {
                string property = context.Scope[0].Substring(EXTERNAL_SYSTEM_SCOPE_PREFIX.Length);

                if (string.IsNullOrEmpty(property))
                {
                    context.SetError(EumisOAuthErrors.UnknownScopeFormat);
                    return;
                }

                if (this.externalClientProperties[context.ClientId] != property)
                {
                    context.SetError(EumisOAuthErrors.ClientScopeMismatch);
                    return;
                }

                properties = AuthExtensions.CreateExternalSystemAuthenticationProperties(property);
            }
            else
            {
                context.SetError(EumisOAuthErrors.UnknownScopeFormat);
                return;
            }

            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            context.Validated(new AuthenticationTicket(oAuthIdentity, properties));
            context.Request.Context.Authentication.SignIn(oAuthIdentity);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (context.TryGetFormCredentials(out clientId, out clientSecret) ||
                context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                if (this.clients.ContainsKey(clientId) && this.clients[clientId] == clientSecret)
                {
                    context.Validated(clientId);
                    return Task.FromResult<object>(null);
                }
                else if (this.externalClients.ContainsKey(clientId) && this.externalClients[clientId] == clientSecret)
                {
                    context.Validated(clientId);
                    return Task.FromResult<object>(null);
                }
                else
                {
                    context.SetError(EumisOAuthErrors.UnauthorizedClient);
                    return Task.FromResult<object>(null);
                }
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}
