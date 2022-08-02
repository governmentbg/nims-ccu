using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class RegOfferXmlPVO
    {
        // procurement plan
        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public EnumPVO<UinType> CompanyUinType { get; set; }

        public string Name { get; set; }

        public EntityCodeNomVO ErrandArea { get; set; }

        public EntityGidNomVO ErrandLegalAct { get; set; }

        public EntityCodeNomVO ErrandType { get; set; }

        public decimal? ExpectedAmount { get; set; }

        public DateTime? NoticeDate { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public string Description { get; set; }

        public IList<FilePVO> PublicDocuments { get; set; }

        // differentiated position
        public Guid? DpGid { get; set; }

        public string DpName { get; set; }

        public int? DpSubmittedOffersCount { get; set; }

        public int? DpRankedOffersCount { get; set; }

        public string DpContractContractCompanyName { get; set; }

        public string DpContractContractCompanyUin { get; set; }

        public EnumPVO<UinType> DpContractContractCompanyUinType { get; set; }

        public string DpContractContractContractNumber { get; set; }

        // offer
        public Guid? OfferGid { get; set; }

        public DateTime? OfferSubmitDate { get; set; }

        public bool OfferIsWithdrawn { get; set; }

        public byte[] OfferVersion { get; set; }
    }
}