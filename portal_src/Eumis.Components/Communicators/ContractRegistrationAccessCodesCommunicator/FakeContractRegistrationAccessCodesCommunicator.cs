using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public class FakeContractRegistrationAccessCodesCommunicator : IContractRegistrationAccessCodesCommunicator
    {
        public string Login(string email, string code, string registrationNumber)
        {
            return "accessToken";
        }

        public ContractRegistrationAccessCodePVO GetRegistrationInfo(string accessToken)
        {
            return new ContractRegistrationAccessCodePVO()
            {
                email = "fake@fake.com",
                firstName = "fake"
            };
        }

        public ContractRegistrationAccessCodesPVO GetContractRegistrationAccessCodes(string accessToken,
            Guid contractGid, int offset = 0, int? limit = null)
        {
            return new ContractRegistrationAccessCodesPVO();
        }

        public ContractRegistrationAccessCodePVO GetContractRegistrationAccessCode(string accessToken, Guid contractGid,
            Guid accessCodeGid)
        {
            return new ContractRegistrationAccessCodePVO();
        }

        public ContractRegistrationAccessCodePVO CreateContractRegistrationAccessCode(string accessToken,
            Guid contractGid, ContractRegistrationAccessCodePVO accessCodePVO)
        {
            return new ContractRegistrationAccessCodePVO();
        }

        public ContractRegistrationAccessCodePVO UpdateContractRegistrationAccessCode(string accessToken,
            Guid contractGid, Guid accessCodeGid, ContractRegistrationAccessCodePVO accessCodePVO)
        {
            return new ContractRegistrationAccessCodePVO();
        }
    }
}