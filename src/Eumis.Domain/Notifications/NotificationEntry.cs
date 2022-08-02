using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Notifications
{
    public partial class NotificationEntry : IAggregateRoot
    {
        public NotificationEntry()
        {
            this.Status = NotificationEntryStatus.Pending;
            this.FailedAttempts = 0;
        }

        public NotificationEntry(DispatchResolver resolver, NotificationEvent notificationEvent)
            : this()
        {
            this.DispatcherId = resolver.GetDispatcherId();
            this.DispatcherPath = resolver.GetDispatcherPath();
            this.NotificationEvent = notificationEvent;

            var dateNow = DateTime.Now;
            this.CreateDate = dateNow;
            this.ModifyDate = dateNow;
        }

        public int NotificationEntryId { get; set; }

        public int NotificationEventId { get; set; }

        public NotificationEntryStatus Status { get; set; }

        public int DispatcherId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public string DispatcherPath { get; set; }

        public int FailedAttempts { get; set; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual NotificationEvent NotificationEvent { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class NotificationEntryMap : EntityTypeConfiguration<NotificationEntry>
    {
        public NotificationEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationEntryId);

            // Properties
            this.Property(t => t.NotificationEntryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.DispatcherId)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.NotificationEventId)
                .IsRequired();

            this.Property(t => t.FailedAttempts)
                .IsRequired();

            this.Property(t => t.DispatcherPath)
                .IsOptional()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("NotificationEntries");
            this.Property(t => t.NotificationEntryId).HasColumnName("NotificationEntryId");
            this.Property(t => t.NotificationEventId).HasColumnName("NotificationEventId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DispatcherId).HasColumnName("DispatcherId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.DispatcherPath).HasColumnName("DispatcherPath");
            this.Property(t => t.FailedAttempts).HasColumnName("FailedAttempts");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.NotificationEvent)
                .WithMany()
                .HasForeignKey(t => t.NotificationEventId);
        }
    }
}
