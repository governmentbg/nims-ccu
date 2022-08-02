using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Owin
{
    internal enum EumisOAuthErrors
    {
        Unauthorized,
        UnauthorizedClient,
        InvalidClientId,
        UnknownScopeFormat,
        ClientScopeMismatch,
        RegistrationNotActivated,
        DeletedOrLocked,
        AccessCodeNotActive,
    }
}
