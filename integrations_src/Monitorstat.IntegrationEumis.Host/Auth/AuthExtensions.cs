using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Auth
{
    public static class AuthExtensions
    {
        internal const string ExternalSystemPropertyAuthPropertiesKey = "externalSystemProperty";

        public static AuthenticationProperties CreateExternalSystemAuthenticationProperties(string property)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthExtensions.ExternalSystemPropertyAuthPropertiesKey, property },
            });
        }
    }
}
