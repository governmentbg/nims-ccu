using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractVersionXml : IAggregateRoot, IEventEmitter
    {
        public static ContractVersionStatus[] FinalStatuses = new ContractVersionStatus[]
        {
            ContractVersionStatus.Active,
            ContractVersionStatus.Archived
        };

        public string Xml { get; private set; }

        public string Hash { get; private set; }

        public int ContractVersionXmlId { get; set; }

        public Guid Gid { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractVersionType VersionType { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public string Name { get; set; }

        public DateTime? ContractDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public string RegNumber { get; set; }

        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public DateTime? StartDate { get; set; }

        public string StartConditions { get; set; }

        public ContractVersionStatus Status { get; set; }

        public int CreatedByUserId { get; set; }

        public string CreateNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        // do not include those amounts anywhere except on creation, since they are used only for Contracts report
        public virtual ICollection<ContractVersionXmlAmount> ContractVersionXmlAmounts { get; set; }

        ICollection<IDomainEvent> IEventEmitter.Events { get; set; }
    }

    public class ContractVersionXmlMap : EntityTypeConfiguration<ContractVersionXml>
    {
        public ContractVersionXmlMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractVersionXmlId);

            // Properties
            this.Property(t => t.ContractVersionXmlId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.VersionType)
                .IsRequired();

            this.Property(t => t.VersionNum)
                .IsRequired();

            this.Property(t => t.VersionSubNum)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreatedByUserId)
                .IsRequired();

            this.Property(t => t.CreateNote)
                .IsRequired();

            this.Property(t => t.RegNumber)
                .HasMaxLength(200)
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
            this.ToTable("ContractVersionXmls");
            this.Property(t => t.ContractVersionXmlId).HasColumnName("ContractVersionXmlId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");

            this.Property(t => t.VersionType).HasColumnName("VersionType");
            this.Property(t => t.VersionNum).HasColumnName("VersionNum");
            this.Property(t => t.VersionSubNum).HasColumnName("VersionSubNum");

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ContractDate).HasColumnName("ContractDate");
            this.Property(t => t.CompletionDate).HasColumnName("CompletionDate");
            this.Property(t => t.TerminationDate).HasColumnName("TerminationDate");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");

            this.Property(t => t.ExecutionStatus).HasColumnName("ExecutionStatus");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.StartConditions).HasColumnName("StartConditions");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            this.Property(t => t.CreateNote).HasColumnName("CreateNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            //RioXmlDocument Mapping
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
