using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureIndicativeAnnualWorkingProgrammeCompany
    {
        public int ProcedureIndicativeAnnualWorkingProgrammeCompanyId { get; set; }

        public int ProcedureIndicativeAnnualWorkingProgrammeId { get; set; }

        public int CompanyId { get; set; }

        public virtual ProcedureIndicativeAnnualWorkingProgramme ProcedureIndicativeAnnualWorkingProgramme { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureIndicativeAnnualWorkingProgrammeCompanyMap : EntityTypeConfiguration<ProcedureIndicativeAnnualWorkingProgrammeCompany>
    {
        public ProcedureIndicativeAnnualWorkingProgrammeCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureIndicativeAnnualWorkingProgrammeCompanyId);

            // Properties
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeCompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeId)
                .IsRequired();

            this.Property(t => t.CompanyId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureIndicativeAnnualWorkingProgrammeCompanies");
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeCompanyId).HasColumnName("ProcedureIndicativeAnnualWorkingProgrammeCompanyId");
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeId).HasColumnName("ProcedureIndicativeAnnualWorkingProgrammeId");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");

            // Relationships
            this.HasRequired(t => t.ProcedureIndicativeAnnualWorkingProgramme)
                .WithMany(t => t.ProcedureIndicativeAnnualWorkingProgrammeCompanies)
                .HasForeignKey(t => t.ProcedureIndicativeAnnualWorkingProgrammeId)
                .WillCascadeOnDelete();
        }
    }
}
