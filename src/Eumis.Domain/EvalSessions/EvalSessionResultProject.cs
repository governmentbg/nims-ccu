using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Eumis.Domain.Users;
using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.EvalSessions
{
    public class EvalSessionResultProject
    {
        public EvalSessionResultProject()
        {
        }

        public EvalSessionResultProject(Project project)
            : this()
        {
            this.ProjectId = project.ProjectId;
            this.ProjectName = project.Name;
            this.ProjectNameAlt = project.NameAlt;
            this.ProjectRegDate = project.RegDate;
            this.ProjectRegNumber = project.RegNumber;

            this.CompanyName = project.CompanyName;
            this.CompanyNameAlt = project.CompanyNameAlt;
            this.CompanyUin = project.CompanyUin;
            this.CompanyUinType = project.CompanyUinType;

            this.GrantAmount = project.TotalBfpAmount;
            this.SelfAmount = project.CoFinancingAmount;
        }

        public int EvalSessionResultId { get; set; }

        public int EvalSessionResultProjectId { get; set; }

        public int ProjectId { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public DateTime ProjectRegDate { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public UinType CompanyUinType { get; set; }

        public string CompanyUin { get; set; }

        public bool? EvaluationAdminAdmissResult { get; set; }

        public string NonAdmissionReason { get; set; }

        public decimal? GrantAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public bool? StandingPreliminaryResult { get; set; }

        public decimal? StandingPreliminaryPoints { get; set; }

        public bool? StandingTechFinanceResult { get; set; }

        public decimal? StandingTechFinancePoints { get; set; }

        public bool? StandingComplexResult { get; set; }

        public decimal? StandingComplexPoints { get; set; }

        public int? ProjectStandingNumber { get; set; }

        public EvalSessionProjectStandingStatus? ProjectStandingStatus { get; set; }

        public decimal? GrantAmountCorrected { get; set; }

        public decimal? SelfAmountCorrected { get; set; }

        public string Note { get; set; }

        public virtual EvalSessionResult EvalSessionResult { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class EvalSessionAdminAdmissProjectMap : EntityTypeConfiguration<EvalSessionResultProject>
    {
        public EvalSessionAdminAdmissProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.EvalSessionResultProjectId);

            // Properties
            this.Property(t => t.ProjectId)
                .IsRequired();

            this.Property(t => t.ProjectRegNumber)
                .IsRequired();

            this.Property(t => t.ProjectName)
                .IsRequired();

            this.Property(t => t.CompanyName)
                .IsRequired();

            this.Property(t => t.ProjectRegDate)
                .IsRequired();

            this.Property(t => t.CompanyUin)
                .IsRequired();

            this.Property(t => t.CompanyUinType)
                .IsRequired();

            this.Property(t => t.ProjectNameAlt)
                .IsOptional();

            this.Property(t => t.CompanyNameAlt)
                .IsOptional()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("EvalSessionResultProjects");
            this.Property(t => t.EvalSessionResultProjectId).HasColumnName("EvalSessionResultProjectId");
            this.Property(t => t.EvalSessionResultId).HasColumnName("EvalSessionResultId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ProjectRegNumber).HasColumnName("ProjectRegNumber");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.ProjectNameAlt).HasColumnName("ProjectNameAlt");
            this.Property(t => t.ProjectRegDate).HasColumnName("ProjectRegDate");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyNameAlt).HasColumnName("CompanyNameAlt");
            this.Property(t => t.CompanyUinType).HasColumnName("CompanyUinType");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.EvaluationAdminAdmissResult).HasColumnName("EvaluationAdminAdmissResult");
            this.Property(t => t.NonAdmissionReason).HasColumnName("NonAdmissionReason");

            this.Property(t => t.GrantAmount).HasColumnName("GrantAmount");
            this.Property(t => t.SelfAmount).HasColumnName("SelfAmount");
            this.Property(t => t.StandingPreliminaryPoints).HasColumnName("StandingPreliminaryPoints");
            this.Property(t => t.StandingPreliminaryResult).HasColumnName("StandingPreliminaryResult");
            this.Property(t => t.ProjectStandingStatus).HasColumnName("ProjectStandingStatus");
            this.Property(t => t.ProjectStandingNumber).HasColumnName("ProjectStandingNumber");
            this.Property(t => t.StandingTechFinanceResult).HasColumnName("StandingTechFinanceResult");
            this.Property(t => t.StandingTechFinancePoints).HasColumnName("StandingTechFinancePoints");
            this.Property(t => t.StandingComplexPoints).HasColumnName("StandingComplexPoints");
            this.Property(t => t.StandingComplexResult).HasColumnName("StandingComplexResult");
            this.Property(t => t.GrantAmountCorrected).HasColumnName("GrantAmountCorrected");
            this.Property(t => t.SelfAmountCorrected).HasColumnName("SelfAmountCorrected");
            this.Property(t => t.Note).HasColumnName("Note");

            // Relationships
            this.HasRequired(t => t.EvalSessionResult)
                .WithMany(t => t.Projects)
                .HasForeignKey(t => t.EvalSessionResultId)
                .WillCascadeOnDelete();
        }
    }
}
