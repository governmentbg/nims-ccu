using Eumis.Domain.Registrations;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects
{
    public class RegistrationDO
    {
        public RegistrationDO()
        {
        }

        public RegistrationDO(Registration reg)
        {
            this.Email = reg.Email;
            this.FirstName = reg.FirstName;
            this.LastName = reg.LastName;
            this.Phone = reg.Phone;
            this.Version = reg.Version;
        }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public byte[] Version { get; set; }
    }
}
