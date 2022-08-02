using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.Projects;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public class EvalSessionReportProject
    {
        public EvalSessionReportProject()
        {
            this.Partners = new List<EvalSessionReportProjectPartner>();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionReportId { get; set; }

        public int ProjectId { get; set; }

        public int? ProjectVersionId { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime RecieveDate { get; set; }

        public ProjectRecieveType RecieveType { get; set; }

        public string Name { get; set; }

        public int? Duration { get; set; }

        public int? ProjectKidCodeId { get; set; }

        public string ProjectPlace { get; set; }

        public decimal? GrandAmount { get; set; }

        public decimal? CoFinancingAmount { get; set; }

        public EvalSessionReportProjectStandingStatus StandingStatus { get; set; }

        public int? StandingNum { get; set; }

        public decimal? StandingGrandAmount { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public int CompanySizeTypeId { get; set; }

        public int? CompanyKidCodeId { get; set; }

        public string RegEmail { get; set; }

        public string Correspondence { get; set; }

        public bool HasAdminAdmiss { get; set; }

        public EvalSessionEvaluationResult? AdminAdmissResult { get; set; }

        public decimal? AdminAdmissPoints { get; set; }

        public bool HasTechFinance { get; set; }

        public EvalSessionEvaluationResult? TechFinanceResult { get; set; }

        public decimal? TechFinancePoints { get; set; }

        public bool HasComplex { get; set; }

        public EvalSessionEvaluationResult? ComplexResult { get; set; }

        public decimal? ComplexPoints { get; set; }

        public virtual EvalSessionReport Report { get; set; }

        public virtual KidCode ProjectKidCode { get; set; }

        public virtual CompanySizeType CompanySizeType { get; set; }

        public virtual KidCode CompanyKidCode { get; set; }

        public ICollection<EvalSessionReportProjectPartner> Partners { get; set; }

        public void AddPartner(
            string partnerUin,
            string partnerName,
            int partnerLegalTypeId,
            string partnerAddress,
            string partnerRepresentative,
            decimal? partnerFinancialContribution)
        {
            var partner = new EvalSessionReportProjectPartner
            {
                EvalSessionId = this.EvalSessionId,
                EvalSessionReportId = this.EvalSessionReportId,
                ProjectId = this.ProjectId,
                PartnerUin = partnerUin,
                PartnerName = partnerName,
                PartnerLegalTypeId = partnerLegalTypeId,
                PartnerAddress = partnerAddress,
                PartnerRepresentative = partnerRepresentative,
                PartnerFinancialContribution = partnerFinancialContribution
            };

            this.Partners.Add(partner);
        }
    }

    public class EvalSessionReportProjectMap : EntityTypeConfiguration<EvalSessionReportProject>
    {
        public EvalSessionReportProjectMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EvalSessionId, t.EvalSessionReportId, t.ProjectId });

            // Properties
            this.Property(t => t.RegNumber)
                .IsRequired()
                .HasMaxLength(200);
            this.Property(t => t.RegDate)
                .IsRequired();
            this.Property(t => t.RecieveDate)
                .IsRequired();
            this.Property(t => t.RecieveType)
                .IsRequired();
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.StandingStatus)
                .IsRequired();
            this.Property(t => t.CompanyUin)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.CompanyName)
                .HasMaxLength(200)
                .IsRequired();
            this.Property(t => t.CompanySizeTypeId)
                .IsRequired();
            this.Property(t => t.HasAdminAdmiss)
                .IsRequired();
            this.Property(t => t.HasTechFinance)
                .IsRequired();
            this.Property(t => t.HasComplex)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EvalSessionReportProjects");
            this.Property(t => t.EvalSessionId).HasColumnName("EvalSessionId");
            this.Property(t => t.EvalSessionReportId).HasColumnName("EvalSessionReportId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ProjectVersionId).HasColumnName("ProjectVersionId");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.RecieveDate).HasColumnName("RecieveDate");
            this.Property(t => t.RecieveType).HasColumnName("RecieveType");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.ProjectKidCodeId).HasColumnName("ProjectKidCodeId");
            this.Property(t => t.ProjectPlace).HasColumnName("ProjectPlace");
            this.Property(t => t.GrandAmount).HasColumnName("GrandAmount");
            this.Property(t => t.CoFinancingAmount).HasColumnName("CoFinancingAmount");
            this.Property(t => t.StandingStatus).HasColumnName("StandingStatus");
            this.Property(t => t.StandingNum).HasColumnName("StandingNum");
            this.Property(t => t.StandingGrandAmount).HasColumnName("StandingGrandAmount");
            this.Property(t => t.CompanyUin).HasColumnName("CompanyUin");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanySizeTypeId).HasColumnName("CompanySizeTypeId");
            this.Property(t => t.CompanyKidCodeId).HasColumnName("CompanyKidCodeId");
            this.Property(t => t.RegEmail).HasColumnName("RegEmail");
            this.Property(t => t.Correspondence).HasColumnName("Correspondence");
            this.Property(t => t.HasAdminAdmiss).HasColumnName("HasAdminAdmiss");
            this.Property(t => t.AdminAdmissResult).HasColumnName("AdminAdmissResult");
            this.Property(t => t.AdminAdmissPoints).HasColumnName("AdminAdmissPoints");
            this.Property(t => t.HasTechFinance).HasColumnName("HasTechFinance");
            this.Property(t => t.TechFinanceResult).HasColumnName("TechFinanceResult");
            this.Property(t => t.TechFinancePoints).HasColumnName("TechFinancePoints");
            this.Property(t => t.HasComplex).HasColumnName("HasComplex");
            this.Property(t => t.ComplexResult).HasColumnName("ComplexResult");
            this.Property(t => t.ComplexPoints).HasColumnName("ComplexPoints");

            //Relationships
            this.HasRequired(t => t.Report)
                .WithMany(t => t.Projects)
                .HasForeignKey(t => t.EvalSessionReportId);
            this.HasOptional(t => t.ProjectKidCode)
                .WithMany()
                .HasForeignKey(t => t.ProjectKidCodeId);
            this.HasRequired(t => t.CompanySizeType)
                .WithMany()
                .HasForeignKey(t => t.CompanySizeTypeId);
            this.HasOptional(t => t.CompanyKidCode)
                .WithMany()
                .HasForeignKey(t => t.CompanyKidCodeId);
        }
    }
}
