using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Users.DataObjects
{
    public class PasswordsDO
    {
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public string Version { get; set; }
    }
}
