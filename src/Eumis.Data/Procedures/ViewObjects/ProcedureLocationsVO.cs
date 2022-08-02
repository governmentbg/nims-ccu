using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureLocationsVO
    {
        public int ProcedureLocationId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NutsLevel NutsLevel { get; set; }

        public string FullPath { get; set; }
    }
}
