using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureBudgetLevel2TreeVO
    {
        public int ProcedureBudgetLevel2Id { get; set; }

        public Guid Gid { get; set; }

        public string DisplayName { get; set; }

        public string NameAlt { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public ProcedureBudgetLevel2AidMode AidMode { get; set; }

        public bool IsEligibleCost { get; set; }

        public bool IsStandardTablesExpense { get; set; }

        public bool IsOneTimeExpense { get; set; }

        public bool IsFlatRateExpense { get; set; }

        public bool IsLandExpense { get; set; }

        public bool IsEuApprovedStandardTablesExpense { get; set; }

        public bool IsEuApprovedOneTimeExpense { get; set; }

        public int OrderNum { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public IList<ProcedureBudgetLevel3TreeVO> Level3Items { get; set; }
    }
}
