using Eumis.Domain.CertReports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.AnnualAccountReports
{
    public class AnnualAccountReportCertFinancialCorrectionCSD
    {
        public AnnualAccountReportCertFinancialCorrectionCSD()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int ContractReportCertAuthorityFinancialCorrectionCSDId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportCertFinancialCorrectionCSDMap : EntityTypeConfiguration<AnnualAccountReportCertFinancialCorrectionCSD>
    {
        public AnnualAccountReportCertFinancialCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.ContractReportCertAuthorityFinancialCorrectionCSDId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.ContractReportCertAuthorityFinancialCorrectionCSDId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportCertFinancialCorrectionCSDs");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ContractReportCertAuthorityFinancialCorrectionCSDId).HasColumnName("ContractReportCertAuthorityFinancialCorrectionCSDId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.CertifiedFinancialCorrectionCSDs)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
