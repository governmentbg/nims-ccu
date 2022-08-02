using Eumis.Domain.Contracts;

namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.DataObjects
{
    public class ContractRegistrationDO
    {
        public ContractRegistrationDO()
        {
        }

        public ContractRegistrationDO(ContractRegistration contractReg)
        {
            this.Email = contractReg.Email;
            this.FirstName = contractReg.FirstName;
            this.LastName = contractReg.LastName;
            this.Phone = contractReg.Phone;
            this.Version = contractReg.Version;
        }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public byte[] Version { get; set; }
    }
}
