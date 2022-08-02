using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.AnnualAccountReports
{
    public class AnnualAccountReportCertRevalidationFinancialCorrectionCSD
    {
        public AnnualAccountReportCertRevalidationFinancialCorrectionCSD()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportCertRevalidationFinancialCorrectionCSDMap : EntityTypeConfiguration<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>
    {
        public AnnualAccountReportCertRevalidationFinancialCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportCertRevalidationFinancialCorrectionCSDs");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId).HasColumnName("ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.CertifiedRevalidationFinancialCorrectionCSDs)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
