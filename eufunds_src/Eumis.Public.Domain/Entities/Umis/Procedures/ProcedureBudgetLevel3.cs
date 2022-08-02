using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureBudgetLevel3
    {
        public ProcedureBudgetLevel3()
        {
            this.Gid = Guid.NewGuid();
        }

        public ProcedureBudgetLevel3(string note, int orderNum)
            :this()
        {
            this.Note = note;
            this.OrderNum = orderNum;
        }

        public int ProcedureBudgetLevel3Id { get; set; }
        public int ProcedureBudgetLevel2Id { get; set; }
        public Guid Gid { get; set; }
        public string Note { get; set; }
        public int OrderNum { get; set; }

        public virtual ProcedureBudgetLevel2 ProcedureBudgetLevel2 { get; set; }
    }

    public class ProcedureBudgetLevel3Map : EntityTypeConfiguration<ProcedureBudgetLevel3>
    {
        public ProcedureBudgetLevel3Map()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureBudgetLevel3Id);

            //Properties
            this.Property(t => t.ProcedureBudgetLevel3Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Note)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureBudgetLevel3");
            this.Property(t => t.ProcedureBudgetLevel3Id).HasColumnName("ProcedureBudgetLevel3Id");
            this.Property(t => t.ProcedureBudgetLevel2Id).HasColumnName("ProcedureBudgetLevel2Id");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");

            // Relationships
            this.HasRequired(t => t.ProcedureBudgetLevel2)
                .WithMany(t => t.ProcedureBudgetLevel3)
                .HasForeignKey(d => d.ProcedureBudgetLevel2Id)
                .WillCascadeOnDelete();
        }
    }
}
