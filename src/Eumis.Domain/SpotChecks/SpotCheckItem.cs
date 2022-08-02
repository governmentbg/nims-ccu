using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.SpotChecks
{
    public class SpotCheckItem
    {
        public int SpotCheckItemId { get; set; }

        public int SpotCheckId { get; set; }

        public SpotCheckItemLevel Level { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractContractId { get; set; }

        public virtual SpotCheck Check { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class SpotCheckItemMap : EntityTypeConfiguration<SpotCheckItem>
    {
        public SpotCheckItemMap()
        {
            // Primary Key
            this.HasKey(t => t.SpotCheckItemId);

            // Properties
            this.Property(t => t.SpotCheckItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SpotCheckId)
                .IsRequired();
            this.Property(t => t.Level)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SpotCheckItems");
            this.Property(t => t.SpotCheckItemId).HasColumnName("SpotCheckItemId");
            this.Property(t => t.SpotCheckId).HasColumnName("SpotCheckId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");

            this.HasRequired(t => t.Check)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.SpotCheckId)
                .WillCascadeOnDelete();
        }
    }
}
