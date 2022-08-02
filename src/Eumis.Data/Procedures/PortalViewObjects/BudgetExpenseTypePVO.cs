using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class BudgetExpenseTypePVO
    {
        public BudgetExpenseTypePVO(BudgetExpenseTypeJson budgetExpenseType)
        {
            this.Gid = budgetExpenseType.Gid;
            this.Name = budgetExpenseType.Name;
            this.NameAlt = budgetExpenseType.NameAlt;
            this.IsActive = budgetExpenseType.IsActive;
            this.Expenses = budgetExpenseType.Expenses.Select(e => new BudgetExpensePVO(e)).ToList();
        }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool IsActive { get; set; }

        public IList<BudgetExpensePVO> Expenses { get; set; }
    }
}
