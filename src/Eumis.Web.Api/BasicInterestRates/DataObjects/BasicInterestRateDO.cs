using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.BasicInterestRates;

namespace Eumis.Web.Api.BasicInterestRates.DataObjects
{
    public class BasicInterestRateDO
    {
        public BasicInterestRateDO()
        {
            this.Rates = new List<InterestRateDO>();
        }

        public BasicInterestRateDO(BasicInterestRate basicInterestRate)
        {
            this.BasicInterestRateData = new BasicInterestRateDataDO(basicInterestRate);
            this.Rates = basicInterestRate.Rates
                .OrderByDescending(t => t.Date)
                .Select(r => new InterestRateDO(r, basicInterestRate.Version));
        }

        public BasicInterestRateDataDO BasicInterestRateData { get; set; }

        public IEnumerable<InterestRateDO> Rates { get; set; }
    }
}
