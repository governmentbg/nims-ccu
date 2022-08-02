using Eumis.Common.Config;
using Monitorstat.IntegrationEumis.Host.Helpers;
using Monitorstat.IntegrationEumis.Host.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Communicators
{
    public static class MonitorstatAPI
    {
        private static readonly string BaseUrl = string.Format("{0}api/monitorstatFiles/", Configuration.ServerLocation);

        public static JObject RegisterDocument(RegisterEumisResultRequest request, string token)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            return ApiRequest.PostAuthorizationRequest<JObject>(BaseUrl, token, body);
        }
    }
}
