using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.News
{
    public partial class News : IAggregateRoot
    {
        private News()
        {
            this.NewsFiles = new List<NewsFile>();
        }

        public News(
            string title,
            string content,
            bool emailNotification,
            IList<NewsFile> files,
            int createdByUserId) : this()
        {
            this.Status = NewsStatus.Draft;
            this.Title = title;
            this.Content = content;
            this.EmailNotification = emailNotification;
            this.NewsFiles = files;
            this.CreatedByUserId = createdByUserId;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int NewsId { get; set; }

        public NewsStatus Status { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool EmailNotification { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<NewsFile> NewsFiles { get; set; }
    }

    public class NewsMap : EntityTypeConfiguration<News>
    {
        public NewsMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsId);

            // Properties
            this.Property(t => t.NewsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.EmailNotification)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
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
            this.ToTable("News");
            this.Property(t => t.NewsId).HasColumnName("NewsId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.EmailNotification).HasColumnName("EmailNotification");

            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
