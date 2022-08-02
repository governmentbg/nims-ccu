using Eumis.Common.Json;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureMonitorstatEconomicActivityVO
    {
        public int ProcedureMonitorstatEconomicActivityId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public MonitorstatYear Year { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureMonitorstatEconomicActivityType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureMonitorstatEconomicActivityStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] Version { get; set; }
    }
}
