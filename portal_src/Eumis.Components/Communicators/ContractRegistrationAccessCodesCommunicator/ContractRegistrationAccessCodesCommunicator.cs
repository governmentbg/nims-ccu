using System;
using System.Configuration;
using System.Web;
using Eumis.Documents.Contracts;
using Newtonsoft.Json.Linq;
using Eumis.Common.Config;

namespace Eumis.Components.Communicators
{
    public class ContractRegistrationAccessCodesCommunicator : IContractRegistrationAccessCodesCommunicator
    {
        public string Login(string email, string code, string registrationNumber)
        {
            var credentials = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:ServerCredentials").Split(',');

            //the password may contain characters outside the range allowed for the scope parameter and should be Escaped/Unescaped
            var scope = string.Format("accesscode:{0}:{1}:{2}", email, code, Uri.EscapeDataString(registrationNumber));

            var body = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials&scope={2}",
                HttpUtility.UrlEncode(credentials[0]),
                HttpUtility.UrlEncode(credentials[1]),
                HttpUtility.UrlEncode(scope));

            return ContractRegistrationAccessCodesApi.Login(body).SelectToken("access_token").Value<string>();
        }

        public ContractRegistrationAccessCodePVO GetRegistrationInfo(string accessToken)
        {
            return ContractRegistrationAccessCodesApi.GetRegistrationInfo(accessToken).ToObject<ContractRegistrationAccessCodePVO>();
        }

        public ContractRegistrationAccessCodesPVO GetContractRegistrationAccessCodes(string accessToken,
            Guid contractGid, int offset = 0, int? limit = null)
        {
            return ContractRegistrationAccessCodesApi.GetContractRegistrationAccessCodes(accessToken, contractGid,
                offset, limit).ToObject<ContractRegistrationAccessCodesPVO>();
        }

        public ContractRegistrationAccessCodePVO GetContractRegistrationAccessCode(string accessToken, Guid contractGid,
            Guid accessCodeGid)
        {
            return ContractRegistrationAccessCodesApi.GetContractRegistrationAccessCode(accessToken, contractGid,
                accessCodeGid).ToObject<ContractRegistrationAccessCodePVO>();
        }

        public ContractRegistrationAccessCodePVO CreateContractRegistrationAccessCode(string accessToken,
            Guid contractGid, ContractRegistrationAccessCodePVO accessCodePVO)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(accessCodePVO);

            return ContractRegistrationAccessCodesApi.CreateContractRegistrationAccessCode(accessToken, contractGid, body)
                .ToObject<ContractRegistrationAccessCodePVO>();
        }

        public ContractRegistrationAccessCodePVO UpdateContractRegistrationAccessCode(string accessToken,
            Guid contractGid, Guid accessCodeGid, ContractRegistrationAccessCodePVO accessCodePVO)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(accessCodePVO);

            return ContractRegistrationAccessCodesApi.UpdateContractRegistrationAccessCode(accessToken, contractGid, accessCodeGid, body)
                .ToObject<ContractRegistrationAccessCodePVO>();
        }
    }
}