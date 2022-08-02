using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public class AuditAscertainment
    {
        public AuditAscertainment()
        {
            this.Items = new List<AuditAscertainmentItem>();
        }

        public int AuditAscertainmentId { get; set; }

        public int AuditId { get; set; }

        public string Ascertainment { get; set; }

        public string Recommendation { get; set; }

        public bool? RecommendationsFulfilled { get; set; }

        public bool IsFinancial { get; set; }

        public decimal? FinancialSum { get; set; }

        public virtual Audit Audit { get; set; }

        public ICollection<AuditAscertainmentItem> Items { get; set; }

        internal void SetAttributes(
            string ascertainment,
            string recommendation,
            bool? recommendationsFulfilled,
            bool isFinancial,
            decimal? financialSum)
        {
            this.Ascertainment = ascertainment;
            this.Recommendation = recommendation;
            this.RecommendationsFulfilled = recommendationsFulfilled;
            this.IsFinancial = isFinancial;
            this.FinancialSum = financialSum;
        }
    }

    public class AuditAscertainmentMap : EntityTypeConfiguration<AuditAscertainment>
    {
        public AuditAscertainmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditAscertainmentId);

            // Properties
            this.Property(t => t.AuditAscertainmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AuditId)
                .IsRequired();
            this.Property(t => t.Ascertainment)
                .IsRequired();
            this.Property(t => t.IsFinancial)
                .IsRequired();


            // Table & Column Mappings
            this.ToTable("AuditAscertainments");
            this.Property(t => t.AuditAscertainmentId).HasColumnName("AuditAscertainmentId");
            this.Property(t => t.AuditId).HasColumnName("AuditId");
            this.Property(t => t.Ascertainment).HasColumnName("Ascertainment");
            this.Property(t => t.Recommendation).HasColumnName("Recommendation");
            this.Property(t => t.RecommendationsFulfilled).HasColumnName("RecommendationsFulfilled");
            this.Property(t => t.IsFinancial).HasColumnName("IsFinancial");
            this.Property(t => t.FinancialSum).HasColumnName("FinancialSum");

            this.HasRequired(t => t.Audit)
                .WithMany(t => t.Ascertainments)
                .HasForeignKey(t => t.AuditId)
                .WillCascadeOnDelete();
        }
    }
}
