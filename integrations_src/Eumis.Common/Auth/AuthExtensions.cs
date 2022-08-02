using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Eumis.Common.Auth
{
    public static class AuthExtensions
    {
        public const string AuthPropertiesOwinEnvironmentKey = "eumis.AuthProperties";

        public const string RegixEmailPropertyAuthPropertiesKey = "regixUserEmail";
        public const string RegixNamePropertyAuthPropertiesKey = "regixUserName";
        public const string RegixIdPropertyAuthPropertiesKey = "regixUserId";
        public const string RegixPositionPropertyAuthPropertiesKey = "regixUserPosition";
        
        public static IDictionary<string, string> GetAuthenticationProperties(this IOwinContext context)
        {
            return context.Get<IDictionary<string, string>>(AuthPropertiesOwinEnvironmentKey);
        }

        public static void SetAuthenticationProperties(this IOwinContext context, IDictionary<string, string> authenticationProperties)
        {
            context.Set<IDictionary<string, string>>(AuthPropertiesOwinEnvironmentKey, authenticationProperties);
        }

        public static string CreateAccessToken(this IOwinContext context)
        {
            var oauthAuthorizationServerOptions = context.Get<OAuthAuthorizationServerOptions>("eumis.OAuthAuthorizationServerOptions");

            if (!oauthAuthorizationServerOptions.AllowInsecureHttp &&
                    string.Equals(context.Request.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var ticket = new AuthenticationTicket(
                new ClaimsIdentity(oauthAuthorizationServerOptions.AuthenticationType),
                new AuthenticationProperties(new Dictionary<string, string>()));

            DateTimeOffset currentUtc = oauthAuthorizationServerOptions.SystemClock.UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(oauthAuthorizationServerOptions.AccessTokenExpireTimeSpan);

            return oauthAuthorizationServerOptions.AccessTokenFormat.Protect(ticket);
        }
    }
}
