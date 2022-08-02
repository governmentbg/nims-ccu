using System;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Messages.Repositories;
using Eumis.Data.Messages.ViewObjects;
using Eumis.Domain.Messages;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Messages.DataObjects;

namespace Eumis.Web.Api.Messages.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IMessagesRepository messagesRepository;
        private IAuthorizer authorizer;
        private UserClaimsContextFactory userClaimsContextFactory;
        private IAccessContext accessContext;
        private IUserClaimsContext currentUserClaimsContext;

        public MessagesController(
            IUnitOfWork unitOfWork,
            IMessagesRepository messagesRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.messagesRepository = messagesRepository;
            this.authorizer = authorizer;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("{messageId:int}")]
        public MessageDO GetMessage(int messageId)
        {
            var message = this.messagesRepository.Find(messageId);

            var userClaimsContext = this.userClaimsContextFactory(message.SenderId);
            var username = userClaimsContext.Fullname + "(" + userClaimsContext.Username + ")";

            return new MessageDO(message, username);
        }

        [Route("~/api/sentMessages/{messageId:int}")]
        public MessageVO GetSentMessage(int messageId)
        {
            var message = this.messagesRepository.Find(messageId);

            if (message.Status != MessageStatus.Sent)
            {
                throw new InvalidOperationException("Message is not in status sent.");
            }

            var senderClaimsContext = this.userClaimsContextFactory(message.SenderId);

            return new MessageVO
            {
                MessageId = message.MessageId,
                Title = message.Title,
                Content = message.Content.MakeHtml(),
                Sender = senderClaimsContext.Fullname,
                SentDate = message.SentDate.Value,
                CreateDate = message.CreateDate,
                Recipients = this.messagesRepository.GetMessageRecipientNames(messageId),
                Files = message.MessageFiles.Select(f => new MessageFileVO
                {
                    Key = f.BlobKey,
                    Name = f.Name,
                    Description = f.Description,
                }).ToList(),
            };
        }

        [Route("~/api/ingoingMessages/{messageId:int}")]
        public MessageVO GetIngoingMessage(int messageId)
        {
            var message = this.messagesRepository.Find(messageId);

            if (message.Status != MessageStatus.Sent)
            {
                throw new InvalidOperationException("Message is not in status sent.");
            }

            var senderClaimsContext = this.userClaimsContextFactory(message.SenderId);

            return new MessageVO
            {
                MessageId = message.MessageId,
                Title = message.Title,
                Content = message.Content.MakeHtml(),
                Sender = senderClaimsContext.Fullname,
                SentDate = message.SentDate.Value,
                CreateDate = message.CreateDate,
                IsArchived = message.IsArchivedByUser(this.accessContext.UserId),
                Recipients = this.messagesRepository.GetMessageRecipientNames(messageId),
                Files = message.MessageFiles.Select(f => new MessageFileVO
                    {
                        Key = f.BlobKey,
                        Name = f.Name,
                        Description = f.Description,
                    }).ToList(),
            };
        }

        [HttpPut]
        [Route("{messageId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Messages.Edit), IdParam = "messageId")]
        public void UpdateMessage(int messageId, MessageDO message)
        {
            var oldMessage = this.messagesRepository.FindForUpdate(messageId, message.Version);

            oldMessage.UpdateAttributes(message.Title, message.Content, message.Recipients);

            var newFiles = message.Files
                .Where(f => f.Status == FileStatus.Added)
                .Select(f => new MessageFile { BlobKey = f.File.Key, Name = f.File.Name, Description = f.Description })
                .ToList();
            oldMessage.AddFiles(newFiles);

            var updatedFiles = message.Files
                .Where(f => f.Status == FileStatus.Updated)
                .Select(f => Tuple.Create<int, Guid, string, string>(f.MessageFileId.Value, f.File.Key, f.File.Name, f.Description))
                .ToList();
            oldMessage.UpdateFiles(updatedFiles);

            var removedFileIds = message.Files
                .Where(f => f.Status == FileStatus.Removed)
                .Select(f => f.MessageFileId.Value)
                .ToList();
            oldMessage.RemoveFiles(removedFileIds);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{messageId:int}/send")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Messages.Send), IdParam = "messageId")]
        public void SendMessage(int messageId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var message = this.messagesRepository.FindForUpdate(messageId, vers);

            message.SendMessage(this.messagesRepository.GetNextMessageNumber());

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{messageId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Messages.Delete), IdParam = "messageId")]
        public void DeleteMessage(int messageId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var message = this.messagesRepository.FindForUpdate(messageId, vers);

            this.messagesRepository.Remove(message);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{messageId:int}/archive")]
        [Transaction]
        public void ArchiveMessage(int messageId)
        {
            var message = this.messagesRepository.Find(messageId);

            message.SetArchivedByUser(this.accessContext.UserId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{messageId:int}/markAsRecieved")]
        [Transaction]
        public void MarkMessageAsRecieved(int messageId)
        {
            var message = this.messagesRepository.Find(messageId);

            message.SetRecievedByUser(this.accessContext.UserId);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public MessageDO NewMessage()
        {
            var username = this.currentUserClaimsContext.Fullname + "(" + this.currentUserClaimsContext.Username + ")";

            return new MessageDO(username);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Messages.Create))]
        public object CreateMessage(MessageDO message)
        {
            var newMessage = new Message(
                message.Title,
                message.Content,
                this.accessContext.UserId,
                message.Recipients,
                message.Files.Select(f => new MessageFile
                {
                    BlobKey = f.File.Key,
                    Name = f.File.Name,
                    Description = f.Description,
                }).ToList());

            this.messagesRepository.Add(newMessage);

            this.unitOfWork.Save();

            return new { MessageId = newMessage.MessageId };
        }

        [HttpGet]
        [Route("newFile")]
        public MessageFileDO NewFile()
        {
            return new MessageFileDO();
        }

        [Route("~/api/messagesCount")]
        public MessagesCountVO GetMessagesCount()
        {
            return this.messagesRepository.GetMessagesCount();
        }

        [Route("~/api/draftMessages")]
        public OutgoingMessagesVO GetDraftMessages(int offset, int limit)
        {
            return this.messagesRepository.GetOutgoingMessages(MessageType.Draft, offset, limit);
        }

        [Route("~/api/sentMessages")]
        public OutgoingMessagesVO GetSentMessages(int offset, int limit)
        {
            return this.messagesRepository.GetOutgoingMessages(MessageType.Sent, offset, limit);
        }

        [Route("~/api/inboxMessages")]
        public IngoingMessagesVO GetInboxMessages(int offset, int limit)
        {
            return this.messagesRepository.GetIngoingMessages(MessageType.Inbox, offset, limit);
        }

        [Route("~/api/archivedMessages")]
        public IngoingMessagesVO GetArchivedMessages(int offset, int limit)
        {
            return this.messagesRepository.GetIngoingMessages(MessageType.Archived, offset, limit);
        }

        [Route("newMessages")]
        public int GetNewMessagesCount()
        {
            return this.messagesRepository.GetNewMessagesCount();
        }
    }
}
