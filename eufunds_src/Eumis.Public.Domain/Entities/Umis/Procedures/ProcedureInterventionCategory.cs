using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureInterventionCategory
    {
        public ProcedureInterventionCategory()
        {
            this.IsActivated = false;
            this.IsActive = true;
        }

        public int ProcedureId { get; set; }

        public int InterventionCategoryId { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    public class ProcedureInterventionCategoryMap : EntityTypeConfiguration<ProcedureInterventionCategory>
    {
        public ProcedureInterventionCategoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProcedureId, t.InterventionCategoryId });

            // Table & Column Mappings
            this.ToTable("ProcedureInterventionCategories");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.InterventionCategoryId).HasColumnName("InterventionCategoryId");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureInterventionCategories)
                .HasForeignKey(t => t.ProcedureId);
        }
    }
}
