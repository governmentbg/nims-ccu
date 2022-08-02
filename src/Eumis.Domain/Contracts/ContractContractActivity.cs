using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public class ContractContractActivity
    {
        public ContractContractActivity()
        {
        }

        public int ContractContractActivityId { get; set; }

        public int ContractContractId { get; set; }

        public int? ContractActivityId { get; set; }

        public int ContractBudgetLevel3AmountId { get; set; }

        public virtual ContractContract ContractContract { get; set; }

        public virtual ContractActivity ContractActivity { get; set; }

        public virtual ContractBudgetLevel3Amount ContractBudgetLevel3Amount { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractContractActivityMap : EntityTypeConfiguration<ContractContractActivity>
    {
        public ContractContractActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractContractActivityId);

            // Properties
            this.Property(t => t.ContractContractActivityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ContractContractActivities");
            this.Property(t => t.ContractContractActivityId).HasColumnName("ContractContractActivityId");
            this.Property(t => t.ContractContractId).HasColumnName("ContractContractId");
            this.Property(t => t.ContractActivityId).HasColumnName("ContractActivityId");
            this.Property(t => t.ContractBudgetLevel3AmountId).HasColumnName("ContractBudgetLevel3AmountId");

            this.HasRequired(t => t.ContractContract)
                .WithMany(t => t.ContractContractActivities)
                .HasForeignKey(t => t.ContractContractId)
                .WillCascadeOnDelete();

            this.HasOptional(t => t.ContractActivity)
                .WithMany()
                .HasForeignKey(t => t.ContractActivityId);

            this.HasRequired(t => t.ContractBudgetLevel3Amount)
                .WithMany()
                .HasForeignKey(t => t.ContractBudgetLevel3AmountId);
        }
    }
}
