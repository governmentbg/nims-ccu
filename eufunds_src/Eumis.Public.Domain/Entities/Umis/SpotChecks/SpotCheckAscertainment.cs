using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public partial class SpotCheckAscertainment
    {
        public SpotCheckAscertainment()
        {
            this.Items = new List<SpotCheckAscertainmentItem>();
        }

        public int SpotCheckAscertainmentId { get; set; }

        public int SpotCheckId { get; set; }

        public int OrderNumber { get; set; }

        public SpotCheckAscertainmentType Type { get; set; }

        public string Ascertainment { get; set; }

        public SpotCheckAscertainmentStatus Status { get; set; }

        public string CheckSubjectComment { get; set; }

        public string ManagingAuthorityComment { get; set; }

        public virtual SpotCheck Check { get; set; }

        public ICollection<SpotCheckAscertainmentItem> Items { get; set; }

        public void SetAttributes(
            SpotCheckAscertainmentType type,
            string ascertainment,
            SpotCheckAscertainmentStatus status,
            string checkSubjectComment,
            string managingAuthorityComment)
        {
            this.Type = type;
            this.Ascertainment = ascertainment;
            this.Status = status;
            this.CheckSubjectComment = checkSubjectComment;
            this.ManagingAuthorityComment = managingAuthorityComment;
        }
    }

    public class SpotCheckAscertainmentMap : EntityTypeConfiguration<SpotCheckAscertainment>
    {
        public SpotCheckAscertainmentMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckAscertainmentId);

            // Properties
            this.Property(t => t.SpotCheckAscertainmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckId)
                .IsRequired();

            this.Property(t => t.OrderNumber)
                .IsRequired();

            this.Property(t => t.Ascertainment)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckAscertainments");
            this.Property(t => t.SpotCheckAscertainmentId).HasColumnName("SpotCheckAscertainmentId");
            this.Property(t => t.SpotCheckId).HasColumnName("SpotCheckId");
            this.Property(t => t.OrderNumber).HasColumnName("OrderNumber");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Ascertainment).HasColumnName("Ascertainment");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CheckSubjectComment).HasColumnName("CheckSubjectComment");
            this.Property(t => t.ManagingAuthorityComment).HasColumnName("ManagingAuthorityComment");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Ascertainments)
                .HasForeignKey(t => t.SpotCheckId)
                .WillCascadeOnDelete();
        }
    }
}
