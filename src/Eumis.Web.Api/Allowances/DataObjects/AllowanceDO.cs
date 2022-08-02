using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Allowances;

namespace Eumis.Web.Api.Allowances.DataObjects
{
    public class AllowanceDO
    {
        public AllowanceDO()
        {
            this.Rates = new List<AllowanceRateDO>();
        }

        public AllowanceDO(Allowance allowance)
        {
            this.AllowanceData = new AllowanceDataDO(allowance);
            this.Rates = allowance.Rates
                .OrderByDescending(t => t.Date)
                .Select(r => new AllowanceRateDO(r, allowance.Version));
        }

        public AllowanceDataDO AllowanceData { get; set; }

        public IEnumerable<AllowanceRateDO> Rates { get; set; }
    }
}
