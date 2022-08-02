using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.TokenProviders
{
    public interface IEumisTokenProvider
    {
        string GenerateToken();

        string GenerateToken(string email, string name, int id, string position);
    }
}
