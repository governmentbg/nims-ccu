using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public class CertReportAttachedCertReport
    {
        public CertReportAttachedCertReport()
        {
        }

        public int CertReportId { get; set; }

        public int AttachedCertReportId { get; set; }

        public virtual CertReport CertReport { get; set; }
    }

    public class CertReportAttachedCertReportMap : EntityTypeConfiguration<CertReportAttachedCertReport>
    {
        public CertReportAttachedCertReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CertReportId, t.AttachedCertReportId });

            // Properties
            this.Property(t => t.CertReportId)
                .IsRequired();

            this.Property(t => t.AttachedCertReportId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CertReportAttachedCertReports");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");
            this.Property(t => t.AttachedCertReportId).HasColumnName("AttachedCertReportId");

            this.HasRequired(t => t.CertReport)
                .WithMany(t => t.CertReportAttachedCertReports)
                .HasForeignKey(t => t.CertReportId)
                .WillCascadeOnDelete();
        }
    }
}
