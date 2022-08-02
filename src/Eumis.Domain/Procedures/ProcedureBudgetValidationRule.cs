using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eumis.Domain.Procedures.Validation;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureBudgetValidationRule
    {
        public ProcedureBudgetValidationRule()
        {
        }

        public ProcedureBudgetValidationRule(ProcedureProgramme procedureProgramme, string message, string condition, string rule)
        {
            this.ProcedureProgramme = procedureProgramme;
            this.Message = message;
            this.Condition = condition;
            this.Rule = rule;
        }

        public int ProcedureBudgetValidationRuleId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public string Message { get; set; }

        public string ConditionDbString { get; set; }

        public string RuleDbString { get; set; }

        public string Condition
        {
            get
            {
                return ProcedureValidationEngine.Instance.DeserializeFromDatabase(this.ConditionDbString, this.ProcedureProgramme);
            }

            set
            {
                this.ConditionDbString = ProcedureValidationEngine.Instance.SerializeToDatabase(value, this.ProcedureProgramme);
            }
        }

        public string Rule
        {
            get
            {
                return ProcedureValidationEngine.Instance.DeserializeFromDatabase(this.RuleDbString, this.ProcedureProgramme);
            }

            set
            {
                this.RuleDbString = ProcedureValidationEngine.Instance.SerializeToDatabase(value, this.ProcedureProgramme);
            }
        }

        public virtual ProcedureProgramme ProcedureProgramme { get; set; }

        public bool HasConditionSerializableGuid(Guid gid)
        {
            return ProcedureValidationEngine.Instance.HasExpressionSerializableGuid(this.ConditionDbString, gid);
        }

        public bool HasRuleSerializableGuid(Guid gid)
        {
            return ProcedureValidationEngine.Instance.HasExpressionSerializableGuid(this.RuleDbString, gid);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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

            this.Ignore(t => t.Condition);
            this.Ignore(t => t.Rule);
        }
    }
}
