using Eumis.Documents.Contracts;
using System;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class OffersCommunicator : IOffersCommunicator
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
            return OffersApi.GetAnnouncedContractDifferentiatedPositions(accessToken, dpName, name, companyName, offersDeadlineDate, noticeDate, limit, offset, sortBy, sortOrder).ToObject<ContractPagePVO<ContractDifferentiatedPositionPVO>>();
        }

        public ContractPagePVO<ContractDifferentiatedPositionPVO> GetArchivedContractDifferentiatedPositions(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            int? limit = null,
            int offset = 0)
        {
            return OffersApi.GetArchivedContractDifferentiatedPositions(accessToken, dpName, name, companyName, limit, offset).ToObject<ContractPagePVO<ContractDifferentiatedPositionPVO>>();
        }

        public ContractDifferentiatedPositionPVO GetContractDifferentiatedPosition(Guid gid, string accessToken)
        {
            return OffersApi.GetContractDifferentiatedPosition(gid, accessToken).ToObject<ContractDifferentiatedPositionPVO>();
        }

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
            return OffersApi.GetRegistrationSubmittedOffers(accessToken, dpName, name, companyName, offerSubmitDate, offset, limit, sortBy, sortOrder).ToObject<ContractPagePVO<RegOfferXmlPVO>>();
        }

        public ContractPagePVO<RegOfferXmlPVO> GetRegistrationDraftOffers(string accessToken, string dpName, string name, string companyName, int offset, int? limit)
        {
            return OffersApi.GetRegistrationDraftOffers(accessToken, dpName, name, companyName, offset, limit).ToObject<ContractPagePVO<RegOfferXmlPVO>>();
        }

        public RegOfferXmlPVO GetRegistrationOfferInfo(Guid gid, string accessToken)
        {
            return OffersApi.GetRegistrationOfferInfo(gid, accessToken).ToObject<RegOfferXmlPVO>();
        }

        public ContractDocumentXml GetRegistrationOffer(Guid gid, string accessToken)
        {
            return OffersApi.GetRegistrationOffer(gid, accessToken).ToObject<ContractDocumentXml>();
        }


        public ContractDocumentXml GetNewRegistrationOffer(Guid gid, string accessToken)
        {
            return OffersApi.GetNewRegistrationOffer(gid, accessToken).ToObject<ContractDocumentXml>();
        }

        public ContractDocumentXml CreateRegistrationOffer2(Guid gid, string xml, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml
                });

            return OffersApi.CreateRegistrationOffer2(gid, body, accessToken).ToObject<ContractDocumentXml>();
        }

        public ContractDocumentXml CreateRegistrationOffer(Guid dpGid, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            return OffersApi.CreateRegistrationOffer(dpGid, body, accessToken).ToObject<ContractDocumentXml>();
        }

        public ContractDocumentXml UpdateRegistrationOffer(Guid gid, string xml, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return OffersApi.UpdateRegistrationOffer(gid, body, accessToken).ToObject<ContractDocumentXml>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void SubmitRegistrationOffer(Guid gid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                OffersApi.SubmitRegistrationOffer(gid, body, accessToken);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        public void WithdrawRegistrationOffer(Guid gid, byte[] version, string accessToken)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            try
            {
                OffersApi.WithdrawRegistrationOffer(gid, body, accessToken);
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
            }
        }

        #endregion

        #region Report

        public ContractPagePVO<ContractRegOfferXmlPVO> GetRegistrationOffers(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            return OffersApi.GetRegistrationOffers(accessToken, contractGid, offset, limit).ToObject<ContractPagePVO<ContractRegOfferXmlPVO>>();
        }

        public ContractRegOfferXmlPVO GetRegistrationOfferInfo(string accessToken, Guid contractGid, Guid offerGid)
        {
            return OffersApi.GetRegistrationOfferInfo(contractGid, offerGid, accessToken).ToObject<ContractRegOfferXmlPVO>();
        }

        public ContractDocumentXml GetRegistrationOffer(string accessToken, Guid contractGid, Guid offerGid)
        {
            return OffersApi.GetRegistrationOffer(contractGid, offerGid, accessToken).ToObject<ContractDocumentXml>();
        }

        #endregion

        #region Private

        public ContractDocumentXml PrivateGetRegistrationOffer(string accessToken, Guid offerGid)
        {
            return OffersApi.PrivateGetRegistrationOffer(offerGid, accessToken).ToObject<ContractDocumentXml>();
        }

        #endregion
    }
}
