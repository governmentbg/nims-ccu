using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureShareDO
    {
        public ProcedureShareDO()
        {
            this.BudgetAmount = new OperationalMapBudgetDO();
        }

        public ProcedureShareDO(int programmeId, int? programmePriorityId, int procedureId, bool isPrimary, byte[] version)
        {
            this.ProgrammeId = programmeId;
            this.ProgrammePriorityId = programmePriorityId;
            this.ProcedureId = procedureId;
            this.IsPrimary = isPrimary;
            this.Version = version;

            this.BudgetAmount = new OperationalMapBudgetDO();
        }

        public ProcedureShareDO(ProcedureShare procedureShare, byte[] version)
        {
            this.ProcedureShareId = procedureShare.ProcedureShareId;
            this.ProcedureId = procedureShare.ProcedureId;
            this.ProgrammeId = procedureShare.ProgrammeId;
            this.ProgrammePriorityId = procedureShare.ProgrammePriorityId;
            this.IsPrimary = procedureShare.IsPrimary;
            this.IsActivated = procedureShare.IsActivated;
            this.HasLinkedExpenseBudgets = procedureShare.ProcedureBudgetLevel2.Count > 0;
            this.Version = version;

            this.BudgetAmount = new OperationalMapBudgetDO(procedureShare.BgAmount);
        }

        public int? ProcedureShareId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public OperationalMapBudgetDO BudgetAmount { get; set; }

        public bool? IsPrimary { get; set; }

        public bool IsActivated { get; set; }

        public bool? HasLinkedExpenseBudgets { get; set; }

        public byte[] Version { get; set; }
    }
}
