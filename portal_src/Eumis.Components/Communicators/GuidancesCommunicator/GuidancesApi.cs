using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class GuidancesApi
    {
        public static JArray GetGuidances(string module)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}guidances?module={1}",
                    serverLocation,
                    module
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JArray>(url);
        }
    }
}