using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.AnnualAccountReports
{
    public class AnnualAccountReportCertRevalidationCorrection
    {
        public AnnualAccountReportCertRevalidationCorrection()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportCertRevalidationCorrectionMap : EntityTypeConfiguration<AnnualAccountReportCertRevalidationCorrection>
    {
        public AnnualAccountReportCertRevalidationCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.ContractReportRevalidationCertAuthorityCorrectionId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportCertRevalidationCorrections");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ContractReportRevalidationCertAuthorityCorrectionId).HasColumnName("ContractReportRevalidationCertAuthorityCorrectionId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.CertifiedRevalidationCorrections)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
