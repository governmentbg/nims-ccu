using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Messages;

namespace Eumis.Web.Api.Messages.DataObjects
{
    public class MessageDO
    {
        public MessageDO()
        {
            this.Files = new List<MessageFileDO>();
            this.Recipients = new List<int>();
        }

        public MessageDO(string createdByUser)
        {
            var currentDate = DateTime.Now;

            this.Status = MessageStatus.Draft;
            this.Sender = createdByUser;
            this.CreateDate = currentDate;

            this.Files = new List<MessageFileDO>();
            this.Recipients = new List<int>();
        }

        public MessageDO(Message message, string createdByUser)
        {
            this.MessageId = message.MessageId;
            this.Status = message.Status;
            this.Title = message.Title;
            this.Content = message.Content;
            this.Sender = createdByUser;
            this.SentDate = message.SentDate;
            this.CreateDate = message.CreateDate;
            this.Version = message.Version;
            this.Files = message.MessageFiles.Select(f => new MessageFileDO(f)).ToList();
            this.Recipients = message.Recipients.Select(r => r.RecipientId).ToList();
        }

        public int? MessageId { get; set; }

        public MessageStatus? Status { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }

        public DateTime? SentDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public byte[] Version { get; set; }

        public IList<int> Recipients { get; set; }

        public IList<MessageFileDO> Files { get; set; }
    }
}
