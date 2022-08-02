using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Eumis.Common.Owin
{
    public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        private const string AccessTokenQueryKey = "access_token";
        public const string AccessTokenKey = "Eumis.AccessToken.Key";

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            if (context.IsValidated &&
                context.Ticket.Identity.Claims.Any(c => c.Issuer != "LOCAL AUTHORITY"))
            {
                context.Rejected();
            }

            return Task.FromResult<object>(null);
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            // check if token found in the default location - "Authorization: Bearer <token>" header
            if (string.IsNullOrEmpty(context.Token))
            {
                var token = context.Request.Query.Get(AccessTokenQueryKey);
                context.OwinContext.Set<string>(AccessTokenKey, token);

                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
