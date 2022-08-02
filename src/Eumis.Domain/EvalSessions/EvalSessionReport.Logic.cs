using System;
using Eumis.Domain.Projects;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSessionReport
    {
        public EvalSessionReportProject AddEvalSessionProject(
            int projectId,
            int? projectVersionId,
            string regNumber,
            DateTime regDate,
            DateTime recieveDate,
            ProjectRecieveType recieveType,
            string name,
            int? duration,
            int? projectKidCodeId,
            string projectPlace,
            decimal? grandAmount,
            decimal? coFinancingAmount,
            EvalSessionReportProjectStandingStatus standingStatus,
            int? standingNum,
            decimal? standingGrandAmount,
            string companyUin,
            string companyName,
            string regEmail,
            string correspondence,
            bool hasAdminAdmiss,
            EvalSessionEvaluationResult? adminAdmissResult,
            decimal? adminAdmissPoints,
            bool hasTechFinance,
            EvalSessionEvaluationResult? techFinanceResult,
            decimal? techFinancePoints,
            bool hasComplex,
            EvalSessionEvaluationResult? complexResult,
            decimal? complexPoints)
        {
            EvalSessionReportProject project = new EvalSessionReportProject()
            {
                EvalSessionId = this.EvalSessionId,
                EvalSessionReportId = this.EvalSessionReportId,
                ProjectId = projectId,
                ProjectVersionId = projectVersionId,
                RegNumber = regNumber,
                RegDate = regDate,
                RecieveDate = recieveDate,
                RecieveType = recieveType,
                Name = name,
                Duration = duration,
                ProjectKidCodeId = projectKidCodeId,
                ProjectPlace = projectPlace,
                GrandAmount = grandAmount,
                CoFinancingAmount = coFinancingAmount,
                StandingStatus = standingStatus,
                StandingNum = standingNum,
                StandingGrandAmount = standingGrandAmount,
                CompanyUin = companyUin,
                CompanyName = companyName,
                RegEmail = regEmail,
                Correspondence = correspondence,
                HasAdminAdmiss = hasAdminAdmiss,
                AdminAdmissResult = adminAdmissResult,
                AdminAdmissPoints = adminAdmissPoints,
                HasTechFinance = hasTechFinance,
                TechFinanceResult = techFinanceResult,
                TechFinancePoints = techFinancePoints,
                HasComplex = hasComplex,
                ComplexResult = complexResult,
                ComplexPoints = complexPoints,
            };

            this.Projects.Add(project);

            return project;
        }

        public void Remove(string isDeletedNote)
        {
            this.IsDeleted = true;
            this.IsDeletedNote = isDeletedNote;

            this.ModifyDate = DateTime.Now;
        }
    }
}
