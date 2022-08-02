using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractRegistrationDO
    {
        public ContractRegistrationDO()
        {
        }

        public ContractRegistrationDO(ContractRegistration contractRegistration)
        {
            this.ContractRegistrationId = contractRegistration.ContractRegistrationId;
            this.Uin = contractRegistration.Uin;
            this.UinType = contractRegistration.UinType;
            this.Email = contractRegistration.Email;
            this.FirstName = contractRegistration.FirstName;
            this.LastName = contractRegistration.LastName;
            this.Phone = contractRegistration.Phone;
        }

        public int ContractRegistrationId { get; set; }

        public string Email { get; set; }

        public string Uin { get; set; }

        public PersonalUinType? UinType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}
