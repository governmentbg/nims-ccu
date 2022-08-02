using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionProjectStandingRejectionReason
    {
        public int EvalSessionProjectStandingRejectionReasonId { get; set; }

        public string Name { get; set; }
    }

    public class EvalSessionProjectStandingRejectionReasonMap : EntityTypeConfiguration<EvalSessionProjectStandingRejectionReason>
    {
        public EvalSessionProjectStandingRejectionReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionProjectStandingRejectionReasonId);

            // Properties
            this.Property(t => t.EvalSessionProjectStandingRejectionReasonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionProjectStandingRejectionReasons");
            this.Property(t => t.EvalSessionProjectStandingRejectionReasonId).HasColumnName("EvalSessionProjectStandingRejectionReasonId");
        }
    }
}
