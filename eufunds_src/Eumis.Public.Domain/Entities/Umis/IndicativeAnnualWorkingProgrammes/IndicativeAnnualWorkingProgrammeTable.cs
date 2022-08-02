using Eumis.Public.Domain.Entities.Umis.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes
{
    public class IndicativeAnnualWorkingProgrammeTable
    {
        public int IndicativeAnnualWorkingProgrammeTableId { get; set; }

        public int IndicativeAnnualWorkingProgrammeId { get; set; }

        public int ProcedureId { get; set; }

        public ProcedureStatus ProcedureStatus { get; set; }

        public int ProgrammePriorityId { get; set; }

        public int OrderNum { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProcedureDescription { get; set; }

        public string ProcedureDescriptionAlt { get; set; }

        public IndicativeAnnualWorkingProgrammeTypeConducting IndicativeAnnualWorkingProgrammeTypeConducting { get; set; }

        public bool WithPreSelection { get; set; }

        public decimal ProcedureTotalAmount { get; set; }

        public string EligibleActivities { get; set; }

        public string EligibleActivitiesAlt { get; set; }

        public string EligibleCosts { get; set; }

        public string EligibleCostsAlt { get; set; }

        public decimal MaxPercentCoFinancing { get; set; }

        public string MaxPercentCoFinancingInfo { get; set; }

        public string MaxPercentCoFinancingInfoAlt { get; set; }

        public DateTime ListingDate { get; set; }

        public IndicativeAnnualWorkingProgrammeAssistance IsStateAssistance { get; set; }

        public IndicativeAnnualWorkingProgrammeAssistance IsMinimalAssistance { get; set; }

        public decimal ProjectMinAmount { get; set; }

        public string ProjectMinAmountInfo { get; set; }

        public string ProjectMinAmountInfoAlt { get; set; }

        public decimal ProjectMaxAmount { get; set; }

        public string ProjectMaxAmountInfo { get; set; }

        public string ProjectMaxAmountInfoAlt { get; set; }

        public virtual IndicativeAnnualWorkingProgramme IndicativeAnnualWorkingProgramme { get; set; }

        public virtual ICollection<IndicativeAnnualWorkingProgrammeTableCandidate> IndicativeAnnualWorkingProgrammeTableCandidates { get; set; }

        public virtual ICollection<IndicativeAnnualWorkingProgrammeTableProgramme> IndicativeAnnualWorkingProgrammeTableProgrammes { get; set; }

        public virtual ICollection<IndicativeAnnualWorkingProgrammeTableTimeLimit> IndicativeAnnualWorkingProgrammeTableTimeLimits { get; set; }
    }
    public class IndicativeAnnualWorkingProgrammeTableMap : EntityTypeConfiguration<IndicativeAnnualWorkingProgrammeTable>
    {
        public IndicativeAnnualWorkingProgrammeTableMap()
        {
            // Primary Key
            this.HasKey(t => t.IndicativeAnnualWorkingProgrammeTableId);

            // Properties
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IndicativeAnnualWorkingProgrammeId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.ProcedureStatus)
                .IsRequired();

            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.ProcedureName)
                .IsRequired();

            this.Property(t => t.ProcedureNameAlt)
                .IsRequired();

            this.Property(t => t.ProcedureDescription)
                .IsRequired();

            this.Property(t => t.ProcedureDescriptionAlt)
                .IsRequired();

            this.Property(t => t.IndicativeAnnualWorkingProgrammeTypeConducting)
                .IsRequired();

            this.Property(t => t.WithPreSelection)
                .IsRequired();

            this.Property(t => t.ProcedureTotalAmount)
                .IsRequired();

            this.Property(t => t.EligibleActivities)
                .IsRequired();

            this.Property(t => t.EligibleActivitiesAlt)
                .IsRequired();

            this.Property(t => t.EligibleCosts)
                .IsRequired();

            this.Property(t => t.EligibleCostsAlt)
                .IsRequired();

            this.Property(t => t.MaxPercentCoFinancing)
                .IsRequired();

            this.Property(t => t.MaxPercentCoFinancingInfo)
                .IsRequired();

            this.Property(t => t.MaxPercentCoFinancingInfoAlt)
                .IsRequired();

            this.Property(t => t.ListingDate)
                .IsRequired();

            this.Property(t => t.IsStateAssistance)
                .IsRequired();

            this.Property(t => t.IsMinimalAssistance)
                .IsRequired();

            this.Property(t => t.ProjectMinAmount)
                .IsRequired();

            this.Property(t => t.ProjectMinAmountInfo)
                .IsRequired();

            this.Property(t => t.ProjectMinAmountInfoAlt)
                .IsRequired();

            this.Property(t => t.ProjectMaxAmount)
                .IsRequired();

            this.Property(t => t.ProjectMaxAmountInfo)
                .IsRequired();

            this.Property(t => t.ProjectMaxAmountInfoAlt)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IndicativeAnnualWorkingProgrammeTables");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTableId).HasColumnName("IndicativeAnnualWorkingProgrammeTableId");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeId).HasColumnName("IndicativeAnnualWorkingProgrammeId");
            this.Property(t => t.ProcedureStatus).HasColumnName("ProcedureId");
            this.Property(t => t.ProcedureStatus).HasColumnName("ProcedureStatus");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.ProcedureName).HasColumnName("ProcedureName");
            this.Property(t => t.ProcedureNameAlt).HasColumnName("ProcedureNameAlt");
            this.Property(t => t.ProcedureDescription).HasColumnName("ProcedureDescription");
            this.Property(t => t.ProcedureDescriptionAlt).HasColumnName("ProcedureDescriptionAlt");
            this.Property(t => t.IndicativeAnnualWorkingProgrammeTypeConducting).HasColumnName("IndicativeAnnualWorkingProgrammeTypeConducting");
            this.Property(t => t.WithPreSelection).HasColumnName("WithPreSelection");
            this.Property(t => t.ProcedureTotalAmount).HasColumnName("ProcedureTotalAmount");
            this.Property(t => t.EligibleActivities).HasColumnName("EligibleActivities");
            this.Property(t => t.EligibleActivitiesAlt).HasColumnName("EligibleActivitiesAlt");
            this.Property(t => t.EligibleCosts).HasColumnName("EligibleCosts");
            this.Property(t => t.EligibleCostsAlt).HasColumnName("EligibleCostsAlt");
            this.Property(t => t.MaxPercentCoFinancing).HasColumnName("MaxPercentCoFinancing");
            this.Property(t => t.MaxPercentCoFinancingInfo).HasColumnName("MaxPercentCoFinancingInfo");
            this.Property(t => t.MaxPercentCoFinancingInfoAlt).HasColumnName("MaxPercentCoFinancingInfoAlt");
            this.Property(t => t.ListingDate).HasColumnName("ListingDate");
            this.Property(t => t.IsStateAssistance).HasColumnName("IsStateAssistance");
            this.Property(t => t.IsMinimalAssistance).HasColumnName("IsMinimalAssistance");
            this.Property(t => t.ProjectMinAmount).HasColumnName("ProjectMinAmount");
            this.Property(t => t.ProjectMinAmountInfo).HasColumnName("ProjectMinAmountInfo");
            this.Property(t => t.ProjectMinAmountInfoAlt).HasColumnName("ProjectMinAmountInfoAlt");
            this.Property(t => t.ProjectMaxAmount).HasColumnName("ProjectMaxAmount");
            this.Property(t => t.ProjectMaxAmountInfo).HasColumnName("ProjectMaxAmountInfo");
            this.Property(t => t.ProjectMaxAmountInfoAlt).HasColumnName("ProjectMaxAmountInfoAlt");

            // Relationships
            this.HasRequired(t => t.IndicativeAnnualWorkingProgramme)
                .WithMany(t => t.IndicativeAnnualWorkingProgrammeTables)
                .HasForeignKey(t => t.IndicativeAnnualWorkingProgrammeId)
                .WillCascadeOnDelete();
        }
    }
}
