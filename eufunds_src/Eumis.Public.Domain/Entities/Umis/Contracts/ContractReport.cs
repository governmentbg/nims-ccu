using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReport : IAggregateRoot, IEventEmitter
    {
        public static ContractReportStatus[] CreationStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.Unchecked,
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused
        };

        public static ContractReportStatus[] FinalStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused
        };

        public static ContractReportStatus[] MonitoringStatuses = new ContractReportStatus[]
        {
            ContractReportStatus.SentChecked,
            ContractReportStatus.Unchecked,
            ContractReportStatus.Accepted,
            ContractReportStatus.Refused
        };

        private ContractReport()
        {
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
            this.ContractReportAttachedFinancialCorrections = new List<ContractReportAttachedFinancialCorrection>();
        }

        public ContractReport(
            int contractId,
            ContractReportType reportType,
            Source source,
            int orderNum,
            DateTime regDate,
            string otherRegistration,
            string storagePlace,
            DateTime? submitDate,
            DateTime? submitDeadline,
            DateTime dateFrom,
            DateTime dateTo)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractId = contractId;
            this.ReportType = reportType;
            this.OrderNum = orderNum;
            this.Status = ContractReportStatus.Draft;
            this.Source = source;

            this.RegDate = regDate;
            this.OtherRegistration = otherRegistration;
            this.StoragePlace = storagePlace;
            this.SubmitDate = submitDate;
            this.SubmitDeadline = submitDeadline;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;

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

        public DateTime RegDate { get; set; }
        public string OtherRegistration { get; set; }
        public string StoragePlace { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? SubmitDeadline { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime? CheckedDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
        public ICollection<ContractReportAttachedFinancialCorrection> ContractReportAttachedFinancialCorrections { get; set; }
    }

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

            this.Property(t => t.RegDate)
                .IsRequired();

            this.Property(t => t.DateFrom)
                .IsRequired();

            this.Property(t => t.DateTo)
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
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.OtherRegistration).HasColumnName("OtherRegistration");
            this.Property(t => t.StoragePlace).HasColumnName("StoragePlace");
            this.Property(t => t.SubmitDate).HasColumnName("SubmitDate");
            this.Property(t => t.SubmitDeadline).HasColumnName("SubmitDeadline");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
