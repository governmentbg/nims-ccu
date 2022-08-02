using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Monitorstat.IntegrationEumis.Host.Auth;
using Monitorstat.IntegrationEumis.Host.Helpers;
using Monitorstat.IntegrationEumis.Host.Models;

namespace Monitorstat.IntegrationEumis.Host.Communicators
{
    public class EumisRestApiCommunicator : IEumisRestApiCommunicator
    {
        private TicketDataFormat ticketDataFormat;
        private string accessToken;

        public EumisRestApiCommunicator()
        {
            var ticketDataFormat =
                new TicketDataFormat(
                    new AesDataProtector(
                        Configuration.InstallationName,
                        Configuration.AuthKey,
                        Configuration.AuthPreamble));

            this.ticketDataFormat = ticketDataFormat;

            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.ExternalSystemPropertyAuthPropertiesKey, nameof(AuthExtensions.ExternalSystemPropertyAuthPropertiesKey) },
            });

            this.accessToken = this.CreateOAuthBearerToken(properties);
        }

        public Response RegisterMonitorstatDocument(RegisterEumisResultRequest request)
        {
            return MonitorstatAPI.RegisterDocument(request, this.accessToken).ToObject<Response>();
        }

        private string CreateOAuthBearerToken(AuthenticationProperties authenticationProperties)
        {
            var ticket = new AuthenticationTicket(
                new ClaimsIdentity("Bearer"),
                authenticationProperties);

            DateTimeOffset currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(Configuration.AuthExpirationTimeSpan);

            return this.ticketDataFormat.Protect(ticket);
        }
    }
}
