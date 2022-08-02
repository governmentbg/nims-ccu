using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.FlatFinancialCorrections.ViewObjects
{
    public class FlatFinancialCorrectionVO
    {
        public int FlatFinancialCorrectionId { get; set; }

        public string ContractRegNumber { get; set; }

        public string Name { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FlatFinancialCorrectionLevel Level { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FlatFinancialCorrectionType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FlatFinancialCorrectionStatus Status { get; set; }

        public DateTime? ImpositionDate { get; set; }

        public string ImpositionNumber { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
