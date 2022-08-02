using Eumis.Common.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Communicators
{
    public static class MonitorstatAPI
    {
        public static JObject GetSurveys(int year, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/surveys/{1}",
                    MonitorstatConfig.ServerLocation,
                    year);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject GetReports(int year, string surveyCode, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/surveys/reports?year={1}&surveyCode={2}",
                    MonitorstatConfig.ServerLocation,
                    year,
                    surveyCode);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        internal static JValue CreateOperationalProgramme(string body, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/mapNodes/programmes",
                    MonitorstatConfig.ServerLocation);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JValue>(url, token, body);
        }

        internal static JValue CreateProgrammePriority(string body, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/mapNodes/programmePriorities",
                    MonitorstatConfig.ServerLocation);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JValue>(url, token, body);
        }

        internal static JValue CreateProcedure(string body, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/mapNodes/procedures",
                    MonitorstatConfig.ServerLocation);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JValue>(url, token, body);
        }

        public static JValue CreateProcedureInquiryRequest(string body, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/procedureRequests",
                    MonitorstatConfig.ServerLocation);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JValue>(url, token, body);
        }

        public static JValue CreateSubjectRequest(string body, string token)
        {
            var url = string.Format(
                    "{0}monitorstat/subjectRequests",
                    MonitorstatConfig.ServerLocation);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JValue>(url, token, body);
        }
    }
}
