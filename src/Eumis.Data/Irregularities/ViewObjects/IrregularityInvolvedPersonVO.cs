using Eumis.Common.Json;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityInvolvedPersonVO
    {
        public int PersonId { get; set; }

        public string Uin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType UinType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public InvolvedPersonLegalType LegalType { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string TradeName { get; set; }

        public string HoldingName { get; set; }
    }
}
