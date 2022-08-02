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
    public class ProcedureBudgetLevel2DO
    {
        public ProcedureBudgetLevel2DO()
        {
        }

        public ProcedureBudgetLevel2DO(
            int procedureBudgetLevel1Id,
            int procedureId,
            int programmeId,
            int? programmePriorityId,
            int expenseTypeId,
            byte[] version)
        {
            this.ProcedureBudgetLevel1Id = procedureBudgetLevel1Id;
            this.ProcedureId = procedureId;
            this.ProgrammeId = programmeId;
            this.ProgrammePriorityId = programmePriorityId;
            this.ExpenseTypeId = expenseTypeId;
            this.Version = version;
        }

        public ProcedureBudgetLevel2DO(
            int procedureBudgetLevel2Id,
            int procedureBudgetLevel1Id,
            int procedureId,
            int programmeId,
            int? programmePriorityId,
            int expenseTypeId,
            string name,
            string nameAlt,
            ProcedureBudgetLevel2AidMode aidMode,
            byte[] version)
        {
            this.ProcedureBudgetLevel2Id = procedureBudgetLevel2Id;
            this.ProcedureBudgetLevel1Id = procedureBudgetLevel1Id;
            this.ProcedureId = procedureId;
            this.ProgrammeId = programmeId;
            this.ProgrammePriorityId = programmePriorityId;
            this.ExpenseTypeId = expenseTypeId;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.AidMode = aidMode;

            this.Version = version;
        }

        public int? ProcedureBudgetLevel2Id { get; set; }

        public int ProcedureBudgetLevel1Id { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int ExpenseTypeId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public ProcedureBudgetLevel2AidMode? AidMode { get; set; }

        public byte[] Version { get; set; }
    }
}
