using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Eumis.Blob.Host.Auth
{
    public static class AuthExtensions
    {
        public const string AuthPropertiesOwinEnvironmentKey = "eumis.AuthProperties";
        public const string BlobKeyAuthPropertiesKey = "blobKey";
        public const string BlobKeyAuthPropertiesIsUploader = "isUploader";

        public static IBlobAccessContext GetBlobAccessContext(this HttpRequestMessage request)
        {
            var authProps = request.GetOwinContext().GetAuthenticationProperties();

            if (authProps != null &&
                authProps.ContainsKey(AuthExtensions.BlobKeyAuthPropertiesKey) &&
                authProps.ContainsKey(AuthExtensions.BlobKeyAuthPropertiesIsUploader))
            {
                return new AuthenticatedBlobAccessContext(
                    Guid.Parse(authProps[AuthExtensions.BlobKeyAuthPropertiesKey]),
                    bool.Parse(authProps[AuthExtensions.BlobKeyAuthPropertiesIsUploader]));
            }
            else
            {
                return new UnauthenticatedBlobAccessContext();
            }
        }

        public static IDictionary<string, string> GetAuthenticationProperties(this IOwinContext context)
        {
            return context.Get<IDictionary<string, string>>(AuthPropertiesOwinEnvironmentKey);
        }

        public static void SetAuthenticationProperties(this IOwinContext context, IDictionary<string, string> authenticationProperties)
        {
            context.Set<IDictionary<string, string>>(AuthPropertiesOwinEnvironmentKey, authenticationProperties);
        }

        public static IDictionary<string, string> CreateAuthenticationProperties(Guid blobKey, bool isUploader)
        {
            return new Dictionary<string, string>
            {
                { AuthExtensions.BlobKeyAuthPropertiesKey, blobKey.ToString() },
                { AuthExtensions.BlobKeyAuthPropertiesIsUploader, isUploader.ToString() },
            };
        }

        public static string CreateUploaderAccessToken(this IOwinContext context, Guid blobKey)
        {
            var oauthAuthorizationServerOptions = context.Get<OAuthAuthorizationServerOptions>("eumis.OAuthAuthorizationServerOptions");

            if (!oauthAuthorizationServerOptions.AllowInsecureHttp &&
                    string.Equals(context.Request.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var ticket = new AuthenticationTicket(
                new ClaimsIdentity(oauthAuthorizationServerOptions.AuthenticationType),
                new AuthenticationProperties(AuthExtensions.CreateAuthenticationProperties(blobKey, true)));

            DateTimeOffset currentUtc = oauthAuthorizationServerOptions.SystemClock.UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(oauthAuthorizationServerOptions.AccessTokenExpireTimeSpan);

            return oauthAuthorizationServerOptions.AccessTokenFormat.Protect(ticket);
        }
    }
}
