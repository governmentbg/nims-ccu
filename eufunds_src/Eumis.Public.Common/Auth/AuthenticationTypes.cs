using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace Eumis.Public.Common.Auth
{
    public static class AuthenticationTypes
    {
        public static readonly string Bearer = OAuthDefaults.AuthenticationType;
        public static readonly string Cookie = CookieAuthenticationDefaults.AuthenticationType;
    }
}
