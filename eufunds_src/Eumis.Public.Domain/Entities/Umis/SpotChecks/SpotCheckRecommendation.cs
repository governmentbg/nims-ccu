using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public partial class SpotCheckRecommendation
    {
        public SpotCheckRecommendation()
        {
            this.Ascertainments = new List<SpotCheckRecommendationAscertainment>();
        }

        public int SpotCheckRecommendationId { get; set; }

        public int SpotCheckId { get; set; }

        public int OrderNumber { get; set; }

        public string Recommendation { get; set; }

        public DateTime? Deadline { get; set; }

        public SpotCheckRecommendationStatus? ExecutionStatus { get; set; }

        public DateTime? StatusDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public DateTime? ExecutionProofDate { get; set; }

        public virtual SpotCheck Check { get; set; }

        public ICollection<SpotCheckRecommendationAscertainment> Ascertainments { get; set; }

        public void SetAttributes(
            string recommendation,
            DateTime? deadline,
            SpotCheckRecommendationStatus? executionStatus,
            DateTime? statusDate,
            DateTime? executionDate,
            DateTime? executionProofDate)
        {
            this.Recommendation = recommendation;
            this.Deadline = deadline;
            this.ExecutionStatus = executionStatus;
            this.StatusDate = statusDate;
            this.ExecutionDate = executionDate;
            this.ExecutionProofDate = executionProofDate;
        }
    }

    public class SpotCheckRecommendationMap : EntityTypeConfiguration<SpotCheckRecommendation>
    {
        public SpotCheckRecommendationMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckRecommendationId);

            // Properties
            this.Property(t => t.SpotCheckRecommendationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckId)
                .IsRequired();
            this.Property(t => t.OrderNumber)
                .IsRequired();
            this.Property(t => t.Recommendation)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckRecommendations");
            this.Property(t => t.SpotCheckRecommendationId).HasColumnName("SpotCheckRecommendationId");
            this.Property(t => t.SpotCheckId).HasColumnName("SpotCheckId");
            this.Property(t => t.OrderNumber).HasColumnName("OrderNumber");
            this.Property(t => t.Recommendation).HasColumnName("Recommendation");
            this.Property(t => t.Deadline).HasColumnName("Deadline");
            this.Property(t => t.ExecutionStatus).HasColumnName("ExecutionStatus");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");
            this.Property(t => t.ExecutionDate).HasColumnName("ExecutionDate");
            this.Property(t => t.ExecutionProofDate).HasColumnName("ExecutionProofDate");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Recommendations)
                .HasForeignKey(t => t.SpotCheckId)
                .WillCascadeOnDelete();
        }
    }
}
