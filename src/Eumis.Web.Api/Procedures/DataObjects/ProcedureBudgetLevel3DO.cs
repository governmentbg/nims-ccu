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
    public class ProcedureBudgetLevel3DO
    {
        public ProcedureBudgetLevel3DO()
        {
        }

        public ProcedureBudgetLevel3DO(int procedureId, int procedureBudgetLevel2Id, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.ProcedureBudgetLevel2Id = procedureBudgetLevel2Id;
            this.Version = version;
        }

        public ProcedureBudgetLevel3DO(int procedureId, int procedureBudgetLevel2Id, string note, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.ProcedureBudgetLevel2Id = procedureBudgetLevel2Id;
            this.Note = note;
            this.Version = version;
        }

        public int ProcedureId { get; set; }

        public int ProcedureBudgetLevel2Id { get; set; }

        public string Note { get; set; }

        public byte[] Version { get; set; }
    }
}
