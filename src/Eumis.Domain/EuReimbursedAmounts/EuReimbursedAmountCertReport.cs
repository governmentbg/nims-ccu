using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public partial class EuReimbursedAmountCertReport
    {
        public int EuReimbursedAmountCertReportId { get; set; }

        public int EuReimbursedAmountId { get; set; }

        public int CertReportId { get; set; }

        public virtual EuReimbursedAmount EuReimbursedAmount { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EuReimbursedAmountCertReportMap : EntityTypeConfiguration<EuReimbursedAmountCertReport>
    {
        public EuReimbursedAmountCertReportMap()
        {
            // Primary Key
            this.HasKey(t => t.EuReimbursedAmountCertReportId);

            // Properties
            this.Property(t => t.EuReimbursedAmountCertReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EuReimbursedAmountId)
                .IsRequired();
            this.Property(t => t.CertReportId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EuReimbursedAmountCertReports");
            this.Property(t => t.EuReimbursedAmountCertReportId).HasColumnName("EuReimbursedAmountCertReportId");
            this.Property(t => t.EuReimbursedAmountId).HasColumnName("EuReimbursedAmountId");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.HasRequired(t => t.EuReimbursedAmount)
                .WithMany(t => t.CertReports)
                .HasForeignKey(t => t.EuReimbursedAmountId)
                .WillCascadeOnDelete();
        }
    }
}
