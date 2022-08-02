using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public class FakeRegistrationCommunicator : IRegistrationCommunicator
    {
        public string Login(string email, string password)
        {
            return "accessToken";
        }

        public void CreateRegistration(string email, string firstName, string lastName, string phone)
        {
            
        }

        public string ActivateRegistration(string activationCode, string password)
        {
            return "accessToken";
        }

        public string ActivateRegistration(string activationCode)
        {
            return "accessToken";
        }

        public bool CanActivateRegistration(string activationCode)
        {
            return true;
        }

        public ContractRegistrationInfo GetRegistrationInfo(string accessToken)
        {
            return new ContractRegistrationInfo()
            {
                email = "fake@fake.com",
                firstName = "fake"
            };
        }

        public ContractRegistrationInfo UpdateRegistrationInfo(string accessToken, string firstName, string lastName,
            string phone, byte[] version)
        {
            return new ContractRegistrationInfo()
            {
                email = "fake@fake.com",
                firstName = "fake"
            };
        }

        public void ChangeCurrentUserPassword(string accessToken, string oldPassword, string newPassowrd)
        {
            
        }

        public void StartPasswordRecovery(string email)
        {

        }

        public void RecoverPassword(string passwordRecoveryCode, string newPassword)
        {

        }

        public bool CanRecoverPassword(string passwordRecoveryCode)
        {
            return true;
        }
    }
}