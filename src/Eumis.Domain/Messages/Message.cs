using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Domain.Core;

namespace Eumis.Domain.Messages
{
    public partial class Message : IAggregateRoot, IEventEmitter
    {
        private Message()
        {
            this.Recipients = new List<MessageRecipient>();
            this.MessageFiles = new List<MessageFile>();

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public Message(
            string title,
            string content,
            int senderId,
            IList<int> userIds,
            IList<MessageFile> files)
        {
            this.Status = MessageStatus.Draft;
            this.Title = title;
            this.Content = content;
            this.SenderId = senderId;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            this.MessageFiles = files;
            this.Recipients = userIds.Select(id => new MessageRecipient
            {
                RecipientId = id,
                IsArchived = false,
            }).ToList();

            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public int MessageId { get; set; }

        public MessageStatus Status { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int SenderId { get; set; }

        public DateTime? SentDate { get; set; }

        public int? Number { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<MessageRecipient> Recipients { get; set; }

        public ICollection<MessageFile> MessageFiles { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.MessageId);

            // Properties
            this.Property(t => t.MessageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.SenderId)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Messages");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.SenderId).HasColumnName("SenderId");
            this.Property(t => t.SentDate).HasColumnName("SentDate");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
