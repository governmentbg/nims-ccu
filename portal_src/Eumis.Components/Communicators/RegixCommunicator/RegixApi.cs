using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using Eumis.Common.Helpers;
using Eumis.Common.Config;

namespace Eumis.Components.Communicators
{
    public class RegixApi
    {
        public static JObject GetCompany(string token, string procedureCode, string uin, string uinType)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}regix/?uin={1}&uinType={2}&code={3}",
                    serverLocation,
                    uin,
                    uinType,
                    procedureCode
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }
    }
}
