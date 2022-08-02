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
    public class AnnualAccountReportContractReportCorrection
    {
        public AnnualAccountReportContractReportCorrection()
        {
        }

        public int AnnualAccountReportId { get; set; }

        public int ContractReportCorrectionId { get; set; }

        public virtual AnnualAccountReport AnnualAccountReport { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportContractReportCorrectionMap : EntityTypeConfiguration<AnnualAccountReportContractReportCorrection>
    {
        public AnnualAccountReportContractReportCorrectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AnnualAccountReportId, t.ContractReportCorrectionId });

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .IsRequired();

            this.Property(t => t.ContractReportCorrectionId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReportContractReportCorrections");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ContractReportCorrectionId).HasColumnName("ContractReportCorrectionId");

            this.HasRequired<AnnualAccountReport>(t => t.AnnualAccountReport)
                .WithMany(t => t.Corrections)
                .HasForeignKey(t => t.AnnualAccountReportId)
                .WillCascadeOnDelete();
        }
    }
}
