using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class GetStateOfPlayRequestCase
    {
        public NomenclatureEntry Court { get; set; }

        public int Year { get; set; }

        public string Number { get; set; }
    }
}
