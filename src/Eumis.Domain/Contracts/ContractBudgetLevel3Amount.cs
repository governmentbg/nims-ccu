using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts
{
    public class ContractBudgetLevel3Amount
    {
        public ContractBudgetLevel3Amount()
        {
        }

        public ContractBudgetLevel3Amount(
            int procedureBudgetLevel2Id,
            int orderNum,
            Guid gid,
            bool isActive,
            string code,
            string name,
            decimal contractEuAmount,
            decimal contractBgAmount,
            decimal contractSelfAmount,
            decimal currentEuAmount,
            decimal currentBgAmount,
            decimal currentSelfAmount,
            string nutsCode,
            string nutsName,
            string nutsFullPath,
            string nutsFullPathName,
            int? directionId,
            int? subDirectionId)
        {
            this.ProcedureBudgetLevel2Id = procedureBudgetLevel2Id;
            this.OrderNum = orderNum;
            this.Gid = gid;
            this.IsActive = isActive;

            this.Code = code;
            this.Name = name;
            this.ContractEuAmount = contractEuAmount;
            this.ContractBgAmount = contractBgAmount;
            this.ContractSelfAmount = contractSelfAmount;
            this.CurrentEuAmount = currentEuAmount;
            this.CurrentBgAmount = currentBgAmount;
            this.CurrentSelfAmount = currentSelfAmount;

            this.NutsCode = nutsCode;
            this.NutsName = nutsName;
            this.NutsFullPath = nutsFullPath;
            this.NutsFullPathName = nutsFullPathName;

            this.DirectionId = directionId;
            this.SubDirectionId = subDirectionId;
        }

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

        public string NutsCode { get; set; }

        public string NutsName { get; set; }

        public string NutsFullPath { get; set; }

        public string NutsFullPathName { get; set; }

        public int? DirectionId { get; set; }

        public int? SubDirectionId { get; set; }

        public virtual Contract Contract { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
            this.Property(t => t.NutsCode)
                .IsRequired();
            this.Property(t => t.NutsName)
                .IsRequired();
            this.Property(t => t.NutsFullPath)
                .IsRequired();
            this.Property(t => t.NutsFullPathName)
                .IsRequired();
            this.Property(t => t.DirectionId)
                .IsOptional();
            this.Property(t => t.SubDirectionId)
                .IsOptional();

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

            this.Property(t => t.NutsCode).HasColumnName("NutsCode");
            this.Property(t => t.NutsName).HasColumnName("NutsName");
            this.Property(t => t.NutsFullPath).HasColumnName("NutsFullPath");
            this.Property(t => t.NutsFullPathName).HasColumnName("NutsFullPathName");

            this.Property(t => t.DirectionId).HasColumnName("DirectionId");
            this.Property(t => t.SubDirectionId).HasColumnName("SubDirectionId");

            this.HasRequired(t => t.Contract)
                .WithMany(t => t.ContractBudgetLevel3Amounts)
                .HasForeignKey(t => t.ContractId)
                .WillCascadeOnDelete();
        }
    }
}
