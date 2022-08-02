using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes
{
    public class IndicativeAnnualWorkingProgrammeTableCandidate
    {
        public int IndicativeAnnualWorkingProgrammeTableCandidateId { get; set; }

        public int IndicativeAnnualWorkingProgrammeTableId { get; set; }

        public int? CompanyTypeId { get; set; }

        public int? CompanyLegalTypeId { get; set; }

        public string Info { get; set; }

        public string InfoAlt { get; set; }

        public virtual IndicativeAnnualWorkingProgrammeTable IndicativeAnnualWorkingProgrammeTable { get; set; }
    }

    public class IndicativeAnnualWorkingProgrammeTableCandidateMap : EntityTypeConfiguration<IndicativeAnnualWorkingProgrammeTableCandidate>
    {
        public IndicativeAnnualWorkingProgrammeTableCandidateMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicativeAnnualWorkingProgrammeTableCandidateId);

            // Properties
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableCandidateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IndicativeAnnualWorkingProgrammeTableCandidates");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableCandidateId).HasColumnName("IndicativeAnnualWorkingProgrammeTableCandidateId");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId).HasColumnName("IndicativeAnnualWorkingProgrammeTableId");
            this.Property(t => t.CompanyTypeId).HasColumnName("CompanyTypeId");
            this.Property(t => t.CompanyLegalTypeId).HasColumnName("CompanyLegalTypeId");
            this.Property(t => t.Info).HasColumnName("Info");
            this.Property(t => t.InfoAlt).HasColumnName("InfoAlt");

            // Relationships
            this.HasRequired(t => t.IndicativeAnnualWorkingProgrammeTable)
                .WithMany(t => t.IndicativeAnnualWorkingProgrammeTableCandidates)
                .HasForeignKey(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .WillCascadeOnDelete();
        }
    }
}
