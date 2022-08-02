using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Core;

namespace Eumis.Domain.Contracts
{
    public partial class ContractSpendingPlanXml : RioXmlDocument<Rio.SpendingPlan>, IAggregateRoot, INotificationEventEmitter
    {
        public static readonly ContractSpendingPlanStatus[] FinalStatuses = new ContractSpendingPlanStatus[]
        {
            ContractSpendingPlanStatus.Active,
            ContractSpendingPlanStatus.Archived,
        };

        private ContractSpendingPlanXml()
        {
            ((INotificationEventEmitter)this).NotificationEvents = new List<INotificationEvent>();
        }

        public ContractSpendingPlanXml(
            int contractId,
            int orderNum,
            int contractVersionXmlId,
            Source source,
            int createdByUserId,
            string createNote,
            string xml)
            : this()
        {
            var currentDate = DateTime.Now;

            this.Gid = Guid.NewGuid();
            this.ContractId = contractId;
            this.ContractVersionXmlId = contractVersionXmlId;
            this.Source = source;
            this.OrderNum = orderNum;
            this.Status = ContractSpendingPlanStatus.Draft;
            this.CreatedByUserId = createdByUserId;
            this.CreateNote = createNote;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;

            base.SetXml(xml);
        }

        public int ContractSpendingPlanXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ContractId { get; set; }

        public int ContractVersionXmlId { get; set; }

        public Source Source { get; set; }

        public int OrderNum { get; set; }

        public ContractSpendingPlanStatus Status { get; set; }

        public int CreatedByUserId { get; set; }

        public string CreateNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        ICollection<INotificationEvent> INotificationEventEmitter.NotificationEvents { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractSpendingPlanXmlMap : EntityTypeConfiguration<ContractSpendingPlanXml>
    {
        public ContractSpendingPlanXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractSpendingPlanXmlId);

            // Properties
            this.Property(t => t.ContractSpendingPlanXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.ContractVersionXmlId)
                .IsRequired();

            this.Property(t => t.Source)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateNote)
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
            this.ToTable("ContractSpendingPlanXmls");
            this.Property(t => t.ContractSpendingPlanXmlId).HasColumnName("ContractSpendingPlanXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractVersionXmlId).HasColumnName("ContractVersionXmlId");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateNote).HasColumnName("CreateNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // RioXmlDocument Mapping
            this.Property(t => t.Xml)
                .IsRequired();

            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.Hash).HasColumnName("Hash");
        }
    }
}
