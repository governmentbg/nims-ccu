using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractBudgetLevel3Amount
    {
        public int ContractBudgetLevel3AmountId { get; set; }
        public int ContractId { get; set; }
        public int ProcedureBudgetLevel2Id { get; set; }
        public int OrderNum { get; set; }
        public Guid Gid { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public decimal ContractEuAmount { get; set; }
        public decimal ContractBgAmount { get; set; }
        public decimal ContractSelfAmount { get; set; }
        public decimal CurrentEuAmount { get; set; }
        public decimal CurrentBgAmount { get; set; }
        public decimal CurrentSelfAmount { get; set; }

        public int InterventionFieldId { get; set; }
        public int FormOfFinanceId { get; set; }
        public int TerritorialDimensionId { get; set; }
        public int TerritorialDeliveryMechanismId { get; set; }
        public int ThematicObjectiveId { get; set; }
        public int ESFSecondaryThemeId { get; set; }
        public int EconomicDimensionId { get; set; }
        public string NutsCode { get; set; }
        public string NutsName { get; set; }
        public string NutsFullPath { get; set; }

        public virtual Contract Contract { get; set; }
    }

    public class ContractBudgetLevel3AmountMap : EntityTypeConfiguration<ContractBudgetLevel3Amount>
    {
        public ContractBudgetLevel3AmountMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractBudgetLevel3AmountId);

            // Properties
            this.Property(t => t.ContractBudgetLevel3AmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.ProcedureBudgetLevel2Id)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.Code)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.ContractEuAmount)
                .IsRequired();

            this.Property(t => t.ContractBgAmount)
                .IsRequired();

            this.Property(t => t.ContractSelfAmount)
                .IsRequired();

            this.Property(t => t.CurrentEuAmount)
                .IsRequired();

            this.Property(t => t.CurrentBgAmount)
                .IsRequired();

            this.Property(t => t.CurrentSelfAmount)
                .IsRequired();

            this.Property(t => t.InterventionFieldId)
                .IsRequired();
            this.Property(t => t.FormOfFinanceId)
                .IsRequired();
            this.Property(t => t.TerritorialDimensionId)
                .IsRequired();
            this.Property(t => t.TerritorialDeliveryMechanismId)
                .IsRequired();
            this.Property(t => t.ThematicObjectiveId)
                .IsRequired();
            this.Property(t => t.ESFSecondaryThemeId)
                .IsRequired();
            this.Property(t => t.EconomicDimensionId)
                .IsRequired();

            this.Property(t => t.NutsCode)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.NutsName)
                .IsRequired();

            this.Property(t => t.NutsFullPath)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractBudgetLevel3Amounts");
            this.Property(t => t.ContractBudgetLevel3AmountId).HasColumnName("ContractBudgetLevel3AmountId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ProcedureBudgetLevel2Id).HasColumnName("ProcedureBudgetLevel2Id");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.ContractEuAmount).HasColumnName("ContractEuAmount");
            this.Property(t => t.ContractBgAmount).HasColumnName("ContractBgAmount");
            this.Property(t => t.ContractSelfAmount).HasColumnName("ContractSelfAmount");
            this.Property(t => t.CurrentEuAmount).HasColumnName("CurrentEuAmount");
            this.Property(t => t.CurrentBgAmount).HasColumnName("CurrentBgAmount");
            this.Property(t => t.CurrentSelfAmount).HasColumnName("CurrentSelfAmount");

            this.Property(t => t.InterventionFieldId).HasColumnName("InterventionFieldId");
            this.Property(t => t.FormOfFinanceId).HasColumnName("FormOfFinanceId");
            this.Property(t => t.TerritorialDimensionId).HasColumnName("TerritorialDimensionId");
            this.Property(t => t.TerritorialDeliveryMechanismId).HasColumnName("TerritorialDeliveryMechanismId");
            this.Property(t => t.ThematicObjectiveId).HasColumnName("ThematicObjectiveId");
            this.Property(t => t.ESFSecondaryThemeId).HasColumnName("ESFSecondaryThemeId");
            this.Property(t => t.EconomicDimensionId).HasColumnName("EconomicDimensionId");
            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.NutsName).HasColumnName("NutsName");
            this.Property(t => t.NutsFullPath).HasColumnName("NutsFullPath");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractBudgetLevel3Amounts)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
