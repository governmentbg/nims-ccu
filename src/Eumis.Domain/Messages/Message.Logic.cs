using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.Events;

namespace Eumis.Domain.Messages
{
    public partial class Message
    {
        public void UpdateAttributes(string title, string content, IList<int> recipients)
        {
            this.AssertIsDraft();

            this.Title = title;
            this.Content = content;

            var removedRecipients = this.Recipients.Where(r => !recipients.Contains(r.RecipientId)).ToList();
            foreach (var recipient in removedRecipients)
            {
                this.Recipients.Remove(recipient);
            }

            var currentRecipientIds = this.Recipients.Select(r => r.RecipientId);
            var newRecipients = recipients.Where(r => !currentRecipientIds.Contains(r));
            foreach (var recipientId in newRecipients)
            {
                this.Recipients.Add(new MessageRecipient
                {
                    RecipientId = recipientId,
                    IsArchived = false,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void SendMessage(int number)
        {
            this.AssertIsDraft();

            if (this.Recipients.Count == 0)
            {
                throw new InvalidOperationException("Cannot send message that has no recipients");
            }

            this.Status = MessageStatus.Sent;
            this.Number = number;

            var currentDate = DateTime.Now;
            this.SentDate = currentDate;
            this.ModifyDate = currentDate;

            ((IEventEmitter)this).Events.Add(new MessageSentEvent() { MessageId = this.MessageId });
        }

        public void SetRecievedByUser(int userId)
        {
            var recipient = this.Recipients.Single(r => r.RecipientId == userId);

            recipient.RecieveDate = DateTime.Now;
        }

        public bool IsArchivedByUser(int userId)
        {
            var recipient = this.Recipients.Single(r => r.RecipientId == userId);

            return recipient.IsArchived;
        }

        public void SetArchivedByUser(int userId)
        {
            if (this.Status != MessageStatus.Sent)
            {
                throw new InvalidOperationException("Cannot archive message");
            }

            var recipient = this.Recipients.Single(r => r.RecipientId == userId);
            recipient.IsArchived = true;
        }

        public void AddFiles(IList<MessageFile> files)
        {
            this.AssertIsDraft();

            this.MessageFiles = this.MessageFiles.Concat(files).ToList();
        }

        public void UpdateFiles(IList<Tuple<int, Guid, string, string>> files)
        {
            this.AssertIsDraft();

            foreach (var file in files)
            {
                var oldFile = this.MessageFiles.Single(f => f.MessageFileId == file.Item1);
                oldFile.BlobKey = file.Item2;
                oldFile.Name = file.Item3;
                oldFile.Description = file.Item4;
            }
        }

        public void RemoveFiles(IList<int> fileIds)
        {
            this.AssertIsDraft();

            var files = this.MessageFiles
                .Where(f => fileIds.Contains(f.MessageFileId))
                .ToList();

            foreach (var file in files)
            {
                this.MessageFiles.Remove(file);
            }
        }

        private void AssertIsDraft()
        {
            if (this.Status != MessageStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit message that is not in Draft status");
            }
        }
    }
}
