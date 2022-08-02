using System;
using System.Configuration;
using System.Web;
using Eumis.Documents.Contracts;
using Newtonsoft.Json.Linq;
using Eumis.Common.Config;

namespace Eumis.Components.Communicators
{
    public class RegistrationCommunicator : IRegistrationCommunicator
    {
        public string Login(string email, string password)
        {
            var credentials = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:ServerCredentials").Split(',');

            //the password may contain characters outside the range allowed for the scope parameter and should be Escaped/Unescaped
            var scope = string.Format("reg:{0}:{1}", email, Uri.EscapeDataString(password));

            var body = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials&scope={2}",
                HttpUtility.UrlEncode(credentials[0]),
                HttpUtility.UrlEncode(credentials[1]),
                HttpUtility.UrlEncode(scope));

            return RegistrationApi.Login(body).SelectToken("access_token").Value<string>();
        }

        public void CreateRegistration(string email, string firstName, string lastName, string phone)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    email = email,
                    firstName = firstName,
                    lastName = lastName,
                    phone = phone
                });

            RegistrationApi.CreateRegistration(body);
        }

        public string ActivateRegistration(string activationCode, string password)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    activationCode = activationCode,
                    password = password
                });

            return RegistrationApi.ActivateRegistration(body).ToObject<ContractActivation>().accessToken;
        }

        public bool CanActivateRegistration(string activationCode)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    activationCode = activationCode
                });

            return RegistrationApi.CanActivateRegistration(body);
        }

        public string ActivateRegistration(string activationCode)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    activationCode = activationCode
                });

            return RegistrationApi.ActivateRegistration(body).ToObject<ContractActivation>().accessToken;
        }

        public ContractRegistrationInfo GetRegistrationInfo(string accessToken)
        {
            return RegistrationApi.GetRegistrationInfo(accessToken).ToObject<ContractRegistrationInfo>();
        }

        public ContractRegistrationInfo UpdateRegistrationInfo(string accessToken, string firstName, string lastName, string phone, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    firstName = firstName,
                    lastName = lastName,
                    phone = phone,
                    version = version
                });

            return RegistrationApi.UpdateRegistrationInfo(accessToken, body).ToObject<ContractRegistrationInfo>();
        }

        public void ChangeCurrentUserPassword(string accessToken, string oldPassword, string newPassword)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    oldPassword = oldPassword,
                    newPassword = newPassword
                });

            RegistrationApi.ChangeCurrentUserPassword(accessToken, body);
        }

        public void StartPasswordRecovery(string email)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    email = email,
                });

            RegistrationApi.StartPasswordRecovery(body);
        }

        public void RecoverPassword(string passwordRecoveryCode, string newPassword)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    passwordRecoveryCode = passwordRecoveryCode,
                    newPassword = newPassword
                });

            RegistrationApi.RecoverPassword(body);
        }

        public bool CanRecoverPassword(string passwordRecoveryCode)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    passwordRecoveryCode = passwordRecoveryCode
                });

            return RegistrationApi.CanRecoverPassword(body);
        }
    }
}