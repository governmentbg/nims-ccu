namespace Eumis.PortalIntegration.Api.Core
{
    public class RecoverPasswordDO
    {
        public string PasswordRecoveryCode { get; set; }

        public string NewPassword { get; set; }
    }
}
