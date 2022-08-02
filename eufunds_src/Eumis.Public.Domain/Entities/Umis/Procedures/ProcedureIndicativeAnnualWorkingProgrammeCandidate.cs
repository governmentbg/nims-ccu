using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureIndicativeAnnualWorkingProgrammeCandidate
    {
        public int ProcedureIndicativeAnnualWorkingProgrammeCandidateId { get; set; }

        public int ProcedureIndicativeAnnualWorkingProgrammeId { get; set; }

        public int? CompanyTypeId { get; set; }

        public int? CompanyLegalTypeId { get; set; }

        public string Info { get; set; }

        public string InfoAlt { get; set; }

        public virtual ProcedureIndicativeAnnualWorkingProgramme ProcedureIndicativeAnnualWorkingProgramme { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureIndicativeAnnualWorkingProgrammeCandidateMap : EntityTypeConfiguration<ProcedureIndicativeAnnualWorkingProgrammeCandidate>
    {
        public ProcedureIndicativeAnnualWorkingProgrammeCandidateMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureIndicativeAnnualWorkingProgrammeCandidateId);

            // Properties
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeCandidateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureIndicativeAnnualWorkingProgrammeCandidates");
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeCandidateId).HasColumnName("ProcedureIndicativeAnnualWorkingProgrammeCandidateId");
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeId).HasColumnName("ProcedureIndicativeAnnualWorkingProgrammeId");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.InfoAlt).HasColumnName("InfoAlt");

            // Relationships
            this.HasRequired(t => t.ProcedureIndicativeAnnualWorkingProgramme)
                .WithMany(t => t.ProcedureIndicativeAnnualWorkingProgrammeCandidates)
                .HasForeignKey(t => t.ProcedureIndicativeAnnualWorkingProgrammeId)
                .WillCascadeOnDelete();
        }
    }
}
