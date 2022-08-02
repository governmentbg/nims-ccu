using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Companies.ViewObjects
{
    public class CompaniesVO
    {
        public int CompanyId { get; set; }

        public string Uin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType UinType { get; set; }

        public UinType UinTypeId { get; set; }

        public string Name { get; set; }

        public string Seat { get; set; }

        public string Corr { get; set; }
    }
}
