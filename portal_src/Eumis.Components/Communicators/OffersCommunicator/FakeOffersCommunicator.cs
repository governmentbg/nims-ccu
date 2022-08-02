using System;
using Eumis.Documents.Contracts;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class FakeOffersCommunicator : IOffersCommunicator
    {
        #region Portal

        public ContractPagePVO<ContractDifferentiatedPositionPVO> GetAnnouncedContractDifferentiatedPositions(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            DateTime? offersDeadlineDate,
            DateTime? noticeDate,
            int? limit = null,
            int offset = 0,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            throw new NotImplementedException();
        }

        public ContractPagePVO<ContractDifferentiatedPositionPVO> GetArchivedContractDifferentiatedPositions(string accessToken, string dpName, string name, string companyName, int? limit = null, int offset = 0) { throw new NotImplementedException(); }

        public ContractDifferentiatedPositionPVO GetContractDifferentiatedPosition(Guid gid, string accessToken) { throw new NotImplementedException(); }

        public ContractPagePVO<RegOfferXmlPVO> GetRegistrationSubmittedOffers(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            DateTime? offerSubmitDate,
            int offset = 0,
            int? limit = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            throw new NotImplementedException();
        }

        public ContractPagePVO<RegOfferXmlPVO> GetRegistrationDraftOffers(string accessToken, string dpName, string name, string companyName, int offset, int? limit) { throw new NotImplementedException(); }

        public RegOfferXmlPVO GetRegistrationOfferInfo(Guid gid, string accessToken) { throw new NotImplementedException(); }

        public ContractDocumentXml GetRegistrationOffer(Guid gid, string accessToken) { throw new NotImplementedException(); }

        public ContractDocumentXml GetNewRegistrationOffer(Guid gid, string accessToken) { throw new NotImplementedException(); }

        public ContractDocumentXml CreateRegistrationOffer(Guid dpGid, string accessToken) { throw new NotImplementedException(); }

        public ContractDocumentXml UpdateRegistrationOffer(Guid gid, string xml, byte[] version, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void SubmitRegistrationOffer(Guid gid, byte[] version, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void WithdrawRegistrationOffer(Guid gid, byte[] version, string accessToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Report

        public ContractPagePVO<ContractRegOfferXmlPVO> GetRegistrationOffers(string accessToken, Guid contractGid, int offset = 0, int? limit = null) { throw new NotImplementedException(); }

        public ContractRegOfferXmlPVO GetRegistrationOfferInfo(string accessToken, Guid contractGid, Guid offerGid) { throw new NotImplementedException(); }

        public ContractDocumentXml GetRegistrationOffer(string accessToken, Guid contractGid, Guid offerGid) { throw new NotImplementedException(); }

        #endregion

        #region Private

        public ContractDocumentXml PrivateGetRegistrationOffer(string accessToken, Guid offerGid) { throw new NotImplementedException(); }

        #endregion
    }
}
