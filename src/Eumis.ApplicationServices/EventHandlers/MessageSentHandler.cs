using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Messages.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class MessageSentHandler : EventHandler<MessageSentEvent>
    {
        private IMessagesRepository messagesRepository;
        private IUsersRepository usersRepository;
        private IEmailsRepository emailsRepository;

        public MessageSentHandler(
            IMessagesRepository messagesRepository,
            IUsersRepository usersRepository,
            IEmailsRepository emailsRepository)
        {
            this.messagesRepository = messagesRepository;
            this.usersRepository = usersRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(MessageSentEvent e)
        {
            var message = this.messagesRepository.Find(e.MessageId);
            var senderFullname = this.usersRepository.GetUserFullname(message.SenderId);

            foreach (var recipient in message.Recipients)
            {
                var recipientEmail = this.usersRepository.GetUserEmail(recipient.RecipientId);

                Email email = new Email(
                    recipientEmail,
                    EmailTemplate.NewMsgMessage,
                    JObject.FromObject(
                        new
                        {
                            messageId = message.MessageId,
                            sender = senderFullname,
                            title = message.Title,
                            content = message.Content,
                        }));

                this.emailsRepository.Add(email);
            }
        }
    }
}
