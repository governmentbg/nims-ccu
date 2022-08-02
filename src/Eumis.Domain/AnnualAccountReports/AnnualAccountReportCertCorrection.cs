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
    public class AnnualAccountReportCertCorrection
    {
        public AnnualAccountReportCertCorrection()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int ContractReportCertAuthorityCorrectionId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportCertCorrectionMap : EntityTypeConfiguration<AnnualAccountReportCertCorrection>
    {
        public AnnualAccountReportCertCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.ContractReportCertAuthorityCorrectionId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.ContractReportCertAuthorityCorrectionId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportCertCorrections");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ContractReportCertAuthorityCorrectionId).HasColumnName("ContractReportCertAuthorityCorrectionId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.CertifiedCorrections)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
