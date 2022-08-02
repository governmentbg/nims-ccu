using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureNumber
    {
        public ProcedureNumber()
        {
        }

        public int ProcedureId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public int Number { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    public class ProcedureNumberMap : EntityTypeConfiguration<ProcedureNumber>
    {
        public ProcedureNumberMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.ProgrammePriorityId });

            this.Property(t => t.Number)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureNumbers");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.Number).HasColumnName("Number");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureNumbers)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
