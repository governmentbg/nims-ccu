using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Eumis.Documents.Contracts
{
    public class RegOfferXmlPVO
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

        //differentiated position
        public Guid? DpGid { get; set; }
        public string DpName { get; set; }
        public int? DpSubmittedOffersCount { get; set; }
        public int? DpRankedOffersCount { get; set; }
        public string DpContractContractCompanyName { get; set; }
        public string DpContractContractCompanyUin { get; set; }
        public ContractEnumNomenclature DpContractContractCompanyUinType { get; set; }
        public string DpContractContractContractNumber { get; set; }

        //offer
        public Guid? OfferGid { get; set; }
        public DateTime? OfferSubmitDate { get; set; }
        public bool OfferIsWithdrawn { get; set; }
        public byte[] OfferVersion { get; set; }
    }

    public class ContractRegOfferXmlPVO
    {
        //procurement plan
        public string CompanyName { get; set; }
        public string CompanyUin { get; set; }
        public ContractEnumNomenclature CompanyUinType { get; set; }
        public string TendererName { get; set; }
        public string TendererEmail { get; set; }
        public string TendererUin { get; set; }
        public ContractEnumNomenclature TendererUinType { get; set; }
        public string Name { get; set; }
        public ContractPublicNomenclature ErrandArea { get; set; }
        public ContractPrivateNomenclature ErrandLegalAct { get; set; }
        public ContractPublicNomenclature ErrandType { get; set; }
        public decimal? ExpectedAmount { get; set; }
        public DateTime? NoticeDate { get; set; }
        public DateTime? OffersDeadlineDate { get; set; }
        public DateTime? ProcurementTerminatedDate { get; set; }
        public string Description { get; set; }
        public IList<FilePVO> PublicDocuments { get; set; }

        //offer
        public Guid OfferGid { get; set; }
        public DateTime OfferSubmitDate { get; set; }
        public bool OfferIsWithdrawn { get; set; }

        public Guid DifferentiatedPositionGid { get; set; }
        public Guid ProcurementPlanGid { get; set; }
    }

    public class FilePVO
    {
        public FilePVO()
        {
        }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}