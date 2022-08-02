using Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public partial class ProcedureIndicativeAnnualWorkingProgramme : IAggregateRoot
    {
        public int ProcedureIndicativeAnnualWorkingProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public IndicativeAnnualWorkingProgrammeYear Year { get; set; }

        public string EligibleActivities { get; set; }

        public string EligibleActivitiesAlt { get; set; }

        public string EligibleCosts { get; set; }

        public string EligibleCostsAlt { get; set; }

        public decimal MaxPercentCoFinancing { get; set; }

        public string MaxPercentCoFinancingInfo { get; set; }

        public string MaxPercentCoFinancingInfoAlt { get; set; }

        public IndicativeAnnualWorkingProgrammeAssistance IsStateAssistance { get; set; }

        public IndicativeAnnualWorkingProgrammeAssistance IsMinimalAssistance { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureIndicativeAnnualWorkingProgrammeCandidate> ProcedureIndicativeAnnualWorkingProgrammeCandidates { get; set; }

        public virtual ICollection<ProcedureIndicativeAnnualWorkingProgrammeCompany> ProcedureIndicativeAnnualWorkingProgrammeCompanies { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureIndicativeAnnualWorkingProgrammeMap : EntityTypeConfiguration<ProcedureIndicativeAnnualWorkingProgramme>
    {
        public ProcedureIndicativeAnnualWorkingProgrammeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureIndicativeAnnualWorkingProgrammeId);

            // Properties
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.Year)
                .IsRequired();

            this.Property(t => t.EligibleActivities)
                .IsRequired();

            this.Property(t => t.EligibleCosts)
                .IsRequired();

            this.Property(t => t.IsStateAssistance)
                .IsRequired();

            this.Property(t => t.IsMinimalAssistance)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProcedureIndicativeAnnualWorkingProgrammes");
            this.Property(t => t.ProcedureIndicativeAnnualWorkingProgrammeId).HasColumnName("ProcedureIndicativeAnnualWorkingProgrammeId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.EligibleActivities).HasColumnName("EligibleActivities");
            this.Property(t => t.EligibleActivitiesAlt).HasColumnName("EligibleActivitiesAlt");
            this.Property(t => t.EligibleCosts).HasColumnName("EligibleCosts");
            this.Property(t => t.EligibleCostsAlt).HasColumnName("EligibleCostsAlt");
            this.Property(t => t.MaxPercentCoFinancing).HasColumnName("MaxPercentCoFinancing");
            this.Property(t => t.MaxPercentCoFinancingInfo).HasColumnName("MaxPercentCoFinancingInfo");
            this.Property(t => t.MaxPercentCoFinancingInfoAlt).HasColumnName("MaxPercentCoFinancingInfoAlt");
            this.Property(t => t.IsStateAssistance).HasColumnName("IsStateAssistance");
            this.Property(t => t.IsMinimalAssistance).HasColumnName("IsMinimalAssistance");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureIndicativeAnnualWorkingProgrammes)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
