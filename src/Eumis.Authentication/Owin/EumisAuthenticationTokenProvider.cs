using Eumis.Common.Auth;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System.Security.Claims;

namespace Eumis.Authentication.Owin
{
    internal class EumisAuthenticationTokenProvider : AuthenticationTokenProvider
    {
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);

            if (context.Ticket != null &&
                context.Ticket.Identity != null &&
                context.Ticket.Identity.AuthenticationType == AuthenticationTypes.Cookie)
            {
                // if a cookie identity is being used
                // repackage the cookie ticket in a bearer auth ticket
                var bearerTicket = new AuthenticationTicket(
                    new ClaimsIdentity(
                        context.Ticket.Identity.Claims,
                        AuthenticationTypes.Bearer,
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType),
                    context.Ticket.Properties);

                context.SetTicket(bearerTicket);
            }
        }
    }
}
