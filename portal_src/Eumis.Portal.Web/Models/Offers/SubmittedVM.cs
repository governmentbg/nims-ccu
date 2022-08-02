using Eumis.Documents.Contracts;
using PagedList;
using System;

namespace Eumis.Portal.Web.Models.Offers
{
    public class SubmittedVM
    {
        public string CompanyName { get; set; }

        public string DpName { get; set; }

        public string Name { get; set; }

        public DateTime? OfferSubmitDate { get; set; }

        public IPagedList<RegOfferXmlPVO> SearchItems { get; set; }
    }
}