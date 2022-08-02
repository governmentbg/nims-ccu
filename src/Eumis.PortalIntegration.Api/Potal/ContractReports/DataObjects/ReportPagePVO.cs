using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Core;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ReportPagePVO : PagePVO<ContractReportPVO>
    {
        public ReportPagePVO(PagePVO<ContractReportPVO> pagePvo, bool canCreate, bool canEditSent)
        {
            this.Results = pagePvo.Results;
            this.Count = pagePvo.Count;
            this.CanCreate = canCreate;
            this.CanEditSent = canEditSent;
        }

        public bool CanCreate { get; set; }

        public bool CanEditSent { get; set; }
    }
}
