using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public class SpotCheckPlanItem
    {
        public int SpotCheckPlanItemId { get; set; }

        public int SpotCheckPlanId { get; set; }

        public SpotCheckItemLevel Level { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractContractId { get; set; }

        public virtual SpotCheckPlan Plan { get; set; }
    }

    public class SpotCheckPlanItemMap : EntityTypeConfiguration<SpotCheckPlanItem>
    {
        public SpotCheckPlanItemMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckPlanItemId);

            // Properties
            this.Property(t => t.SpotCheckPlanItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckPlanId)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckPlanItems");
            this.Property(t => t.SpotCheckPlanItemId).HasColumnName("SpotCheckPlanItemId");
            this.Property(t => t.SpotCheckPlanId).HasColumnName("SpotCheckPlanId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");

            this.HasRequired(t => t.Plan)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.SpotCheckPlanId)
                .WillCascadeOnDelete();
        }
    }
}
