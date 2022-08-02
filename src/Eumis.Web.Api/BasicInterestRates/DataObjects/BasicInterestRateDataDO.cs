using Eumis.Domain.BasicInterestRates;

namespace Eumis.Web.Api.BasicInterestRates.DataObjects
{
    public class BasicInterestRateDataDO
    {
        public BasicInterestRateDataDO()
        {
        }

        public BasicInterestRateDataDO(BasicInterestRate basicInterestRate)
        {
            this.BasicInterestRateId = basicInterestRate.BasicInterestRateId;
            this.Name = basicInterestRate.Name;
            this.Version = basicInterestRate.Version;
        }

        public int? BasicInterestRateId { get; set; }

        public string Name { get; set; }

        public byte[] Version { get; set; }
    }
}
