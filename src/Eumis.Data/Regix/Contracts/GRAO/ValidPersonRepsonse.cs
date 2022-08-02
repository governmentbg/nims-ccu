using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Regix.Contracts.Grao
{
    public partial class ValidPersonResponse
    {
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string FamilyName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }
    }
}
