using Eumis.Domain.Registrations;

namespace Eumis.Web.Api.Registrations.DataObjects
{
    public class RegistrationDO
    {
        public RegistrationDO()
        {
        }

        public RegistrationDO(Registration registration)
        {
            this.RegistrationId = registration.RegistrationId;
            this.Email = registration.Email;
            this.FirstName = registration.FirstName;
            this.LastName = registration.LastName;
            this.Phone = registration.Phone;
            this.Version = registration.Version;
        }

        public int RegistrationId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public byte[] Version { get; set; }
    }
}
