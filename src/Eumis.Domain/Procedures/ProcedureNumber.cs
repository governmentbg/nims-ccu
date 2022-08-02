using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureNumber
    {
        public ProcedureNumber()
        {
        }

        public int ProcedureId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public int Year { get; set; }

        public int Number { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureNumberMap : EntityTypeConfiguration<ProcedureNumber>
    {
        public ProcedureNumberMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.ProgrammePriorityId });

            this.Property(t => t.Number)
                .IsRequired();

            this.Property(t => t.Year)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureNumbers");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Number).HasColumnName("Number");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureNumbers)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
