using Eumis.Common.Config;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Eumis.Common.Auth
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private static IDictionary<string, string> clients =
            ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:Clients")
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Split(','))
                .ToDictionary(c => c[0], c => c[1]);

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            if (context.Scope.Count != 1)
            {
                context.Rejected();
                return Task.CompletedTask;
            }

            var scope = context.Scope[0];

            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>());
            context.Validated(new AuthenticationTicket(oAuthIdentity, properties));
            context.Request.Context.Authentication.SignIn(oAuthIdentity);

            return Task.CompletedTask;
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
                return Task.CompletedTask;
            }

            context.Rejected();
            return Task.CompletedTask;
        }
    }
}
