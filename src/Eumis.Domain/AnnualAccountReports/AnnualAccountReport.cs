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
    public partial class AnnualAccountReport : IAggregateRoot
    {
        public AnnualAccountReport()
        {
            this.AuditDocuments = new List<AnnualAccountReportAuditDocument>();
            this.CertificationDocuments = new List<AnnualAccountReportCertificationDocument>();
            this.CertReports = new List<AnnualAccountReportCertReport>();
            this.FinancialCorrectionCSDs = new List<AnnualAccountReportFinancialCorrectionCSD>();
            this.CertifiedCorrections = new List<AnnualAccountReportCertCorrection>();
            this.CertifiedFinancialCorrectionCSDs = new List<AnnualAccountReportCertFinancialCorrectionCSD>();
            this.CertifiedRevalidationCorrections = new List<AnnualAccountReportCertRevalidationCorrection>();
            this.CertifiedRevalidationFinancialCorrectionCSDs = new List<AnnualAccountReportCertRevalidationFinancialCorrectionCSD>();
            this.Appendices = new List<AnnualAccountReportAppendix>();
            this.Corrections = new List<AnnualAccountReportContractReportCorrection>();
        }

        public AnnualAccountReport(
            int programmeId,
            int orderNum,
            DateTime regDate,
            AnnualAccountReportPeriod accountPeriod)
            : this()
        {
            var currentDate = DateTime.Now;

            this.ProgrammeId = programmeId;
            this.OrderNum = orderNum;
            this.OrderVersionNum = 1;
            this.RegDate = regDate;
            this.Status = AnnualAccountReportStatus.Draft;
            this.AccountPeriod = accountPeriod;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int AnnualAccountReportId { get; set; }

        public int ProgrammeId { get; set; }

        public int OrderNum { get; set; }

        public int OrderVersionNum { get; set; }

        public DateTime RegDate { get; set; }

        public AnnualAccountReportPeriod AccountPeriod { get; set; }

        public AnnualAccountReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ICollection<AnnualAccountReportAuditDocument> AuditDocuments { get; set; }

        public ICollection<AnnualAccountReportCertificationDocument> CertificationDocuments { get; set; }

        public ICollection<AnnualAccountReportCertReport> CertReports { get; set; }

        public ICollection<AnnualAccountReportContractReportCorrection> Corrections { get; set; }

        public ICollection<AnnualAccountReportFinancialCorrectionCSD> FinancialCorrectionCSDs { get; set; }

        public ICollection<AnnualAccountReportCertCorrection> CertifiedCorrections { get; set; }

        public ICollection<AnnualAccountReportCertFinancialCorrectionCSD> CertifiedFinancialCorrectionCSDs { get; set; }

        public ICollection<AnnualAccountReportCertRevalidationCorrection> CertifiedRevalidationCorrections { get; set; }

        public ICollection<AnnualAccountReportCertRevalidationFinancialCorrectionCSD> CertifiedRevalidationFinancialCorrectionCSDs { get; set; }

        public ICollection<AnnualAccountReportAppendix> Appendices { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class AnnualAccountReportMap : EntityTypeConfiguration<AnnualAccountReport>
    {
        public AnnualAccountReportMap()
        {
            // Primary Key
            this.HasKey(t => t.AnnualAccountReportId);

            // Properties
            this.Property(t => t.AnnualAccountReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.OrderVersionNum)
                .IsRequired();

            this.Property(t => t.RegDate)
                .IsRequired();

            this.Property(t => t.AccountPeriod)
               .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AnnualAccountReports");
            this.Property(t => t.AnnualAccountReportId).HasColumnName("AnnualAccountReportId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.AccountPeriod).HasColumnName("AccountPeriod");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.OrderVersionNum).HasColumnName("OrderVersionNum");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.ApprovalDate).HasColumnName("ApprovalDate");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
