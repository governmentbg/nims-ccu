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
    public class ProcedureBudgetValidationRuleDO
    {
        public ProcedureBudgetValidationRuleDO()
        {
        }

        public ProcedureBudgetValidationRuleDO(int procedureId, int programmeId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.ProgrammeId = programmeId;
            this.Version = version;
        }

        public ProcedureBudgetValidationRuleDO(
            int procedureBudgetValidationRuleId,
            int procedureId,
            int programmeId,
            string message,
            string condition,
            string rule,
            byte[] version)
        {
            this.ProcedureBudgetValidationRuleId = procedureBudgetValidationRuleId;
            this.ProcedureId = procedureId;
            this.ProgrammeId = programmeId;
            this.Message = message;
            this.Condition = condition;
            this.Rule = rule;
            this.Version = version;
        }

        public int ProcedureBudgetValidationRuleId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public string Message { get; set; }

        public string Condition { get; set; }

        public string Rule { get; set; }

        public byte[] Version { get; set; }
    }
}
