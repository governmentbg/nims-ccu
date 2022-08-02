﻿using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ContractRegistrationCreatedEvent : IDomainEvent
    {
        public string Email { get; set; }

        public string ActivationCode { get; set; }
    }
}
