using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Regix.Helpers.Bulstat
{
    public static class BulstatHelper
    {
        public static string ToBulstatCode(this AddressType address)
        {
            int code = (int)address;

            return code.ToString();
        }

        public static string ToBulstatCode(this CommunicationType communication)
        {
            int code = (int)communication;

            return code.ToString();
        }
    }
}
