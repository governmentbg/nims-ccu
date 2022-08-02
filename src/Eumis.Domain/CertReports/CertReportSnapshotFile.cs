using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertReports
{
    public partial class CertReportSnapshotFile
    {
        public CertReportSnapshotFile()
        {
        }

        public int CertReportSnapshotFileId { get; set; }

        public int CertReportSnapshotId { get; set; }

        public Guid BlobKey { get; set; }

        public virtual CertReportSnapshot CertReportSnapshot { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CertReportSnapshotFileMap : EntityTypeConfiguration<CertReportSnapshotFile>
    {
        public CertReportSnapshotFileMap()
        {
            // Primary Key
            this.HasKey(t => t.CertReportSnapshotFileId);

            // Properties
            this.Property(t => t.CertReportSnapshotFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CertReportSnapshotId)
                .IsRequired();

            this.Property(t => t.BlobKey)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CertReportSnapshotFiles");
            this.Property(t => t.CertReportSnapshotFileId).HasColumnName("CertReportSnapshotFileId");
            this.Property(t => t.CertReportSnapshotId).HasColumnName("CertReportSnapshotId");
            this.Property(t => t.BlobKey).HasColumnName("BlobKey");

            this.HasRequired(t => t.CertReportSnapshot)
                .WithMany(t => t.CertReportSnapshotFiles)
                .HasForeignKey(t => t.CertReportSnapshotId)
                .WillCascadeOnDelete();
        }
    }
}
