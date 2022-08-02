using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain
{
    public class NewsFile
    {
        public int NewsFileId { get; set; }

        public int NewsId { get; set; }

        public Guid BlobKey { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual News News { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NewsFileMap : EntityTypeConfiguration<NewsFile>
    {
        public NewsFileMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsFileId);

            // Properties
            this.Property(t => t.NewsFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NewsId)
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
            this.ToTable("NewsFiles");
            this.Property(t => t.NewsFileId).HasColumnName("NewsFileId");
            this.Property(t => t.NewsId).HasColumnName("NewsId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");

            this.HasRequired(t => t.News)
                .WithMany(t => t.NewsFiles)
                .HasForeignKey(t => t.NewsId)
                .WillCascadeOnDelete();
        }
    }
}
