using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureEvalTable
    {
        public ProcedureEvalTable()
        {
            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureEvalTableId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public ProcedureEvalTableType Type { get; set; }

        public ProcedureEvalType EvalType { get; set; }

        public ProcedureEvalTableStatus Status { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }

        internal void SetAttributes(string name, ProcedureEvalTableType type)
        {
            this.Name = name;
            this.Type = type;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureEvalTableMap : EntityTypeConfiguration<ProcedureEvalTable>
    {
        public ProcedureEvalTableMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureEvalTableId);

            // Properties
            this.Property(t => t.ProcedureEvalTableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureEvalTables");
            this.Property(t => t.ProcedureEvalTableId).HasColumnName("ProcedureEvalTableId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.EvalType).HasColumnName("EvalType");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureEvalTables)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
