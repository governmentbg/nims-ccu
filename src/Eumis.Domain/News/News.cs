using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain
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
            int createdByUserId)
            : this()
        {
            this.Type = NewsType.Internal;
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

        public News(
            string title,
            string titleAlt,
            string content,
            string contentAlt,
            string author,
            string authorAlt,
            DateTime dateFrom,
            DateTime dateTo,
            IList<NewsFile> files,
            int createdByUserId)
            : this()
        {
            this.Type = NewsType.Portal;
            this.Status = NewsStatus.Draft;
            this.Title = title;
            this.TitleAlt = titleAlt;
            this.Content = content;
            this.ContentAlt = contentAlt;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.EmailNotification = false;
            this.NewsFiles = files;
            this.Author = author;
            this.AuthorAlt = authorAlt;
            this.CreatedByUserId = createdByUserId;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int NewsId { get; set; }

        public NewsType Type { get; set; }

        public NewsStatus Status { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public bool EmailNotification { get; set; }

        public string Author { get; set; }

        public string AuthorAlt { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<NewsFile> NewsFiles { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NewsMap : EntityTypeConfiguration<News>
    {
        public NewsMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsId);

            // Properties
            this.Property(t => t.NewsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TitleAlt)
                .IsOptional()
                .HasMaxLength(200);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.ContentAlt)
                .IsOptional();

            this.Property(t => t.EmailNotification)
                .IsRequired();

            this.Property(t => t.Author)
                .IsOptional()
                .HasMaxLength(200);

            this.Property(t => t.AuthorAlt)
                .IsOptional()
                .HasMaxLength(200);

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
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleAlt).HasColumnName("TitleAlt");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ContentAlt).HasColumnName("ContentAlt");
            this.Property(t => t.EmailNotification).HasColumnName("EmailNotification");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.AuthorAlt).HasColumnName("AuthorAlt");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
