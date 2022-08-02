using System;
using System.Collections.Generic;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class BudgetExpenseTypeJson
    {
        public BudgetExpenseTypeJson()
        {
            this.Expenses = new List<BudgetExpenseJson>();
        }

        public int BudgetLevel1Id { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public IList<BudgetExpenseJson> Expenses { get; set; }
    }
}
