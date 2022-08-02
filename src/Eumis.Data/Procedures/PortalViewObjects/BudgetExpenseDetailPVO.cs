using System;
using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class BudgetExpenseDetailPVO
    {
        public BudgetExpenseDetailPVO(BudgetExpenseDetailJson budgetExpenseDetail)
        {
            this.Gid = budgetExpenseDetail.Gid;
            this.Note = budgetExpenseDetail.Note;
        }

        public Guid Gid { get; set; }

        public string Note { get; set; }
    }
}
