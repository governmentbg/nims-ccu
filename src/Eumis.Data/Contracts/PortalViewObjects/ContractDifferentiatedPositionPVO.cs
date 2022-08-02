using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractDifferentiatedPositionPVO
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

        public DateTime? AnnouncedDate { get; set; }

        public DateTime? TerminatedDate { get; set; }

        public string Description { get; set; }

        public IList<FilePVO> PublicDocuments { get; set; }

        public IList<FilePVO> AdditionalDocuments { get; set; }

        // differentiated position
        public Guid? DpGid { get; set; }

        public string DpName { get; set; }
    }
}
