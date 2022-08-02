using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public interface IContractRegistrationAccessCodesCommunicator
    {
        //[Route("api/token")]
        string Login(string email, string code, string registrationNumber);

        //[Route("api/registration/info")]
        ContractRegistrationAccessCodePVO GetRegistrationInfo(string accessToken);

        ContractRegistrationAccessCodesPVO GetContractRegistrationAccessCodes(string accessToken, Guid contractGid, int offset = 0, int? limit = null);

        ContractRegistrationAccessCodePVO GetContractRegistrationAccessCode(string accessToken, Guid contractGid, Guid accessCodeGid);

        ContractRegistrationAccessCodePVO CreateContractRegistrationAccessCode(string accessToken, Guid contractGid, ContractRegistrationAccessCodePVO accessCodePVO);

        ContractRegistrationAccessCodePVO UpdateContractRegistrationAccessCode(string accessToken, Guid contractGid, Guid accessCodeGid, ContractRegistrationAccessCodePVO accessCodePVO);
    }
}