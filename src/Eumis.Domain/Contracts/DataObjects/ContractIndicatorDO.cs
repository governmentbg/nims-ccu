using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Eumis.Domain.Indicators.DataObjects;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractIndicatorDO
    {
        public ContractIndicatorDO()
        {
        }

        public ContractIndicatorDO(ContractIndicator contractIndicator, Indicator indicator)
        {
            this.ContractIndicatorId = contractIndicator.ContractIndicatorId;
            this.ContractId = contractIndicator.ContractId;

            this.Gid = contractIndicator.Gid;
            this.IsActive = contractIndicator.IsActive;

            this.BaseTotalValue = contractIndicator.BaseTotalValue;
            this.BaseMenValue = contractIndicator.BaseMenValue;
            this.BaseWomenValue = contractIndicator.BaseWomenValue;
            this.TargetTotalValue = contractIndicator.TargetTotalValue;
            this.TargetMenValue = contractIndicator.TargetMenValue;
            this.TargetWomenValue = contractIndicator.TargetWomenValue;
            this.Description = contractIndicator.Description;

            this.Indicator = new IndicatorDO(indicator);
        }

        public int ContractIndicatorId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public bool IsActive { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BaseTotalValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BaseMenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BaseWomenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TargetTotalValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TargetMenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TargetWomenValue { get; set; }

        public string Description { get; set; }

        public IndicatorDO Indicator { get; set; }
    }
}