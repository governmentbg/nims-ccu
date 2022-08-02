using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureBudgetTreeVO
    {
        public int ProcedureId { get; set; }

        public byte[] Version { get; set; }

        public IList<ProcedureBudgetLevel0TreeVO> Programmes { get; set; }
    }
}
