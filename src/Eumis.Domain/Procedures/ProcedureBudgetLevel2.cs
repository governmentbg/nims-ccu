using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
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
            string nameAlt,
            int orderNum,
            ProcedureBudgetLevel2AidMode aidMode)
            : this()
        {
            this.ProcedureShareId = procedureShareId;
            this.ProcedureBudgetLevel1Id = procedureBudgetLevel1Id;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.OrderNum = orderNum;
            this.AidMode = aidMode;

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureBudgetLevel2Id { get; set; }

        public int ProcedureShareId { get; set; }

        public int ProcedureBudgetLevel1Id { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public ProcedureBudgetLevel2AidMode AidMode { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual ProcedureBudgetLevel1 ProcedureBudgetLevel1 { get; set; }

        public virtual ProcedureShare ProcedureShare { get; set; }

        public virtual ICollection<ProcedureBudgetLevel3> ProcedureBudgetLevel3 { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureBudgetLevel2Map : EntityTypeConfiguration<ProcedureBudgetLevel2>
    {
        public ProcedureBudgetLevel2Map()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureBudgetLevel2Id);

            // Properties
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
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.AidMode).HasColumnName("AidMode");
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
