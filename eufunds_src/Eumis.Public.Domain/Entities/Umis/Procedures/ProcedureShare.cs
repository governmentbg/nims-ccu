using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureShare
    {
        public ProcedureShare()
        {
            this.ProcedureBudgetLevel2 = new List<ProcedureBudgetLevel2>();
        }

        internal void SetAttributes(decimal euAmount, decimal bgAmount)
        {
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
        }

        public int ProcedureShareId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public FinanceSource FinanceSource { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsActivated { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureBudgetLevel2> ProcedureBudgetLevel2 { get; set; }
    }

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
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
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
