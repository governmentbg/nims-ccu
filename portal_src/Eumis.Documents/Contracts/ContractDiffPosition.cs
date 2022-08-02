using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Eumis.Documents.Contracts
{
    public class ContractDifferentiatedPositionPVO
    {
        //procurement plan
        public string CompanyName { get; set; }
        public string CompanyUin { get; set; }
        public ContractEnumNomenclature CompanyUinType { get; set; }
        public string Name { get; set; }
        public ContractPublicNomenclature ErrandArea { get; set; }
        public ContractPrivateNomenclature ErrandLegalAct { get; set; }
        public ContractPublicNomenclature ErrandType { get; set; }
        public decimal? ExpectedAmount { get; set; }
        public DateTime? NoticeDate { get; set; }
        public DateTime? OffersDeadlineDate { get; set; }
        public DateTime? AnnouncedDate { get; set; }
        public DateTime? TerminatedDate { get; set; }
        public string Description { get; set; }
        public IList<FilePVO> PublicDocuments { get; set; }
        public IList<FilePVO> AdditionalDocuments { get; set; }

        //differentiated position
        public Guid? DpGid { get; set; }
        public string DpName { get; set; }
    }
}