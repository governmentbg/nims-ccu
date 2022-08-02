using Eumis.Documents.Contracts;
using PagedList;
namespace Eumis.Portal.Web.Areas.Report.Models.BFPContract
{
    public class IndexVM
    {
        public StaticPagedList<ContractVersionPVO> ContractVersions { get; set; }
        public StaticPagedList<ContractProcurementPVO> ProcurementVersions { get; set; }
        public StaticPagedList<ContractSpendingPlanPVO> SpendingPlanVersions { get; set; }
        public bool CanCreateProcurement { get; set; }
        public bool CanCreateSpendingPlan { get; set; }
    }
}