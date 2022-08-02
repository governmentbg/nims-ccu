using Eumis.Common.Json;
using Eumis.Domain.Irregularities;
using Newtonsoft.Json;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularitySignalInfoVO
    {
        public IrregularitySignalStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularitySignalStatus StatusDescr { get; set; }

        public string ContractNum { get; set; }

        public string ProjectNum { get; set; }
    }
}
