using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using System;
namespace Eumis.Components.Communicators
{
    public interface IOffersCommunicator
    {
        #region Portal

        ContractPagePVO<ContractDifferentiatedPositionPVO> GetAnnouncedContractDifferentiatedPositions(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            DateTime? offersDeadlineDate,
            DateTime? noticeDate,
            int? limit = null,
            int offset = 0,
            string sortBy = null,
            SortOrder? sortOrder = null);

        ContractPagePVO<ContractDifferentiatedPositionPVO> GetArchivedContractDifferentiatedPositions(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            int? limit = null,
            int offset = 0);

        ContractDifferentiatedPositionPVO GetContractDifferentiatedPosition(Guid gid, string accessToken);

        ContractPagePVO<RegOfferXmlPVO> GetRegistrationSubmittedOffers(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            DateTime? offerSubmitDate,
            int offset = 0,
            int? limit = null,
            string sortBy = null,
            SortOrder? sortOrder = null);

        ContractPagePVO<RegOfferXmlPVO> GetRegistrationDraftOffers(string accessToken, string dpName, string name, string companyName, int offset, int? limit);

        RegOfferXmlPVO GetRegistrationOfferInfo(Guid gid, string accessToken);

        ContractDocumentXml GetRegistrationOffer(Guid gid, string accessToken);

        ContractDocumentXml GetNewRegistrationOffer(Guid gid, string accessToken);

        ContractDocumentXml CreateRegistrationOffer(Guid dpGid, string accessToken);

        ContractDocumentXml UpdateRegistrationOffer(Guid gid, string xml, byte[] version, string accessToken);

        void SubmitRegistrationOffer(Guid gid, byte[] version, string accessToken);

        void WithdrawRegistrationOffer(Guid gid, byte[] version, string accessToken);

        #endregion

        #region Report

        ContractPagePVO<ContractRegOfferXmlPVO> GetRegistrationOffers(string accessToken, Guid contractGid, int offset = 0, int? limit = null);

        ContractRegOfferXmlPVO GetRegistrationOfferInfo(string accessToken, Guid contractGid, Guid offerGid);

        ContractDocumentXml GetRegistrationOffer(string accessToken, Guid contractGid, Guid offerGid);

        #endregion

        #region Private

        ContractDocumentXml PrivateGetRegistrationOffer(string accessToken, Guid offerGid);

        #endregion
    }
}
