using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureBudgetLevel0TreeVO
    {
        public int ProgrammeId { get; set; }

        public string DisplayName { get; set; }

        public string NameAlt { get; set; }

        public string Code { get; set; }

        public IList<ProcedureBudgetLevel1TreeVO> Level1Items { get; set; }

        public IList<ProcedureBudgetValidationRuleVO> ValidationRules { get; set; }
    }
}
