using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects
{
    public class FlatFinancialCorrectionInfoDO
    {
        public FlatFinancialCorrectionInfoDO()
        {
        }

        public FlatFinancialCorrectionInfoDO(FlatFinancialCorrection flatFinancialCorrection)
        {
            this.FlatFinancialCorrectionId = flatFinancialCorrection.FlatFinancialCorrectionId;
            this.Name = flatFinancialCorrection.Name;
            this.OrderNum = flatFinancialCorrection.OrderNum;
            this.Level = flatFinancialCorrection.Level;
            this.LevelDescription = flatFinancialCorrection.Level;
            this.Type = flatFinancialCorrection.Type;
            this.Status = flatFinancialCorrection.Status;
            this.ContractId = flatFinancialCorrection.ContractId;
            this.StatusDescription = flatFinancialCorrection.Status;
            this.ImpositionDate = flatFinancialCorrection.ImpositionDate;
            this.ImpositionNumber = flatFinancialCorrection.ImpositionNumber;
            this.Version = flatFinancialCorrection.Version;
        }

        public int FlatFinancialCorrectionId { get; set; }

        public string Name { get; set; }

        public int? OrderNum { get; set; }

        public FlatFinancialCorrectionLevel? Level { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FlatFinancialCorrectionLevel? LevelDescription { get; set; }

        public FlatFinancialCorrectionType? Type { get; set; }

        public FlatFinancialCorrectionStatus? Status { get; set; }

        public int? ContractId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FlatFinancialCorrectionStatus? StatusDescription { get; set; }

        public DateTime? ImpositionDate { get; set; }

        public string ImpositionNumber { get; set; }

        public byte[] Version { get; set; }
    }
}
