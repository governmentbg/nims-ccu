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
    public class AnnualAccountReportFinancialCorrectionCSD
    {
        public AnnualAccountReportFinancialCorrectionCSD()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int ContractReportFinancialCorrectionCSDId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportFinancialCorrectionCSDMap : EntityTypeConfiguration<AnnualAccountReportFinancialCorrectionCSD>
    {
        public AnnualAccountReportFinancialCorrectionCSDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.ContractReportFinancialCorrectionCSDId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialCorrectionCSDId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportFinancialCorrectionCSDs");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ContractReportFinancialCorrectionCSDId).HasColumnName("ContractReportFinancialCorrectionCSDId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.FinancialCorrectionCSDs)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
