using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class ProcurementsApi
    {
        public static JObject GetProcurements(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractProcurements/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PutProcurements(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractProcurements/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject SubmitProcurements(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractProcurements/{1}/enter",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        //[RoutePrefix("api/contractreg/contracts/{contractGid:guid}/procurements")]
        public static JObject GetContractProcurements(string accessToken, Guid contractGid, int offset = 0,
            int? limit = null)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements?limit={2}&offset={3}",
                    serverLocation,
                    contractGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JArray GetCentralProcurements(string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractProcurements/centralProcurements",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JArray>(url, token);
        }

        //[Route("{procurementGid:guid}")]
        public static JObject GetContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements/{2}",
                    serverLocation,
                    contractGid,
                    procurementGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[Route("{procurementGid:guid/edit}")]
        public static JObject GetContractProcurementForEdit(string accessToken, Guid contractGid, Guid procurementGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements/{2}/edit",
                    serverLocation,
                    contractGid,
                    procurementGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[HttpPost]
        //[Route("")]
        public static JObject CreateContractProcurement(string accessToken, Guid contractGid, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements",
                    serverLocation,
                    contractGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPut]
        //[Route("{procurementGid:guid}")]
        public static JObject UpdateContractProcurement(string accessToken, Guid contractGid, Guid procurementGid,
            string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements/{2}",
                    serverLocation,
                    contractGid,
                    procurementGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("{procurementGid:guid}/submit")]
        public static void SubmitContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements/{2}/submit",
                    serverLocation,
                    contractGid,
                    procurementGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }

        //[HttpDelete]
        //[Route("{procurementGid:guid}")]
        public static void DeleteContractProcurement(string accessToken, Guid contractGid, Guid procurementGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/procurements/{2}",
                    serverLocation,
                    contractGid,
                    procurementGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }
    }
}