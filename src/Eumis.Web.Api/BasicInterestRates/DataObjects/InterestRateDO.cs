using System;
using Eumis.Domain.BasicInterestRates;

namespace Eumis.Web.Api.BasicInterestRates.DataObjects
{
    public class InterestRateDO
    {
        public InterestRateDO()
        {
        }

        public InterestRateDO(int basicInterestRateId, byte[] version)
        {
            this.BasicInterestRateId = basicInterestRateId;
            this.Version = version;
        }

        public InterestRateDO(InterestRate interestRate, byte[] version)
        {
            this.InterestRateId = interestRate.InterestRateId;
            this.BasicInterestRateId = interestRate.BasicInterestRateId;
            this.Date = interestRate.Date;
            this.Rate = interestRate.Rate;

            this.Version = version;
        }

        public int? InterestRateId { get; set; }

        public int? BasicInterestRateId { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Rate { get; set; }

        public byte[] Version { get; set; }
    }
}
