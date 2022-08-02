using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class PaymentRequestApi
    {
        #region Report

        public static JObject GetPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    paymentRequestGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject CreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment?type={3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    type
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PutPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    paymentRequestGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static void DeletePaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    paymentRequestGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanCreatePaymentRequest(Guid contractGid, Guid packageGid, string type, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/canCreate?type={3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    type
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static JObject SubmitPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/{3}/enter",
                    serverLocation,
                    contractGid,
                    packageGid,
                    paymentRequestGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject MakeDraftPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/{3}/makedraft",
                    serverLocation,
                    contractGid,
                    packageGid,
                    paymentRequestGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject MakeActualPaymentRequest(Guid contractGid, Guid packageGid, Guid paymentRequestGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/payment/{3}/makeActual",
                    serverLocation,
                    contractGid,
                    packageGid,
                    paymentRequestGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion

        #region Private

        public static JObject PrivateGetPaymentRequest(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportPayments/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PrivatePutPaymentRequest(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportPayments/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PrivateSubmitPaymentRequest(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportPayments/{1}/enter",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion
    }
}