using System;
using System.Configuration;
using System.Web;
using Eumis.Documents.Contracts;
using Newtonsoft.Json.Linq;
using Eumis.Common.Config;

namespace Eumis.Components.Communicators
{
    public class ContractRegistrationCommunicator : IContractRegistrationCommunicator
    {
        public string Login(string email, string password)
        {
            var credentials = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:ServerCredentials").Split(',');

            //the password may contain characters outside the range allowed for the scope parameter and should be Escaped/Unescaped
            var scope = string.Format("contractreg:{0}:{1}", email, Uri.EscapeDataString(password));

            var body = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials&scope={2}",
                HttpUtility.UrlEncode(credentials[0]),
                HttpUtility.UrlEncode(credentials[1]),
                HttpUtility.UrlEncode(scope));

            return ContractRegistrationApi.Login(body).SelectToken("access_token").Value<string>();
        }

        public string ActivateRegistration(string activationCode, string password)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    activationCode = activationCode,
                    password = password
                });

            return ContractRegistrationApi.ActivateRegistration(body).ToObject<ContractActivation>().accessToken;
        }

        public bool CanActivateRegistration(string activationCode)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    activationCode = activationCode
                });

            return ContractRegistrationApi.CanActivateRegistration(body);
        }

        public string ActivateRegistration(string activationCode)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    activationCode = activationCode
                });

            return ContractRegistrationApi.ActivateRegistration(body).ToObject<ContractActivation>().accessToken;
        }

        public ContractRegistrationInfo GetRegistrationInfo(string accessToken)
        {
            return ContractRegistrationApi.GetRegistrationInfo(accessToken).ToObject<ContractRegistrationInfo>();
        }

        public ContractRegistrationInfo UpdateRegistrationInfo(string accessToken, string phone, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    phone = phone,
                    version = version
                });

            return ContractRegistrationApi.UpdateRegistrationInfo(accessToken, body).ToObject<ContractRegistrationInfo>();
        }

        public void ChangeCurrentUserPassword(string accessToken, string oldPassword, string newPassword)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    oldPassword = oldPassword,
                    newPassword = newPassword
                });

            ContractRegistrationApi.ChangeCurrentUserPassword(accessToken, body);
        }

        public void StartPasswordRecovery(string email)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    email = email,
                });

            ContractRegistrationApi.StartPasswordRecovery(body);
        }

        public void RecoverPassword(string passwordRecoveryCode, string newPassword)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    passwordRecoveryCode = passwordRecoveryCode,
                    newPassword = newPassword
                });

            ContractRegistrationApi.RecoverPassword(body);
        }

        public bool CanRecoverPassword(string passwordRecoveryCode)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    passwordRecoveryCode = passwordRecoveryCode
                });

            return ContractRegistrationApi.CanRecoverPassword(body);
        }
    }
}