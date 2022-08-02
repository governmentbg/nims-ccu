using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Common.Export
{
    public class ExportContract
    {
        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public string ContractId { get; set; }

        public DateTime SignatureDate { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string[] SubcontractEntityIds { get; set; }
    }
}
