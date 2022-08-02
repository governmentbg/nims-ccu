using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractProcurementPlan
    {
        public bool IsErrandLegalActPms
        {
            get
            {
                return this.ErrandLegalActId == ErrandLegalAct.PmsErrandLegalActId;
            }
        }

        public void AssertNotExpired()
        {
            if ((DateTime.Now - this.OffersDeadlineDate.Value).TotalDays >= 1)
            {
                throw new InvalidOperationException("Deadline required by the contract procurement expired.");
            }
        }
    }
}
