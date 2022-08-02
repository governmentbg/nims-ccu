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
    public class ProcedureBudgetLevel1DO
    {
        public ProcedureBudgetLevel1DO()
        {
        }

        public ProcedureBudgetLevel1DO(int procedureId, int programmeId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.ProgrammeId = programmeId;
            this.Version = version;
        }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public int? ExpenseTypeId { get; set; }

        public byte[] Version { get; set; }
    }
}
