using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Companies.ViewObjects
{
    public class LocalActionGroupMunicipalitiesVO
    {
        public int LocalActionGroupMunicipalityId { get; set; }

        public string MunicipalityName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NutsLevel NutsLevel { get; set; }
    }
}
