using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class MailApi
    {
        public static JObject Send(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}emails/send",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<JObject>(url, body);
        } 
    }
}