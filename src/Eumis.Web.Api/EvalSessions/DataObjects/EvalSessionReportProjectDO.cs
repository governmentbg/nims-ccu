using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionReportProjectDO
    {
        public EvalSessionReportProjectDO()
        {
            this.Partners = new List<EvalSessionReportProjectPartnerDO>();
        }

        public EvalSessionReportProjectDO(EvalSessionReportProject project)
        {
            this.EvalSessionId = project.EvalSessionId;
            this.EvalSessionReportId = project.EvalSessionReportId;
            this.RegNumber = project.RegNumber;
            this.RegDate = project.RegDate;
            this.RecieveDate = project.RecieveDate;
            this.RecieveType = project.RecieveType;
            this.Name = project.Name;
            this.Duration = project.Duration;
            this.ProjectKidCode = project.ProjectKidCode == null ? null : string.Format("{0} {1}", project.ProjectKidCode.Code, project.ProjectKidCode.Name);
            this.ProjectPlace = project.ProjectPlace;
            this.GrandAmount = project.GrandAmount;
            this.CoFinancingAmount = project.CoFinancingAmount;
            this.StandingStatusId = project.StandingStatus;
            this.StandingStatus = project.StandingStatus;
            this.StandingNum = project.StandingNum;
            this.StandingGrandAmount = project.StandingGrandAmount;
            this.CompanyUin = project.CompanyUin;
            this.CompanyName = project.CompanyName;
            this.CompanyKidCode = project.CompanyKidCode == null ? null : string.Format("{0} {1}", project.CompanyKidCode.Code, project.CompanyKidCode.Name);
            this.RegEmail = project.RegEmail;
            this.Correspondence = project.Correspondence;
            this.HasAdminAdmiss = project.HasAdminAdmiss;
            this.AdminAdmissResult = project.AdminAdmissResult;
            this.AdminAdmissPoints = project.AdminAdmissPoints;
            this.HasTechFinance = project.HasTechFinance;
            this.TechFinanceResult = project.TechFinanceResult;
            this.TechFinancePoints = project.TechFinancePoints;
            this.HasComplex = project.HasComplex;
            this.ComplexResult = project.ComplexResult;
            this.ComplexPoints = project.ComplexPoints;
            this.Partners = project.Partners.Select(p => new EvalSessionReportProjectPartnerDO(p)).ToList();
        }

        public int EvalSessionId { get; set; }

        public int EvalSessionReportId { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime RecieveDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProjectRecieveType RecieveType { get; set; }

        public string Name { get; set; }

        public int? Duration { get; set; }

        public string ProjectKidCode { get; set; }

        public string ProjectPlace { get; set; }

        public decimal? GrandAmount { get; set; }

        public decimal? CoFinancingAmount { get; set; }

        public EvalSessionReportProjectStandingStatus StandingStatusId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionReportProjectStandingStatus StandingStatus { get; set; }

        public int? StandingNum { get; set; }

        public decimal? StandingGrandAmount { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public string CompanyKidCode { get; set; }

        public string RegEmail { get; set; }

        public string Correspondence { get; set; }

        public bool HasAdminAdmiss { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionEvaluationResult? AdminAdmissResult { get; set; }

        public decimal? AdminAdmissPoints { get; set; }

        public bool HasTechFinance { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionEvaluationResult? TechFinanceResult { get; set; }

        public decimal? TechFinancePoints { get; set; }

        public bool HasComplex { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionEvaluationResult? ComplexResult { get; set; }

        public decimal? ComplexPoints { get; set; }

        public IList<EvalSessionReportProjectPartnerDO> Partners { get; set; }
    }
}
