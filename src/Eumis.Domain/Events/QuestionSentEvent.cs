﻿using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class QuestionSentEvent : IDomainEvent
    {
        public int ProjectCommunicationId { get; set; }
    }
}
