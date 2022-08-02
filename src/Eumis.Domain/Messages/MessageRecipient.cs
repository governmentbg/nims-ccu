using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Messages
{
    public class MessageRecipient
    {
        public int MessageRecipientId { get; set; }

        public int MessageId { get; set; }

        public int RecipientId { get; set; }

        public DateTime? RecieveDate { get; set; }

        public bool IsArchived { get; set; }

        public virtual Message Message { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class MessageRecipientMap : EntityTypeConfiguration<MessageRecipient>
    {
        public MessageRecipientMap()
        {
            // Primary Key
            this.HasKey(t => t.MessageRecipientId);

            // Properties
            this.Property(t => t.MessageRecipientId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.MessageId)
                .IsRequired();

            this.Property(t => t.RecipientId)
                .IsRequired();

            this.Property(t => t.IsArchived)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("MessageRecipients");
            this.Property(t => t.MessageRecipientId).HasColumnName("MessageRecipientId");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.RecipientId).HasColumnName("RecipientId");
            this.Property(t => t.RecieveDate).HasColumnName("RecieveDate");
            this.Property(t => t.IsArchived).HasColumnName("IsArchived");

            this.HasRequired(t => t.Message)
                .WithMany(t => t.Recipients)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete();
        }
    }
}
