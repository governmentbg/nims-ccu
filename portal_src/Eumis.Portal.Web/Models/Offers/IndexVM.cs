using Eumis.Documents.Contracts;
using PagedList;
using System;

namespace Eumis.Portal.Web.Models.Offers
{
    public class IndexVM
    {
        public string CompanyName { get; set; }

        public string DpName { get; set; }

        public string Name { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? NoticeDate { get; set; }

        public IPagedList<ContractDifferentiatedPositionPVO> SearchItems { get; set; }
    }
}