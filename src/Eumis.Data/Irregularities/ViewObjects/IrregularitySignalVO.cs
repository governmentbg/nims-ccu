using Eumis.Common.Json;
using Eumis.Domain.Irregularities;
using Newtonsoft.Json;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularitySignalVO
    {
        public int IrregularitySignalId { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNumber { get; set; }

        public string ProjectRegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularitySignalStatus StatusDescr { get; set; }

        public IrregularitySignalStatus Status { get; set; }

        public string IrregularitySignalRegNumber { get; set; }

        public bool IsIrregularityFound { get; set; }

        public string IrregularityRegNumber { get; set; }
    }
}
