using Newtonsoft.Json.Linq;
using System;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class OffersApi
    {
        #region Portal

        public static JObject GetAnnouncedContractDifferentiatedPositions(
            string accessToken,
            string dpName,
            string name,
            string companyName,
            DateTime? offersDeadlineDate,
            DateTime? noticeDate,
            int? limit,
            int offset,
            string sortBy,
            SortOrder? sortOrder)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}differentiatedpositions/announced?dpName={1}&name={2}&companyName={3}&limit={4}&offset={5}&sortBy={6}&sortOrder={7}&offersDeadlineDate={8}&noticeDate={9}",
                    serverLocation,
                    dpName,
                    name,
                    companyName,
                    limit,
                    offset,
                    sortBy,
                    sortOrder,
                    offersDeadlineDate?.ToISO8601Format(),
                    noticeDate?.ToISO8601Format()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetArchivedContractDifferentiatedPositions(string accessToken, string dpName, string name, string companyName, int? limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}differentiatedpositions/archived?dpName={1}&name={2}&companyName={3}&limit={4}&offset={5}",
                    serverLocation,
                    dpName,
                    name,
                    companyName,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetContractDifferentiatedPosition(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}differentiatedpositions/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[Route("submitted")]
        public static JObject GetRegistrationSubmittedOffers(string accessToken, string dpName, string name, string companyName, DateTime? offerSubmitDate, int offset, int? limit, string sortBy, SortOrder? sortOrder)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/offers/submitted?dpName={1}&name={2}&companyName={3}&limit={4}&offset={5}&sortBy={6}&sortOrder={7}&submitDate={8}",
                    serverLocation,
                    dpName,
                    name,
                    companyName,
                    limit,
                    offset,
                    sortBy,
                    sortOrder,
                    offerSubmitDate?.ToISO8601Format()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetRegistrationDraftOffers(string accessToken, string dpName, string name, string companyName, int offset, int? limit)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = $"{serverLocation}registration/offers/drafts?dpName={dpName}&name={name}&companyName={companyName}&limit={limit}&offset={offset}";

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetRegistrationOfferInfo(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/offers/{1}/info",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetRegistrationOffer(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/offers/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetNewRegistrationOffer(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/offers/new?dpGid={1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject CreateRegistrationOffer2(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/offers?dpGid={1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("")]
        public static JObject CreateRegistrationOffer(Guid dpGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = $"{serverLocation}registration/offers?dpGid={dpGid}";

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPut]
        //[Route("{offerGid:guid}")]
        public static JObject UpdateRegistrationOffer(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = $"{serverLocation}registration/offers/{gid}";

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("{offerGid:guid}/submit")]
        public static void SubmitRegistrationOffer(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = $"{serverLocation}registration/offers/{gid}/submit";

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("{offerGid:guid}/withdraw")]
        public static void WithdrawRegistrationOffer(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = $"{serverLocation}registration/offers/{gid}/withdraw";

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        #endregion

        #region Report

        public static JObject GetRegistrationOffers(string accessToken, Guid contractGid, int offset = 0, int? limit = null)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/offers",
                    serverLocation,
                    contractGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetRegistrationOfferInfo(Guid contractGid, Guid offerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/offers/{2}/info",
                    serverLocation,
                    contractGid,
                    offerGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetRegistrationOffer(Guid contractGid, Guid offerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/offers/{2}",
                    serverLocation,
                    contractGid,
                    offerGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        #endregion

        #region Private

        public static JObject PrivateGetRegistrationOffer(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractOffers/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        #endregion
    }
}
