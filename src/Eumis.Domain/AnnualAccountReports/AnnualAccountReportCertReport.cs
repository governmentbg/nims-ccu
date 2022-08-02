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
    public class AnnualAccountReportCertReport
    {
        public AnnualAccountReportCertReport()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int CertReportId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportCertReportMap : EntityTypeConfiguration<AnnualAccountReportCertReport>
    {
        public AnnualAccountReportCertReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.CertReportId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.CertReportId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportCertReports");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.CertReports)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
