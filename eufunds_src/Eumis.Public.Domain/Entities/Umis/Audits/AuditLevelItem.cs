using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public partial class AuditLevelItem
    {
        public int AuditLevelItemId { get; set; }

        public int AuditId { get; set; }

        public AuditItemLevel Level { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractContractId { get; set; }

        public virtual Audit Audit { get; set; }
    }

    public class AuditLevelItemMap : EntityTypeConfiguration<AuditLevelItem>
    {
        public AuditLevelItemMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditLevelItemId);

            // Properties
            this.Property(t => t.AuditLevelItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AuditId)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AuditLevelItems");
            this.Property(t => t.AuditLevelItemId).HasColumnName("AuditLevelItemId");
            this.Property(t => t.AuditId).HasColumnName("AuditId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");

            this.HasRequired(t => t.Audit)
                .WithMany(t => t.LevelItems)
                .HasForeignKey(t => t.AuditId)
                .WillCascadeOnDelete();
        }
    }
}
