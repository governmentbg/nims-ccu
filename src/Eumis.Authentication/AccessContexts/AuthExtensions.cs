using Eumis.Common.Auth;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Eumis.Authentication.AccessContexts
{
    public static class AuthExtensions
    {
        internal const string AuthPropertiesOwinEnvironmentKey = "eumis.AuthProperties";
        internal const string UserIdAuthPropertiesKey = "userId";
        internal const string UsernameAuthPropertiesKey = "username";
        internal const string RegistrationIdAuthPropertiesKey = "registrationId";
        internal const string RegistrationEmailAuthPropertiesKey = "registrationEmail";
        internal const string ContractRegistrationIdAuthPropertiesKey = "contractRegistrationId";
        internal const string ContractRegistrationEmailAuthPropertiesKey = "contractRegistrationEmail";
        internal const string ContractAccessCodeIdAuthPropertiesKey = "contractAccessCodeId";
        internal const string ContractIdAuthPropertiesKey = "contractId";
        internal const string ContractAccessCodeEmailAuthPropertiesKey = "contractAccessCodeEmail";
        internal const string ExternalSystemPropertyAuthPropertiesKey = "externalSystemProperty";

        internal const string RegixEmailPropertyAuthPropertiesKey = "regixUserEmail";
        internal const string RegixNamePropertyAuthPropertiesKey = "regixUserName";
        internal const string RegixIdPropertyAuthPropertiesKey = "regixUserId";
        internal const string RegixPositionPropertyAuthPropertiesKey = "regixUserPosition";

        public static IAccessContext GetAccessContext(this IOwinContext context, string authenticationType)
        {
            var auth = context.Authentication.AuthenticateAsync(authenticationType).Result;

            return CreateAccessContext(auth);
        }

        internal static IAccessContext CreateAccessContext(AuthenticateResult authResult)
        {
            if (authResult != null &&
                authResult.Properties != null &&
                authResult.Properties.Dictionary != null)
            {
                if (authResult.Properties.Dictionary.ContainsKey(UserIdAuthPropertiesKey) && authResult.Properties.Dictionary.ContainsKey(UsernameAuthPropertiesKey))
                {
                    return new UserAccessContext(int.Parse(authResult.Properties.Dictionary[UserIdAuthPropertiesKey]), authResult.Properties.Dictionary[UsernameAuthPropertiesKey]);
                }

                if (authResult.Properties.Dictionary.ContainsKey(RegistrationIdAuthPropertiesKey) && authResult.Properties.Dictionary.ContainsKey(RegistrationEmailAuthPropertiesKey))
                {
                    return new RegistrationAccessContext(int.Parse(authResult.Properties.Dictionary[RegistrationIdAuthPropertiesKey]), authResult.Properties.Dictionary[RegistrationEmailAuthPropertiesKey]);
                }

                if (authResult.Properties.Dictionary.ContainsKey(ContractRegistrationIdAuthPropertiesKey) && authResult.Properties.Dictionary.ContainsKey(ContractRegistrationEmailAuthPropertiesKey))
                {
                    return new ContractRegistrationAccessContext(int.Parse(authResult.Properties.Dictionary[ContractRegistrationIdAuthPropertiesKey]), authResult.Properties.Dictionary[ContractRegistrationEmailAuthPropertiesKey]);
                }

                if (authResult.Properties.Dictionary.ContainsKey(ContractAccessCodeIdAuthPropertiesKey) &&
                    authResult.Properties.Dictionary.ContainsKey(ContractIdAuthPropertiesKey) &&
                    authResult.Properties.Dictionary.ContainsKey(ContractAccessCodeEmailAuthPropertiesKey))
                {
                    return new ContractAccessCodeAccessContext(
                        int.Parse(authResult.Properties.Dictionary[ContractAccessCodeIdAuthPropertiesKey]),
                        int.Parse(authResult.Properties.Dictionary[ContractIdAuthPropertiesKey]),
                        authResult.Properties.Dictionary[ContractAccessCodeEmailAuthPropertiesKey]);
                }

                if (authResult.Properties.Dictionary.ContainsKey(ExternalSystemPropertyAuthPropertiesKey))
                {
                    return new ExternalSystemAccessContext(authResult.Properties.Dictionary[ExternalSystemPropertyAuthPropertiesKey]);
                }
            }

            return new UnauthenticatedAccessContext();
        }

        public static string CreateOAuthBearerToken(this IOwinContext context, AuthenticationProperties authenticationProperties)
        {
            if (!context.Get<bool>("eumis.OAuthAllowInsecureHttp") &&
                string.Equals(context.Request.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var ticket = new AuthenticationTicket(
                new ClaimsIdentity(AuthenticationTypes.Bearer),
                authenticationProperties);

            DateTimeOffset currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(context.Get<TimeSpan>("eumis.AuthExpirationTimeSpan"));

            return context.Get<TicketDataFormat>("eumis.TicketDataFormat").Protect(ticket);
        }

        public static AuthenticationProperties CreateUserAuthenticationProperties(int userId, string username)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.UserIdAuthPropertiesKey, userId.ToString() },
                { AuthExtensions.UsernameAuthPropertiesKey, username },
            });
        }

        public static AuthenticationProperties CreateRegistrationAuthenticationProperties(int registrationId, string registrationEmail)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.RegistrationIdAuthPropertiesKey, registrationId.ToString() },
                { AuthExtensions.RegistrationEmailAuthPropertiesKey, registrationEmail },
            });
        }

        public static AuthenticationProperties CreateContractRegistrationAuthenticationProperties(int contractRegistrationId, string contractRegistrationEmail)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.ContractRegistrationIdAuthPropertiesKey, contractRegistrationId.ToString() },
                { AuthExtensions.ContractRegistrationEmailAuthPropertiesKey, contractRegistrationEmail },
            });
        }

        public static AuthenticationProperties CreateContractAccessCodeAuthenticationProperties(int contractAccessCodeId, int contractId, string contractAccessCodeEmail)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.ContractAccessCodeIdAuthPropertiesKey, contractAccessCodeId.ToString() },
                { AuthExtensions.ContractIdAuthPropertiesKey, contractId.ToString() },
                { AuthExtensions.ContractAccessCodeEmailAuthPropertiesKey, contractAccessCodeEmail },
            });
        }

        public static AuthenticationProperties CreateExternalSystemAuthenticationProperties(string property)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.ExternalSystemPropertyAuthPropertiesKey, property },
            });
        }

        public static AuthenticationProperties CreateRegixContextAuthenticationProperties(string email, string name, int userId, string position)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.RegixEmailPropertyAuthPropertiesKey, email },
                { AuthExtensions.RegixNamePropertyAuthPropertiesKey, name },
                { AuthExtensions.RegixIdPropertyAuthPropertiesKey, userId.ToString() },
                { AuthExtensions.RegixPositionPropertyAuthPropertiesKey, position },
            });
        }
    }
}
