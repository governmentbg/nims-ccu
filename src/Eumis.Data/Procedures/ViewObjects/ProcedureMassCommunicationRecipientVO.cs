using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureMassCommunicationRecipientVO
    {
        public int ContractId { get; set; }

        public string ContractRegNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public string ContractName { get; set; }

        public string BeneficiaryName { get; set; }
    }
}
