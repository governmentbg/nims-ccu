using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public class ContractRegOfferXmlPVO
    {
        // procurement plan
        public Guid ProcurementPlanGid { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public EnumPVO<UinType> CompanyUinType { get; set; }

        public string TendererName { get; set; }

        public string TendererUin { get; set; }

        public EnumPVO<UinType> TendererUinType { get; set; }

        public string TendererEmail { get; set; }

        public string Name { get; set; }

        public EntityCodeNomVO ErrandArea { get; set; }

        public EntityGidNomVO ErrandLegalAct { get; set; }

        public EntityCodeNomVO ErrandType { get; set; }

        public decimal? ExpectedAmount { get; set; }

        public DateTime? NoticeDate { get; set; }

        public DateTime? OffersDeadlineDate { get; set; }

        public DateTime? ProcurementTerminatedDate { get; set; }

        public string Description { get; set; }

        public IList<FilePVO> PublicDocuments { get; set; }

        // offer
        public Guid OfferGid { get; set; }

        public DateTime OfferSubmitDate { get; set; }

        public bool OfferIsWithdrawn { get; set; }

        // differantiated position
        public Guid DifferentiatedPositionGid { get; set; }
    }
}