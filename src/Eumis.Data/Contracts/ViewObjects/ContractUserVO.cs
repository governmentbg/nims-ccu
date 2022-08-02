using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractUserVO
    {
        public int ContractUserId { get; set; }

        public int ContractId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }
    }
}
