using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public partial class SpotCheckRecommendationAscertainment
    {
        public int SpotCheckRecommendationAscertainmentId { get; set; }

        public int SpotCheckAscertainmentId { get; set; }

        public int SpotCheckRecommendationId { get; set; }

        public virtual SpotCheckRecommendation Recommendation { get; set; }
    }

    public class SpotCheckRecommendationAscertainmentMap : EntityTypeConfiguration<SpotCheckRecommendationAscertainment>
    {
        public SpotCheckRecommendationAscertainmentMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckRecommendationAscertainmentId);

            // Properties
            this.Property(t => t.SpotCheckRecommendationAscertainmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckAscertainmentId)
                .IsRequired();
            this.Property(t => t.SpotCheckRecommendationId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckRecommendationAscertainments");
            this.Property(t => t.SpotCheckRecommendationAscertainmentId).HasColumnName("SpotCheckRecommendationAscertainmentId");
            this.Property(t => t.SpotCheckAscertainmentId).HasColumnName("SpotCheckAscertainmentId");
            this.Property(t => t.SpotCheckRecommendationId).HasColumnName("SpotCheckRecommendationId");

            this.HasRequired(t => t.Recommendation)
                .WithMany(t => t.Ascertainments)
                .HasForeignKey(t => t.SpotCheckRecommendationId)
                .WillCascadeOnDelete();
        }
    }
}
