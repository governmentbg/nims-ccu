using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractInfoVO
    {
        public int ContractId { get; set; }

        public string Name { get; set; }

        public string RegNumber { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public ProcedureKind ProcedureKind { get; set; }

        public bool IsIndicatorSectionVisible { get; set; }
    }
}
