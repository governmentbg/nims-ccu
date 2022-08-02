using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureProgramme
    {
        public ProcedureProgramme()
        {
            this.ProcedureBudgetLevel1 = new List<ProcedureBudgetLevel1>();
            this.ProcedureBudgetValidationRules = new List<ProcedureBudgetValidationRule>();
        }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureBudgetLevel1> ProcedureBudgetLevel1 { get; set; }

        public virtual ICollection<ProcedureBudgetValidationRule> ProcedureBudgetValidationRules { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureProgrammeMap : EntityTypeConfiguration<ProcedureProgramme>
    {
        public ProcedureProgrammeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.ProgrammeId });

            // Table & Column Mappings
            this.ToTable("ProcedureProgrammes");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureProgrammes)
                .HasForeignKey(t => t.ProcedureId);
        }
    }
}
