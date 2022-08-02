using Eumis.Documents.Contracts;
using PagedList;

namespace Eumis.Portal.Web.Models.Offers
{
    public class DraftsVM
    {
        public string CompanyName { get; set; }

        public string DpName { get; set; }

        public string Name { get; set; }

        public IPagedList<RegOfferXmlPVO> SearchItems { get; set; }
    }
}