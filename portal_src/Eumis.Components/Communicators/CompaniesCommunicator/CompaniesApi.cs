using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using Eumis.Common.Helpers;
using Eumis.Common.Config;

namespace Eumis.Components.Communicators
{
    public class CompaniesApi
    {
        public static JObject GetCompany(string uin, string uinType)
        {
            string serverLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:RegistersServerLocation");
            if (!serverLocation.EndsWith("/"))
            {
                serverLocation += "/";
            }

            var url = string.Format
                (
                    "{0}company?uin={1}&uinType={2}",
                    serverLocation,
                    uin,
                    uinType
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        public static JObject GetEumisCompany(string uin, string uinType)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}companies?uin={1}&uinType={2}",
                    serverLocation,
                    uin,
                    uinType
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        } 
    }
}