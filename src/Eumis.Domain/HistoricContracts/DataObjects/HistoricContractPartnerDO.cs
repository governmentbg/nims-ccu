using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractPartnerDO
    {
        public HistoricContractPartnerType? PartnerType { get; set; }

        public string PartnerName { get; set; }

        public string PartnerNameEn { get; set; }

        public string PartnerUin { get; set; }

        public UinType? PartnerUinType { get; set; }

        public string PartnerTypeCode { get; set; }

        public int PartnerTypeId { get; set; }

        public int PartnerLegalTypeId { get; set; }

        public string SeatCountryCode { get; set; }

        public string SeatSettlementCode { get; set; }

        public string SeatPostCode { get; set; }

        public string SeatStreet { get; set; }

        public string SeatAddress { get; set; }
    }
}
