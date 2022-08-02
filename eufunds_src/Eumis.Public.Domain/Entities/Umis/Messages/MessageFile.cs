using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Messages
{
    public class MessageFile
    {
        public int MessageFileId { get; set; }

        public int MessageId { get; set; }

        public Guid BlobKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Message Message { get; set; }
    }

    public class MessageFileMap : EntityTypeConfiguration<MessageFile>
    {
        public MessageFileMap()
        {
            // Primary Key
            this.HasKey(t => t.MessageFileId);

            // Properties
            this.Property(t => t.MessageFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.MessageId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("MessageFiles");
            this.Property(t => t.MessageFileId).HasColumnName("MessageFileId");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.Message)
                .WithMany(t => t.MessageFiles)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete();
        }
    }
}
