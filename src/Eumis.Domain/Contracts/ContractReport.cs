using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReport : IAggregateRoot, INotificationEventEmitter
    {
        public static readonly ContractReportStatus[] CreationStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.Unchecked,
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused,
        };

        public static readonly ContractReportStatus[] ConcurrencyCreationStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.Unchecked,
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused,
            ContractReportStatus.Entered,
            ContractReportStatus.SentChecked,
        };

        public static readonly ContractReportStatus[] FinalStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused,
        };

        public static readonly ContractReportStatus[] MonitoringStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.SentChecked,
            ContractReportStatus.Unchecked,
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused,
        };

        private ContractReport()
        {
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
            this.ContractReportAttachedFinancialCorrections = new List<ContractReportAttachedFinancialCorrection>();
        }

        public ContractReport(
            int contractId,
            ContractReportType reportType,
            Source source,
            int orderNum,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractId = contractId;
            this.ReportType = reportType;
            this.OrderNum = orderNum;
            this.Status = ContractReportStatus.Draft;
            this.Source = source;

            this.OtherRegistration = otherRegistration;
            this.StoragePlace = storagePlace;
            this.SubmitDate = submitDate;
            this.SubmitDeadline = submitDeadline;

            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportType ReportType { get; set; }

        public int OrderNum { get; set; }

        public ContractReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        public Source Source { get; set; }

        public string OtherRegistration { get; set; }

        public string StoragePlace { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime? CheckedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }

        public ICollection<ContractReportAttachedFinancialCorrection> ContractReportAttachedFinancialCorrections { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMap : EntityTypeConfiguration<ContractReport>
    {
        public ContractReportMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportId);

            // Properties
            this.Property(t => t.ContractReportId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ReportType)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.Source)
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
            this.ToTable("ContractReports");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ReportType).HasColumnName("ReportType");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.OtherRegistration).HasColumnName("OtherRegistration");
            this.Property(t => t.StoragePlace).HasColumnName("StoragePlace");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");
            this.Property(t => t.SubmitDeadline).HasColumnName("SubmitDeadline");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
