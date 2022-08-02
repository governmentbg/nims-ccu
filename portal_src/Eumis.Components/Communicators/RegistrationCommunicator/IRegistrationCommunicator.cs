using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public interface IRegistrationCommunicator
    {
        //[Route("api/token")]
        string Login(string email, string password);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations")]
        void CreateRegistration(string email, string firstName, string lastName, string phone);

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/registrations/activate")]
        string ActivateRegistration(string activationCode, string password);

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/registrations/canActivate")]
        bool CanActivateRegistration(string activationCode);

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/registrations/canActivate")]
        string ActivateRegistration(string activationCode);

        //[Route("api/registration/info")]
        ContractRegistrationInfo GetRegistrationInfo(string accessToken);

        //[HttpPost]
        //[Route("api/registration/info")]
        ContractRegistrationInfo UpdateRegistrationInfo(string accessToken, string firstName, string lastName, string phone, byte[] version);

        //[HttpPost]
        //[Route("api/registration/password")]
        void ChangeCurrentUserPassword(string accessToken, string oldPassword, string newPassowrd);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/startPasswordRecovery")]
        void StartPasswordRecovery(string email);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/recoverPassword")]
        void RecoverPassword(string passwordRecoveryCode, string newPassword);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("api/registrations/canRecoverPassword")]
        bool CanRecoverPassword(string passwordRecoveryCode);
    }
}