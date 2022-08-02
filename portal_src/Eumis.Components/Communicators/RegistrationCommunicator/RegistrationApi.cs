using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class RegistrationApi
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations")]
        public static JObject CreateRegistration(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registrations/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/registrations/activate")]
        public static JObject ActivateRegistration(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registrations/activate/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/canActivate")]
        public static bool CanActivateRegistration(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registrations/canActivate/",
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
                    "{0}registration/info/",
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
                    "{0}registration/info/",
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
                    "{0}registration/password/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/startPasswordRecovery")]
        public static void StartPasswordRecovery(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registrations/startPasswordRecovery/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/recoverPassword")]
        public static void RecoverPassword(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registrations/recoverPassword/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/canRecoverPassword")]
        public static bool CanRecoverPassword(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registrations/canRecoverPassword/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAnonymousRequest<bool>(url, body);
        }
    }
}