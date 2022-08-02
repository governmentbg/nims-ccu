using Eumis.Documents.Contracts;
using PagedList;
namespace Eumis.Portal.Web.Areas.Report.Models.Communication
{
    public class IndexVM
    {
        public R_09987.CommunicationTypeNomenclature type { get; set; }

        public StaticPagedList<ContractCommunicationInfo> Communications { get; set; }
    }
}