using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class SpendingPlanApi
    {
        public static JObject GetSpendingPlan(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractSpendingPlan/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PutSpendingPlan(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractSpendingPlan/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject SubmitSpendingPlan(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractSpendingPlan/{1}/enter",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        //[RoutePrefix("api/contractreg/contracts/{contractGid:guid}/spendingPlan")]
        public static JObject GetContractSpendingPlans(string accessToken, Guid contractGid, int offset = 0,
            int? limit = null)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/spendingPlan?limit={2}&offset={3}",
                    serverLocation,
                    contractGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[Route("{spendingPlanGid:guid}")]
        public static JObject GetContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/spendingPlan/{2}",
                    serverLocation,
                    contractGid,
                    spendingPlanGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[HttpPost]
        //[Route("")]
        public static JObject CreateContractSpendingPlan(string accessToken, Guid contractGid, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/spendingPlan",
                    serverLocation,
                    contractGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPut]
        //[Route("{spendingPlanGid:guid}")]
        public static JObject UpdateContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid,
            string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/spendingPlan/{2}",
                    serverLocation,
                    contractGid,
                    spendingPlanGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("{spendingPlanGid:guid}/submit")]
        public static void SubmitContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/spendingPlan/{2}/submit",
                    serverLocation,
                    contractGid,
                    spendingPlanGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }

        //[HttpDelete]
        //[Route("{spendingPlanGid:guid}")]
        public static void DeleteContractSpendingPlan(string accessToken, Guid contractGid, Guid spendingPlanGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/spendingPlan/{2}",
                    serverLocation,
                    contractGid,
                    spendingPlanGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }
    }
}