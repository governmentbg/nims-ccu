using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class BudgetExpensePVO
    {
        public BudgetExpensePVO(BudgetExpenseJson budgetExpense)
        {
            this.Gid = budgetExpense.Gid;
            this.Name = budgetExpense.Name;
            this.NameAlt = budgetExpense.NameAlt;
            this.IsActive = budgetExpense.IsActive;
            this.ProgrammePriorityCode = budgetExpense.ProgrammePriorityCode;

            this.AidMode = new EnumPVO<ProcedureBudgetLevel2AidMode>
            {
                Description = budgetExpense.AidMode,
                Value = budgetExpense.AidMode,
            };
            this.IsEligibleCost = budgetExpense.IsEligibleCost;
            this.IsStandardTablesExpense = budgetExpense.IsStandardTablesExpense;
            this.IsOneTimeExpense = budgetExpense.IsOneTimeExpense;
            this.IsFlatRateExpense = budgetExpense.IsFlatRateExpense;
            this.IsLandExpense = budgetExpense.IsLandExpense;
            this.IsEuApprovedStandardTablesExpense = budgetExpense.IsEuApprovedStandardTablesExpense;
            this.IsEuApprovedOneTimeExpense = budgetExpense.IsEuApprovedOneTimeExpense;

            this.Details = budgetExpense.Details.Select(d => new BudgetExpenseDetailPVO(d)).ToList();
        }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public bool IsEligibleCost { get; set; }

        public bool IsStandardTablesExpense { get; set; }

        public bool IsOneTimeExpense { get; set; }

        public bool IsFlatRateExpense { get; set; }

        public bool IsLandExpense { get; set; }

        public bool IsEuApprovedStandardTablesExpense { get; set; }

        public bool IsEuApprovedOneTimeExpense { get; set; }

        public bool IsActive { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public EnumPVO<ProcedureBudgetLevel2AidMode> AidMode { get; set; }

        public IList<BudgetExpenseDetailPVO> Details { get; set; }
    }
}
