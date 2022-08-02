using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureMonitorstatEconomicActivityDO
    {
        public MonitorstatYear Year { get; set; }

        public ProcedureMonitorstatEconomicActivityType Type { get; set; }
    }
}
