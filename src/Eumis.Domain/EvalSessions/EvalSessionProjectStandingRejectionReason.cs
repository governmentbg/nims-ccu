using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionProjectStandingRejectionReason
    {
        public EvalSessionProjectStandingRejectionReason()
        {
        }

        public int EvalSessionProjectStandingRejectionReasonId { get; set; }

        public string Name { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
