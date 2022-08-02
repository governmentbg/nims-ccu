using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Newtonsoft.Json;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects
{
    public class FlatFinancialCorrectionItemDO
    {
        public FlatFinancialCorrectionItemDO()
        {
        }

        public FlatFinancialCorrectionItemDO(FlatFinancialCorrectionLevelItem flatFinancialCorrectionLevelItem, byte[] version, int itemId, string itemName, string itemCode)
        {
            this.Type = flatFinancialCorrectionLevelItem.Type;

            this.FlatFinancialCorrectionLevelItemId = flatFinancialCorrectionLevelItem.FlatFinancialCorrectionLevelItemId;
            this.FlatFinancialCorrectionId = flatFinancialCorrectionLevelItem.FlatFinancialCorrectionId;

            this.Percent = flatFinancialCorrectionLevelItem.Percent;
            this.EuAmount = flatFinancialCorrectionLevelItem.EuAmount;
            this.BgAmount = flatFinancialCorrectionLevelItem.BgAmount;
            this.TotalAmount = flatFinancialCorrectionLevelItem.TotalAmount;

            this.Version = version;

            this.ItemId = itemId;
            this.ItemName = itemName;
            this.ItemCode = itemCode;
        }

        public FlatFinancialCorrectionLevel Type { get; set; }

        public int FlatFinancialCorrectionLevelItemId { get; set; }

        public int FlatFinancialCorrectionId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? Percent { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TotalAmount { get; set; }

        public byte[] Version { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }
    }
}
