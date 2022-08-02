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
    public class ProcedureSpecificTargetsDO
    {
        public ProcedureSpecificTargetsDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Version = version;
        }

        public int ProcedureId { get; set; }

        public int? SpecificTargetId { get; set; }

        public byte[] Version { get; set; }
    }
}
