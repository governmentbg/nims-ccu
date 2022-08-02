using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.CertReports
{
    public partial class CertReport : IAggregateRoot, INotificationEventEmitter
    {
        public static readonly CertReportStatus[] FinalStatuses = new CertReportStatus[]
        {
            CertReportStatus.Approved,
            CertReportStatus.PartialyApproved,
            CertReportStatus.Unapproved,
        };

        public CertReport()
        {
            this.CertReportDocuments = new List<CertReportDocument>();
            this.CertReportCertificationDocuments = new List<CertReportCertificationDocument>();
            this.CertReportAttachedCertReports = new List<CertReportAttachedCertReport>();

            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public CertReport(
            int programmeId,
            int orderNum,
            DateTime regDate,
            DateTime dateFrom,
            DateTime dateTo,
            CertReportType type,
            string reportNumber)
            : this()
        {
            var currentDate = DateTime.Now;

            this.ProgrammeId = programmeId;
            this.OrderNum = orderNum;
            this.OrderVersionNum = 1;
            this.RegDate = regDate;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.Status = CertReportStatus.Draft;
            this.Type = type;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
            this.CertReportNumber = reportNumber;
        }

        public int CertReportId { get; set; }

        public int ProgrammeId { get; set; }

        public int OrderNum { get; set; }

        public int OrderVersionNum { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public CertReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        public CertReportType Type { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? CertReportOriginId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CertReportNumber { get; set; }

        public byte[] Version { get; set; }

        public ICollection<CertReportDocument> CertReportDocuments { get; set; }

        public ICollection<CertReportCertificationDocument> CertReportCertificationDocuments { get; set; }

        public ICollection<CertReportAttachedCertReport> CertReportAttachedCertReports { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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

            this.Property(t => t.OrderVersionNum)
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
            this.Property(t => t.OrderVersionNum).HasColumnName("OrderVersionNum");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ApprovalDate).HasColumnName("ApprovalDate");

            this.Property(t => t.CertReportOriginId).HasColumnName("CertReportOriginId");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.CertReportNumber).HasColumnName("CertReportNumber");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
