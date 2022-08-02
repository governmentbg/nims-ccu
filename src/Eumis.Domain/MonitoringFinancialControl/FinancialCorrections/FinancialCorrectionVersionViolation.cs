using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.FinancialCorrections
{
    public partial class FinancialCorrectionVersionViolation
    {
        public FinancialCorrectionVersionViolation()
        {
        }

        public int FinancialCorrectionVersionId { get; set; }

        public int OtherViolationId { get; set; }

        public virtual FinancialCorrectionVersion FinancialCorrectionVersion { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class FinancialCorrectionVersionViolationMap : EntityTypeConfiguration<FinancialCorrectionVersionViolation>
    {
        public FinancialCorrectionVersionViolationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.FinancialCorrectionVersionId, t.OtherViolationId });

            this.Property(t => t.FinancialCorrectionVersionId)
                .IsRequired();

            this.Property(t => t.OtherViolationId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("FinancialCorrectionVersionViolations");
            this.Property(t => t.FinancialCorrectionVersionId).HasColumnName("FinancialCorrectionVersionId");
            this.Property(t => t.OtherViolationId).HasColumnName("OtherViolationId");

            // Relationships
            this.HasRequired(t => t.FinancialCorrectionVersion)
                .WithMany(t => t.FinancialCorrectionVersionViolations)
                .HasForeignKey(t => t.FinancialCorrectionVersionId);
        }
    }
}
