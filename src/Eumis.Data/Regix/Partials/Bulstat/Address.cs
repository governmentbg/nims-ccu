using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Address
    {
        public override string ToString()
        {
            string address = string.Empty;

            if (!string.IsNullOrEmpty(this.Street))
            {
                address += $"{this.Street}, ";
            }

            if (!string.IsNullOrEmpty(this.StreetNumber))
            {
                address += $"{DataTexts.Regix_Address_StreetNumber} {this.StreetNumber}, ";
            }

            if (!string.IsNullOrEmpty(this.Building))
            {
                address += $"{DataTexts.Regix_Address_Building} {this.Building}, ";
            }

            if (!string.IsNullOrEmpty(this.Entrance))
            {
                address += $"{DataTexts.Regix_Address_Entrance} {this.Entrance}, ";
            }

            if (!string.IsNullOrEmpty(this.Floor))
            {
                address += $"{DataTexts.Regix_Address_Floor} {this.Floor}, ";
            }

            if (!string.IsNullOrEmpty(this.Apartment))
            {
                address += $"{DataTexts.Regix_Address_Apartment} {this.Apartment}, ";
            }

            if (!string.IsNullOrEmpty(address))
            {
                address = address.TrimEnd(' ').TrimEnd(',');
            }

            return address;
        }
    }
}
