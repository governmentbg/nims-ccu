using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.IntegrationRegiX.Host.Auth
{
    public interface IRegixCallContext
    {
        string Email { get; set; }

        string Name { get; set; }

        string UserId { get; set; }

        string Position { get; set; }
    }
}
