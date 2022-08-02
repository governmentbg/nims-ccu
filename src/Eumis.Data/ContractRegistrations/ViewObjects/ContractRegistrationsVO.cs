using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eumis.Data.ContractRegistrations.ViewObjects
{
    public class ContractRegistrationsVO
    {
        public int ContractRegistrationId { get; set; }

        public string Email { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public PersonalUinType UinType { get; set; }

        public string Uin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public IList<ContractRegistrationContractVO> Contracts { get; set; }
    }
}
