using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Components.Communicators
{
    public enum ApiError
    {
        // EumisOAuthErrors
        unauthorized,
        unauthorizedClient,
        invalidClientId,
        unknownScopeFormat,
        registrationNotActivated,
        deletedOrLocked,

        // PortalIntegrationErrors
        invalidActivationCode,
        invalidPasswordRecoveryCode,
        registrationEmailExists,
        registrationEmailDoesNotExist,
        wrongOldPassword,

        //drafts
        updateConcurrencyError,
        objectNotFound,

        //procedures
        procedureInactive,

        //messages
        messageCanceled,
        messageTimedOut,

        //contract registrations
        accessCodeEmailNotUnique,
        accessCodeNotActive,

        unknown
    }

    public class ContractError
    {
        public string error { get; set; }
    }

    public class ApiErrorHandling
    {
        public static ApiError GetError(WebException exception)
        {
            try
            {
                using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    var responseFromServer = reader.ReadToEnd();
                    var error = JsonConvert.DeserializeObject<ContractError>(responseFromServer).error;

                    switch (error)
                    {
                         // EumisOAuthErrors
                        case "unauthorized":
                            return ApiError.unauthorized;
                        case "unauthorizedClient":
                            return ApiError.unauthorizedClient;
                        case "invalidClientId":
                            return ApiError.invalidClientId;
                        case "unknownScopeFormat":
                            return ApiError.unknownScopeFormat;
                        case "registrationNotActivated":
                            return ApiError.registrationNotActivated;
                        case "deletedOrLocked":
                            return ApiError.deletedOrLocked;


                        // PortalIntegrationErrors
                        case "invalidActivationCode":
                            return ApiError.invalidActivationCode;
                        case "invalidPasswordRecoveryCode":
                            return ApiError.invalidPasswordRecoveryCode;
                        case "registrationEmailExists":
                            return ApiError.registrationEmailExists;
                        case "registrationEmailDoesNotExist":
                            return ApiError.registrationEmailDoesNotExist;
                        case "wrongOldPassword":
                            return ApiError.wrongOldPassword;
                        case "updateConcurrencyError":
                            return ApiError.updateConcurrencyError;
                        case "objectNotFound":
                            return ApiError.objectNotFound;
                        case "procedureInactive":
                            return ApiError.procedureInactive;
                        case "messageCanceled":
                            return ApiError.messageCanceled;
                        case "messageTimedOut":
                            return ApiError.messageTimedOut;
                        case "accessCodeEmailNotUnique":
                            return ApiError.accessCodeEmailNotUnique;
                        case "accessCodeNotActive":
                            return ApiError.accessCodeNotActive;
                        default:
                            return ApiError.unknown;
                    }
                }
            }
            catch
            {
                return ApiError.unknown;
            }
        }

        public static void HandleDraftCommunicationExceptions(Exception exception)
        {
            if (exception is WebException)
            {
                var webError = ApiErrorHandling.GetError((WebException)exception);

                if (webError == ApiError.updateConcurrencyError)
                {
                    throw new System.Web.HttpException(505, "Update Concurrency Error");
                }
                else if (webError == ApiError.objectNotFound)
                {
                    throw new System.Web.HttpException(501, "Object Not Found");
                }
                else if (webError == ApiError.messageCanceled)
                {
                    throw new System.Web.HttpException(502, "Message Canceled");
                }
            }

            throw new System.Web.HttpException(503, "Service Unavailable");
        }
    }
}
