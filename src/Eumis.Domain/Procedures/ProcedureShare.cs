using Eumis.Domain.NonAggregates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public class ProcedureShare
    {
        public ProcedureShare()
        {
            this.ProcedureBudgetLevel2 = new List<ProcedureBudgetLevel2>();
        }

        public int ProcedureShareId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public decimal BgAmount { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsActivated { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureBudgetLevel2> ProcedureBudgetLevel2 { get; set; }

        internal void SetAttributes(decimal bgAmount)
        {
            this.BgAmount = bgAmount;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureShareMap : EntityTypeConfiguration<ProcedureShare>
    {
        public ProcedureShareMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureShareId);

            // Properties
            this.Property(t => t.ProcedureShareId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureShares");
            this.Property(t => t.ProcedureShareId).HasColumnName("ProcedureShareId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.IsPrimary).HasColumnName("IsPrimary");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureShares)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
