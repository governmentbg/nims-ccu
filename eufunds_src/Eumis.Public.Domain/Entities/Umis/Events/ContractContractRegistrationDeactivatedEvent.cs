﻿using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractContractRegistrationDeactivatedEvent : IDomainEvent
    {
        public int ContractId { get; set; }

        public int ContractRegistrationId { get; set; }
    }
}
