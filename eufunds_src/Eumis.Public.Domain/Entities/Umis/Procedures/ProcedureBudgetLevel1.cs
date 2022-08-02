using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureBudgetLevel1
    {
        public ProcedureBudgetLevel1()
        {
            this.Gid = Guid.NewGuid();
            this.ProcedureBudgetLevel2 = new List<ProcedureBudgetLevel2>();
        }

        public ProcedureBudgetLevel1(int programmeId, int expenseTypeId, int orderNum)
            :this()
        {
            this.ProgrammeId = programmeId;
            this.ExpenseTypeId = expenseTypeId;
            this.OrderNum = orderNum;

            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureBudgetLevel1Id { get; set; }
        public int ProcedureId { get; set; }
        public int ProgrammeId { get; set; }
        public int ExpenseTypeId { get; set; }
        public Guid Gid { get; set; }
        public int OrderNum { get; set; }
        public bool IsActivated { get; set; }
        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }
        public virtual ProcedureProgramme ProcedureProgramme { get; set; }

        public virtual ICollection<ProcedureBudgetLevel2> ProcedureBudgetLevel2 { get; set; }
    }

    public class ProcedureBudgetLevel1Map : EntityTypeConfiguration<ProcedureBudgetLevel1>
    {
        public ProcedureBudgetLevel1Map()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureBudgetLevel1Id);

            //Properties
            this.Property(t => t.ProcedureBudgetLevel1Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ProcedureBudgetLevel1");
            this.Property(t => t.ProcedureBudgetLevel1Id).HasColumnName("ProcedureBudgetLevel1Id");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ExpenseTypeId).HasColumnName("ExpenseTypeId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.ProcedureProgramme)
                .WithMany(t => t.ProcedureBudgetLevel1)
                .HasForeignKey(d => new { d.ProcedureId, d.ProgrammeId })
                .WillCascadeOnDelete();
            this.HasRequired(t => t.Procedure)
                .WithMany()
                .HasForeignKey(d => d.ProcedureId);
        }
    }
}
