using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureBudgetLevel2
    {
        public ProcedureBudgetLevel2()
        {
            this.Gid = Guid.NewGuid();
            this.ProcedureBudgetLevel3 = new List<ProcedureBudgetLevel3>();
        }

        public ProcedureBudgetLevel2(
            int procedureShareId,
            int procedureBudgetLevel1Id,
            string name,
            int orderNum,
            ProcedureBudgetLevel2AidMode aidMode,
            bool isEligibleCost,
            bool isStandardTablesExpense,
            bool isOneTimeExpense,
            bool isFlatRateExpense,
            bool isLandExpense,
            bool isEuApprovedStandardTablesExpense,
            bool IsEuApprovedOneTimeExpense)
            :this()
        {
            this.ProcedureShareId = procedureShareId;
            this.ProcedureBudgetLevel1Id = procedureBudgetLevel1Id;
            this.Name = name;
            this.OrderNum = orderNum;
            this.AidMode = aidMode;
            this.IsEligibleCost = isEligibleCost;
            this.IsStandardTablesExpense = isStandardTablesExpense;
            this.IsOneTimeExpense = isOneTimeExpense;
            this.IsFlatRateExpense = isFlatRateExpense;
            this.IsLandExpense = isLandExpense;
            this.IsEuApprovedStandardTablesExpense = isEuApprovedStandardTablesExpense;
            this.IsEuApprovedOneTimeExpense = IsEuApprovedOneTimeExpense;

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureBudgetLevel2Id { get; set; }
        public int ProcedureShareId { get; set; }
        public int ProcedureBudgetLevel1Id { get; set; }
        public string Name { get; set; }
        public Guid Gid { get; set; }
        public int OrderNum { get; set; }
        public ProcedureBudgetLevel2AidMode AidMode { get; set; }
        public bool IsEligibleCost { get; set; }
        public bool IsStandardTablesExpense { get; set; }
        public bool IsOneTimeExpense { get; set; }
        public bool IsFlatRateExpense { get; set; }
        public bool IsLandExpense { get; set; }
        public bool IsEuApprovedStandardTablesExpense { get; set; }
        public bool IsEuApprovedOneTimeExpense { get; set; }
        public bool IsActivated { get; set; }
        public bool IsActive { get; set; }

        public virtual ProcedureBudgetLevel1 ProcedureBudgetLevel1 { get; set; }
        public virtual ProcedureShare ProcedureShare { get; set; }

        public virtual ICollection<ProcedureBudgetLevel3> ProcedureBudgetLevel3 { get; set; }
    }

    public class ProcedureBudgetLevel2Map : EntityTypeConfiguration<ProcedureBudgetLevel2>
    {
        public ProcedureBudgetLevel2Map()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureBudgetLevel2Id);

            //Properties
            this.Property(t => t.ProcedureBudgetLevel2Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureBudgetLevel2");
            this.Property(t => t.ProcedureBudgetLevel2Id).HasColumnName("ProcedureBudgetLevel2Id");
            this.Property(t => t.ProcedureShareId).HasColumnName("ProcedureShareId");
            this.Property(t => t.ProcedureBudgetLevel1Id).HasColumnName("ProcedureBudgetLevel1Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.AidMode).HasColumnName("AidMode");
            this.Property(t => t.IsEligibleCost).HasColumnName("IsEligibleCost");
            this.Property(t => t.IsStandardTablesExpense).HasColumnName("IsStandardTablesExpense");
            this.Property(t => t.IsOneTimeExpense).HasColumnName("IsOneTimeExpense");
            this.Property(t => t.IsFlatRateExpense).HasColumnName("IsFlatRateExpense");
            this.Property(t => t.IsLandExpense).HasColumnName("IsLandExpense");
            this.Property(t => t.IsEuApprovedStandardTablesExpense).HasColumnName("IsEuApprovedStandardTablesExpense");
            this.Property(t => t.IsEuApprovedOneTimeExpense).HasColumnName("IsEuApprovedOneTimeExpense");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.ProcedureBudgetLevel1)
                .WithMany(t => t.ProcedureBudgetLevel2)
                .HasForeignKey(d => d.ProcedureBudgetLevel1Id)
                .WillCascadeOnDelete();
            this.HasRequired(t => t.ProcedureShare)
                .WithMany(t => t.ProcedureBudgetLevel2)
                .HasForeignKey(d => d.ProcedureShareId);
        }
    }
}
