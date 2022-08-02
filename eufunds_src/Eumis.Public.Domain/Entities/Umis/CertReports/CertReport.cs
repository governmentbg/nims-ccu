using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public partial class CertReport : IAggregateRoot
    {
        public int CertReportId { get; set; }
        public int ProgrammeId { get; set; }
        public int OrderNum { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public CertReportStatus Status { get; set; }
        public CertReportType Type { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public ICollection<CertReportDocument> CertReportDocuments { get; set; }
        public ICollection<CertReportAttachedCertReport> CertReportAttachedCertReports { get; set; }
    }

    public class CertReportMap : EntityTypeConfiguration<CertReport>
    {
        public CertReportMap()
        {
            // Primary Key
            this.HasKey(t => t.CertReportId);

            // Properties
            this.Property(t => t.CertReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProgrammeId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.RegDate)
                .IsRequired();

            this.Property(t => t.DateFrom)
                .IsRequired();

            this.Property(t => t.DateTo)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Type)
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
            this.ToTable("CertReports");
            this.Property(t => t.CertReportId).HasColumnName("CertReportId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Type).HasColumnName("Type");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
