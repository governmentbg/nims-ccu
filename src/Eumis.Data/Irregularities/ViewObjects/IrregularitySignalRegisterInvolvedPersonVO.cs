using Eumis.Common.Json;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularitySignalRegisterInvolvedPersonVO
    {
        public int PersonId { get; set; }

        public int IrregularitySignalId { get; set; }

        public string IrregularitySignalRegNum { get; set; }

        public string Uin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType UinType { get; set; }

        public InvolvedPersonLegalType LegalType { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string TradeName { get; set; }

        public string HoldingName { get; set; }
    }
}
