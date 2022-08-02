using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Data.UmisVOs
{
    public class PPFundsWithProcedureFundsVO
    {
        public int ProgrammePriorityId { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityNameAlt { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public IList<PPByProcedureChildVO> Procedures { get; set; }
    }
}
