using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.ContractRegistrations.ViewObjects
{
    public class ContractRegistrationNomVO : EntityNomVO
    {
        public int ContractRegistrationId { get; set; }

        public string Email { get; set; }

        public PersonalUinType UinType { get; set; }

        public string Uin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}
