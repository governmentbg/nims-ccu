using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public partial class PublicDiscussion : IAggregateRoot
    {

        public int PublicDiscussionId { get; set; }

        public int ProcedureId { get; set; }

        public PublicDiscussionStatus Status { get; set; }

        public PublicDiscussionCommentsSectionStatus CommentsSectionStatus { get; set; }

        public int? FirstPublicatedByUserId { get; set; }

        public DateTime? FirstPublicationDate { get; set; }

        public int? LastPublicatedByUserId { get; set; }

        public DateTime? LastPublicationDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class PublicDiscussionMap : EntityTypeConfiguration<PublicDiscussion>
    {
        public PublicDiscussionMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicDiscussionId);

            // Properties
            this.Property(t => t.PublicDiscussionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Status)
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
            this.ToTable("PublicDiscussions");
            this.Property(t => t.PublicDiscussionId).HasColumnName("PublicDiscussionId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CommentsSectionStatus).HasColumnName("CommentsSectionStatus");
            this.Property(t => t.FirstPublicatedByUserId).HasColumnName("FirstPublicatedByUserId");
            this.Property(t => t.FirstPublicationDate).HasColumnName("FirstPublicationDate");
            this.Property(t => t.LastPublicatedByUserId).HasColumnName("LastPublicatedByUserId");
            this.Property(t => t.LastPublicationDate).HasColumnName("LastPublicationDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
