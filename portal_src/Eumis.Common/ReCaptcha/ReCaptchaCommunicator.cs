using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Eumis.Common.ReCaptcha
{
    public static class ReCaptchaCommunicator
    {
        public static ReCaptchaResponse GetReCaptchaResponse(string clientResponse, string serverKey)
        {
            var client = new WebClient();

            string url = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", serverKey, clientResponse, GetIpAddress());

            var response = client.DownloadString(url);
            var googleresponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(response);

            if (googleresponse.ErrorCodes != null && googleresponse.ErrorCodes.Count > 0)
            {
                if(googleresponse.ErrorCodes.Count > 0)
                {
                    throw new Exception(string.Join(";", googleresponse.ErrorCodes));
                }

            }
            return googleresponse;
        }

        private static string GetIpAddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            var ipAddress = string.Empty;

#if !REVERSE_PROXY_MODE

            ipAddress = request.UserHostAddress;
#else
            ipAddress = request.Headers["X-Forwarded-For"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = request.UserHostAddress;
            }
#endif
            return ipAddress;
        }
    }

}
