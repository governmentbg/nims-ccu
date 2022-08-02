using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public partial class ContractReportMicro : IAggregateRoot, INotificationEventEmitter, IEventEmitter
    {
        private ContractReportMicro()
        {
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
            ((IEventEmitter)this).Events = new List<IDomainEvent>();
        }

        public ContractReportMicro(
            int contractId,
            int contractReportId,
            int versionNum,
            int versionSubNum,
            ContractReportMicroType type,
            Source source)
            : this()
        {
            this.ContractId = contractId;
            this.ContractReportId = contractReportId;
            this.Gid = Guid.NewGuid();
            this.Type = type;

            this.VersionNum = versionNum;
            this.VersionSubNum = versionSubNum;
            this.Status = ContractReportMicroStatus.Draft;
            this.Source = source;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public ContractReportMicro(
            int versionSubNum,
            ContractReportMicro prevMicro)
            : this()
        {
            this.ContractId = prevMicro.ContractId;
            this.ContractReportId = prevMicro.ContractReportId;
            this.Gid = Guid.NewGuid();
            this.Type = prevMicro.Type;

            this.VersionNum = prevMicro.VersionNum;
            this.VersionSubNum = versionSubNum;
            this.Status = ContractReportMicroStatus.Draft;
            this.Source = prevMicro.Source;

            this.CreateDate = this.ModifyDate = DateTime.Now;
        }

        public int ContractReportMicroId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportMicroType Type { get; set; }

        public Guid? ExcelBlobKey { get; set; }

        public string ExcelName { get; set; }

        public bool IsFromExternalSystem { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public Source Source { get; set; }

        public ContractReportMicroStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public int? SenderContractRegistrationId { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMicroMap : EntityTypeConfiguration<ContractReportMicro>
    {
        public ContractReportMicroMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicroId);

            // Properties
            this.Property(t => t.ContractReportMicroId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.ContractReportId)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();
            this.Property(t => t.Type)
                .IsRequired();
            this.Property(t => t.ExcelName)
                .HasMaxLength(200)
                .IsOptional();
            this.Property(t => t.VersionNum)
                .IsRequired();
            this.Property(t => t.VersionSubNum)
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
            this.ToTable("ContractReportMicros");
            this.Property(t => t.ContractReportMicroId).HasColumnName("ContractReportMicroId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ExcelBlobKey).HasColumnName("ExcelBlobKey");
            this.Property(t => t.ExcelName).HasColumnName("ExcelName");
            this.Property(t => t.IsFromExternalSystem).HasColumnName("IsFromExternalSystem");

            this.Property(t => t.VersionNum).HasColumnName("VersionNum");
            this.Property(t => t.VersionSubNum).HasColumnName("VersionSubNum");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusNote).HasColumnName("StatusNote");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.SenderContractRegistrationId).HasColumnName("SenderContractRegistrationId");
        }
    }
}
