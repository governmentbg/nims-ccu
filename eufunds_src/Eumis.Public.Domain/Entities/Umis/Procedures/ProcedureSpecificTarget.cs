using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureSpecificTarget
    {
        public ProcedureSpecificTarget()
        {
        }

        public int ProcedureId { get; set; }

        public int SpecificTargetId { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    public class ProcedureSpecificTargetMap : EntityTypeConfiguration<ProcedureSpecificTarget>
    {
        public ProcedureSpecificTargetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.SpecificTargetId });

            // Table & Column Mappings
            this.ToTable("ProcedureSpecificTargets");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.SpecificTargetId).HasColumnName("SpecificTargetId");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureSpecificTargets)
                .HasForeignKey(t => t.ProcedureId);
        }
    }
}
