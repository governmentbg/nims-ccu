using System.Configuration;
using Eumis.Common.Helpers;
using Newtonsoft.Json.Linq;

namespace Eumis.Components.Communicators
{
    public class ContractRegistrationApi
    {
        //[Route("api/token")]
        public static JObject Login(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}token",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAccessTokenRequest<JObject>(url, body);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/contractregs/activate")]
        public static JObject ActivateRegistration(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/activate/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/contractregs/canActivate")]
        public static bool CanActivateRegistration(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/canActivate/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<bool>(url, body);
        }

        //[Route("api/registration/info")]
        public static JObject GetRegistrationInfo(string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/info/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[HttpPost]
        //[Route("api/registration/info")]
        public static JObject UpdateRegistrationInfo(string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/info/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }
        
        //[HttpPost]
        //[Route("api/registration/password")]
        public static void ChangeCurrentUserPassword(string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/password/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/contractregs/startPasswordRecovery")]
        public static void StartPasswordRecovery(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/startPasswordRecovery/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/contractregs/recoverPassword")]
        public static void RecoverPassword(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/recoverPassword/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/contractregs/canRecoverPassword")]
        public static bool CanRecoverPassword(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/canRecoverPassword/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<bool>(url, body);
        }
    }
}