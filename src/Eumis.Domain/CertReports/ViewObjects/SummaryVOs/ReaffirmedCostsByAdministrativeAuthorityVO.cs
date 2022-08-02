using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class ReaffirmedCostsByAdministrativeAuthorityVO
    {
        public ReaffirmedCostsByAdministrativeAuthorityVO()
        {
            this.RevalidatedTotalAmount = 0;
        }

        public string ProgrammePriorityName { get; set; }

        public string RevalidationNote { get; set; }

        public decimal? RevalidatedTotalAmount { get; set; }
    }
}
