using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Core;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ProcurementPagePVO : PagePVO<ContractProcurementPVO>
    {
        public ProcurementPagePVO(PagePVO<ContractProcurementPVO> pagePvo, bool canCreate)
        {
            this.Results = pagePvo.Results;
            this.Count = pagePvo.Count;
            this.CanCreate = canCreate;
        }

        public bool CanCreate { get; set; }
    }
}
