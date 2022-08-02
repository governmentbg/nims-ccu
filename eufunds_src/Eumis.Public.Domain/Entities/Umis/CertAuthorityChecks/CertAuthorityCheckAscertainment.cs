using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public partial class CertAuthorityCheckAscertainment
    {
        public int CertAuthorityCheckAscertainmentId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        public CertAuthorityCheckAscertainmentType Type { get; set; }

        public string Ascertainment { get; set; }

        public CertAuthorityCheckAscertainmentStatus Status { get; set; }

        public string Recommendation { get; set; }

        public DateTime? RecommendationDeadline { get; set; }

        public CertAuthorityCheckAscertainmentExecutionStatus? RecommendationExecutionStatus { get; set; }

        public string CertAuthorityComment { get; set; }

        public string ManagingAuthorityComment { get; set; }

        public virtual CertAuthorityCheck Check { get; set; }

        public void SetAttributes(
            CertAuthorityCheckAscertainmentType type,
            string ascertainment,
            CertAuthorityCheckAscertainmentStatus status,
            string recommendation,
            DateTime? recommendationDeadline,
            CertAuthorityCheckAscertainmentExecutionStatus? recommendationExecutionStatus,
            string certAuthorityComment,
            string managingAuthorityComment)
        {
            this.Type = type;
            this.Ascertainment = ascertainment;
            this.Status = status;
            this.Recommendation = recommendation;
            this.RecommendationDeadline = recommendationDeadline;
            this.RecommendationExecutionStatus = recommendationExecutionStatus;
            this.CertAuthorityComment = certAuthorityComment;
            this.ManagingAuthorityComment = managingAuthorityComment;
        }
    }

    public class CertAuthorityCheckAscertainmentMap : EntityTypeConfiguration<CertAuthorityCheckAscertainment>
    {
        public CertAuthorityCheckAscertainmentMap()
        {
            // Primary Key
            this.HasKey(t => t.CertAuthorityCheckAscertainmentId);

            // Properties
            this.Property(t => t.CertAuthorityCheckAscertainmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CertAuthorityCheckId)
                .IsRequired();

            this.Property(t => t.Ascertainment)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CertAuthorityCheckAscertainments");
            this.Property(t => t.CertAuthorityCheckAscertainmentId).HasColumnName("CertAuthorityCheckAscertainmentId");
            this.Property(t => t.CertAuthorityCheckId).HasColumnName("CertAuthorityCheckId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Ascertainment).HasColumnName("Ascertainment");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Recommendation).HasColumnName("Recommendation");
            this.Property(t => t.RecommendationDeadline).HasColumnName("RecommendationDeadline");
            this.Property(t => t.RecommendationExecutionStatus).HasColumnName("RecommendationExecutionStatus");
            this.Property(t => t.CertAuthorityComment).HasColumnName("CertAuthorityComment");
            this.Property(t => t.ManagingAuthorityComment).HasColumnName("ManagingAuthorityComment");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Ascertainments)
                .HasForeignKey(t => t.CertAuthorityCheckId)
                .WillCascadeOnDelete();
        }
    }
}
