using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public partial class ProcedureBudgetValidationRule
    {
        public int ProcedureBudgetValidationRuleId { get; set; }
        public int ProcedureId { get; set; }
        public int ProgrammeId { get; set; }
        public string Message { get; set; }
        public string ConditionDbString { get; set; }
        public string RuleDbString { get; set; }

        public virtual ProcedureProgramme ProcedureProgramme { get; set; }
    }

    public class ProcedureBudgetValidationRuleMap : EntityTypeConfiguration<ProcedureBudgetValidationRule>
    {
        public ProcedureBudgetValidationRuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureBudgetValidationRuleId);

            // Properties
            this.Property(t => t.ProcedureBudgetValidationRuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Message)
                .IsRequired();

            this.Property(t => t.RuleDbString)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureBudgetValidationRules");
            this.Property(t => t.ProcedureBudgetValidationRuleId).HasColumnName("ProcedureBudgetValidationRuleId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.ConditionDbString).HasColumnName("Condition");
            this.Property(t => t.RuleDbString).HasColumnName("Rule");

            // Relationships
            this.HasRequired(t => t.ProcedureProgramme)
                .WithMany(t => t.ProcedureBudgetValidationRules)
                .HasForeignKey(d => new { d.ProcedureId, d.ProgrammeId })
                .WillCascadeOnDelete();
        }
    }
}
