using System;
using System.Collections.Generic;
using Eumis.Data.Messages.ViewObjects;
using Eumis.Domain.Messages;

namespace Eumis.Data.Messages.Repositories
{
    public interface IMessagesRepository : IAggregateRepository<Message>
    {
        MessagesCountVO GetMessagesCount();

        int GetNewMessagesCount();

        OutgoingMessagesVO GetOutgoingMessages(MessageType type, int offset, int limit);

        IngoingMessagesVO GetIngoingMessages(MessageType type, int offset, int limit);

        bool IsMessageFileVisible(int messageId, Guid fileKey);

        int GetNextMessageNumber();

        IList<string> GetMessageRecipientNames(int messageId);
    }
}
