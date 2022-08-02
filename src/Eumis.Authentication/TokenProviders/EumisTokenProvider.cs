using Eumis.Authentication.AccessContexts;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.TokenProviders
{
    public class EumisTokenProvider : IEumisTokenProvider
    {
        private IOwinContext owinContext;

        public EumisTokenProvider(IOwinContext owinContext)
        {
            this.owinContext = owinContext;
        }

        public string GenerateToken()
        {
            return this.owinContext.CreateOAuthBearerToken(new AuthenticationProperties());
        }

        public string GenerateToken(string email, string name, int id, string position)
        {
            return this.owinContext.CreateOAuthBearerToken(AuthExtensions.CreateRegixContextAuthenticationProperties(email, name, id, position));
        }
    }
}
