using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureInvestmentPriority
    {
        public ProcedureInvestmentPriority()
        {
        }

        public int ProcedureId { get; set; }

        public int InvestmentPriorityId { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    public class ProcedureInvestmentPriorityMap : EntityTypeConfiguration<ProcedureInvestmentPriority>
    {
        public ProcedureInvestmentPriorityMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.InvestmentPriorityId });

            // Table & Column Mappings
            this.ToTable("ProcedureInvestmentPriorities");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.InvestmentPriorityId).HasColumnName("InvestmentPriorityId");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureInvestmentPriorities)
                .HasForeignKey(t => t.ProcedureId);
        }
    }
}
