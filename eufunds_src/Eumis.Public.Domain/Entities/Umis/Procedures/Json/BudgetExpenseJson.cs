using System;
using System.Collections.Generic;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class BudgetExpenseJson
    {
        public BudgetExpenseJson()
        {
            this.Details = new List<BudgetExpenseDetailJson>();
        }

        public int BudgetLevel2Id { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public bool IsEligibleCost { get; set; }

        public bool IsStandardTablesExpense { get; set; }

        public bool IsOneTimeExpense { get; set; }

        public bool IsFlatRateExpense { get; set; }

        public bool IsLandExpense { get; set; }

        public bool IsEuApprovedStandardTablesExpense { get; set; }

        public bool IsEuApprovedOneTimeExpense { get; set; }

        public bool IsActive { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public FinanceSource FinanceSource { get; set; }

        public ProcedureBudgetLevel2AidMode AidMode { get; set; }

        public IList<BudgetExpenseDetailJson> Details { get; set; }
    }
}
