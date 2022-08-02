using Eumis.Common.Config;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.Communicators
{
    public static class RegixAPI
    {
        public static JObject GetValidPerson(string personalBulstat, string token)
        {
            var url = string.Format(
                    "{0}api/regix/GraoNBD/{1}/ValidPersonSearch/",
                    RegixConfig.ServerLocation,
                    personalBulstat);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        internal static JObject GetPersonalIdentity(string personalBulstat, string documentIdNumber, string token)
        {
            var url = string.Format(
                    "{0}api/regix/MVRBDS/GetPersonalIdentity?personalBulstat={1}&identityDocumentNumber={2}",
                    RegixConfig.ServerLocation,
                    personalBulstat,
                    documentIdNumber);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        internal static JObject GetActualState(string uin, string token)
        {
            var url = string.Format(
                    "{0}api/regix/AVTR/GetActualState?uic={1}",
                    RegixConfig.ServerLocation,
                    uin);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        internal static JObject GetStateOfPlay(string uin, string token)
        {
            var url = string.Format(
                    "{0}api/regix/AVBulstat2/GetStateOfPlay?uic={1}",
                    RegixConfig.ServerLocation,
                    uin);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        internal static JObject GetNPORegistrationInfo(string uin, string token)
        {
            var url = string.Format(
                    "{0}api/regix/MPNPO/GetNPORegistrationInfo?uic={1}",
                    RegixConfig.ServerLocation,
                    uin);

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }
    }
}
