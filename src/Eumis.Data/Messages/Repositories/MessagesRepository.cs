using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Messages.ViewObjects;
using Eumis.Domain.Messages;
using Eumis.Domain.Users;

namespace Eumis.Data.Messages.Repositories
{
    internal class MessagesRepository : AggregateRepository<Message>, IMessagesRepository
    {
        private IAccessContext accessContext;

        public MessagesRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(unitOfWork)
        {
            this.accessContext = accessContext;
        }

        protected override Expression<Func<Message, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Message, object>>[]
                {
                    m => m.MessageFiles,
                    m => m.Recipients,
                };
            }
        }

        public MessagesCountVO GetMessagesCount()
        {
            var userId = this.accessContext.UserId;
            var outgoingQuery = (from m in this.unitOfWork.DbContext.Set<Message>()
                                 where m.SenderId == userId
                                 group m by m.Status into g0
                                 select new { Status = g0.Key, Count = g0.Count() }).ToList();

            var ingoingQuery = (from mr in this.unitOfWork.DbContext.Set<MessageRecipient>()
                                join m in this.unitOfWork.DbContext.Set<Message>() on mr.MessageId equals m.MessageId
                                where mr.RecipientId == userId && m.Status == MessageStatus.Sent
                                group mr by mr.IsArchived into g0
                                select new { IsArchived = g0.Key, Count = g0.Count() }).ToList();

            var draft = outgoingQuery.SingleOrDefault(m => m.Status == MessageStatus.Draft);
            var sent = outgoingQuery.SingleOrDefault(m => m.Status == MessageStatus.Sent);
            var inbox = ingoingQuery.SingleOrDefault(m => !m.IsArchived);
            var archived = ingoingQuery.SingleOrDefault(m => m.IsArchived);

            return new MessagesCountVO
            {
                Draft = draft == null ? 0 : draft.Count,
                Sent = sent == null ? 0 : sent.Count,
                Inbox = inbox == null ? 0 : inbox.Count,
                Archive = archived == null ? 0 : archived.Count,
            };
        }

        public int GetNewMessagesCount()
        {
            var userId = this.accessContext.UserId;
            return (from mr in this.unitOfWork.DbContext.Set<MessageRecipient>()
                    join m in this.unitOfWork.DbContext.Set<Message>() on mr.MessageId equals m.MessageId
                    where m.Status == MessageStatus.Sent && mr.RecipientId == userId && !mr.RecieveDate.HasValue && !mr.IsArchived
                    select m.MessageId).Count();
        }

        public OutgoingMessagesVO GetOutgoingMessages(MessageType type, int offset, int limit)
        {
            var predicate = PredicateBuilder.True<Message>()
                .And(m => m.SenderId == this.accessContext.UserId);

            switch (type)
            {
                case MessageType.Draft:
                    predicate = predicate.And(m => m.Status == MessageStatus.Draft);
                    break;
                case MessageType.Sent:
                    predicate = predicate.And(m => m.Status == MessageStatus.Sent);
                    break;
                default:
                    throw new InvalidOperationException("Invalid outgoing message type.");
            }

            var query =
                from m in this.unitOfWork.DbContext.Set<Message>().Where(predicate)
                orderby new { m.Number, m.CreateDate } descending
                select new
                {
                    m.MessageId,
                    m.Title,
                    m.Content,
                    m.SentDate,
                };

            var messages = query.WithOffsetAndLimit(offset, limit).ToList();
            var messageIds = messages.Select(m => m.MessageId);

            var recipients = (from mr in this.unitOfWork.DbContext.Set<MessageRecipient>()
                              join u in this.unitOfWork.DbContext.Set<User>() on mr.RecipientId equals u.UserId
                              where messageIds.Contains(mr.MessageId)
                              select new { mr.MessageId, u.Fullname }).ToList();

            return new OutgoingMessagesVO
            {
                Count = query.Count(),
                Messages = messages.Select(m =>
                    new OutgoingMessageVO
                    {
                        MessageId = m.MessageId,
                        Title = m.Title,
                        Content = m.Content,
                        SentDate = m.SentDate,
                        Recipients = recipients.Where(r => r.MessageId == m.MessageId).Select(r => r.Fullname).ToList(),
                    }).ToList(),
            };
        }

        public IngoingMessagesVO GetIngoingMessages(MessageType type, int offset, int limit)
        {
            var predicate = PredicateBuilder.True<MessageRecipient>()
                .And(m => m.RecipientId == this.accessContext.UserId);

            switch (type)
            {
                case MessageType.Inbox:
                    predicate = predicate.And(m => !m.IsArchived);
                    break;
                case MessageType.Archived:
                    predicate = predicate.And(m => m.IsArchived);
                    break;
                default:
                    throw new InvalidOperationException("Invalid ingoing message type.");
            }

            var query = from mr in this.unitOfWork.DbContext.Set<MessageRecipient>().Where(predicate)
                        join m in this.unitOfWork.DbContext.Set<Message>() on mr.MessageId equals m.MessageId
                        join s in this.unitOfWork.DbContext.Set<User>() on m.SenderId equals s.UserId
                        where m.Status == MessageStatus.Sent
                        orderby m.Number descending
                        select new IngoingMessageVO
                        {
                            MessageId = m.MessageId,
                            Title = m.Title,
                            Content = m.Content,
                            Sender = s.Fullname,
                            SentDate = m.SentDate,
                            RecieveDate = mr.RecieveDate,
                        };

            return new IngoingMessagesVO
            {
                Count = query.Count(),
                Messages = query.WithOffsetAndLimit(offset, limit).ToList(),
            };
        }

        public bool IsMessageFileVisible(int messageId, Guid fileKey)
        {
            var userId = this.accessContext.UserId;
            var subquery = from mr in this.unitOfWork.DbContext.Set<MessageRecipient>()
                           where mr.MessageId == messageId
                           select mr.RecipientId;

            return (from m in this.unitOfWork.DbContext.Set<Message>()
                    join mf in this.unitOfWork.DbContext.Set<MessageFile>() on m.MessageId equals mf.MessageId
                    where mf.BlobKey == fileKey && (m.SenderId == userId || (m.Status == MessageStatus.Sent && subquery.Contains(userId)))
                    select m.MessageId).Any();
        }

        public int GetNextMessageNumber()
        {
            var lastOrderNumver = this.unitOfWork.DbContext.Set<Message>()
                    .Max(m => (int?)m.Number);

            return lastOrderNumver.HasValue ? lastOrderNumver.Value + 1 : 1;
        }

        public override Message Find(int messageId)
        {
            var message = base.Find(messageId);

            if (this.accessContext.UserId != message.SenderId && !(message.Status == MessageStatus.Sent && message.Recipients.Select(r => r.RecipientId).Contains(this.accessContext.UserId)))
            {
                throw new InvalidOperationException("Cannot view message.");
            }

            return message;
        }

        public new void Remove(Message message)
        {
            if (message.Status != MessageStatus.Draft)
            {
                throw new InvalidOperationException("Cannot delete nondraft message.");
            }

            base.Remove(message);
        }

        public IList<string> GetMessageRecipientNames(int messageId)
        {
            return (from r in this.unitOfWork.DbContext.Set<MessageRecipient>()
                    join ru in this.unitOfWork.DbContext.Set<User>() on r.RecipientId equals ru.UserId
                    where r.MessageId == messageId
                    select ru.Fullname).ToList();
        }

        private IList<MessageFileVO> GetMessageFiles(int messageId)
        {
            return (from f in this.unitOfWork.DbContext.Set<MessageFile>()
                    where f.MessageId == messageId
                    select new MessageFileVO
                    {
                        Key = f.BlobKey,
                        Name = f.Name,
                        Description = f.Description,
                    }).ToList();
        }
    }
}
