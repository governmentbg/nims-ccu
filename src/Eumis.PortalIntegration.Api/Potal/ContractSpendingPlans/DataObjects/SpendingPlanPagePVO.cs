using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Core;

namespace Eumis.PortalIntegration.Api.Potal.ContractSpendingPlans.DataObjects
{
    public class SpendingPlanPagePVO : PagePVO<ContractSpendingPlanPVO>
    {
        public SpendingPlanPagePVO(PagePVO<ContractSpendingPlanPVO> pagePvo, bool canCreate)
        {
            this.Results = pagePvo.Results;
            this.Count = pagePvo.Count;
            this.CanCreate = canCreate;
        }

        public bool CanCreate { get; set; }
    }
}
