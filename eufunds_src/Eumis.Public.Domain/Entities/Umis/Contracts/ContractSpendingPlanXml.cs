using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractSpendingPlanXml : IAggregateRoot
    {
        public static ContractSpendingPlanStatus[] FinalStatuses = new ContractSpendingPlanStatus[]
        {
            ContractSpendingPlanStatus.Active,
            ContractSpendingPlanStatus.Archived
        };

        public string Xml { get; private set; }

        public string Hash { get; private set; }

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
    }

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
