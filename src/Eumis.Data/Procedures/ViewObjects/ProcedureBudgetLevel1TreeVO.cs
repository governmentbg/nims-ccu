using System;
using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureBudgetLevel1TreeVO
    {
        public int ProcedureBudgetLevel1Id { get; set; }

        public Guid Gid { get; set; }

        public string DisplayName { get; set; }

        public string NameAlt { get; set; }

        public int OrderNum { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public IList<ProcedureBudgetLevel2TreeVO> Level2Items { get; set; }
    }
}
