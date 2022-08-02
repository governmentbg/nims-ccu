using Eumis.Common.Config;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eumis.Blob.Host.Auth
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private const string UPLOADER_TOKEN_SUFFIX = ":uploader";

        private static IDictionary<string, string> clients =
            ConfigurationManager.AppSettings.GetWithEnv("Eumis.Blob.Host:Clients")
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Split(','))
                .ToDictionary(c => c[0], c => c[1]);

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            if (context.Scope.Count != 1)
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var scope = context.Scope[0];

            bool isUploader = scope.EndsWith(UPLOADER_TOKEN_SUFFIX);
            if (isUploader)
            {
                scope = scope.Substring(0, scope.Length - UPLOADER_TOKEN_SUFFIX.Length);
            }

            Guid blobKey;
            if (!Guid.TryParse(scope, out blobKey))
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            AuthenticationProperties properties = new AuthenticationProperties(AuthExtensions.CreateAuthenticationProperties(blobKey, isUploader));
            context.Validated(new AuthenticationTicket(oAuthIdentity, properties));
            context.Request.Context.Authentication.SignIn(oAuthIdentity);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (context.TryGetFormCredentials(out clientId, out clientSecret) &&
                clients.ContainsKey(clientId) &&
                clients[clientId] == clientSecret)
            {
                context.Validated();
                return Task.FromResult<object>(null);
            }

            context.Rejected();
            return Task.FromResult<object>(null);
        }
    }
}
