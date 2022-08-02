using Eumis.Data.Companies.Repositories;
using Eumis.Data.Counters;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Services;
using Eumis.Rio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.EvalSessionReport
{
    public class EvalSessionReportService : IEvalSessionReportService
    {
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionReportsRepository evalSessionReportsRepository;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private ICompaniesRepository companiesRepository;
        private IRegistrationsRepository registrationsRepository;
        private ICountersRepository countersRepository;
        private INomenclatureDomainService nomenclaturesDomainService;

        public EvalSessionReportService(
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionReportsRepository evalSessionReportsRepository,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            ICompaniesRepository companiesRepository,
            IRegistrationsRepository registrationsRepository,
            ICountersRepository countersRepository,
            INomenclatureDomainService nomenclaturesDomainService)
        {
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionReportsRepository = evalSessionReportsRepository;
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.companiesRepository = companiesRepository;
            this.registrationsRepository = registrationsRepository;
            this.countersRepository = countersRepository;
            this.nomenclaturesDomainService = nomenclaturesDomainService;
        }

        public IList<string> CanDelete(int evalSessionReportId)
        {
            var errors = new List<string>();

            var report = this.evalSessionReportsRepository.Find(evalSessionReportId);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(report.EvalSessionId);

            if ((sessionStatus != EvalSessionStatus.Active && sessionStatus != EvalSessionStatus.EndedByLAG) || report.IsDeleted)
            {
                errors.Add(ApplicationServicesTexts.EvalSessionReportService_CanDelete_CannotDeleteReport);
            }

            return errors;
        }

        public IList<string> CanCreate(int evalSessionId)
        {
            var errors = new List<string>();

            var session = this.evalSessionsRepository.Find(evalSessionId);
            if (session.EvalSessionStatus != EvalSessionStatus.Active && session.EvalSessionStatus != EvalSessionStatus.EndedByLAG)
            {
                errors.Add(ApplicationServicesTexts.EvalSessionReportService_CanCreate_SessionInactive);
            }

            var projectIds = session.EvalSessionProjects.Select(t => t.ProjectId).ToArray();
            var projects = this.projectsRepository.FindAll(projectIds);

            foreach (var sessionProject in session.EvalSessionProjects)
            {
                var project = projects.Where(t => t.ProjectId == sessionProject.ProjectId).Single();
                var lastVersion = this.projectVersionXmlsRepository.GetLastProjectVersion(sessionProject.ProjectId);
                if (lastVersion != null)
                {
                    var projectXml = lastVersion.GetDocument();
                    foreach (var partner in projectXml.Partners.PartnerCollection)
                    {
                        if (string.IsNullOrEmpty(partner.Uin) ||
                            string.IsNullOrEmpty(partner.Name) ||
                            string.IsNullOrEmpty(partner.CompanyRepresentativePerson) ||
                            partner.CompanyLegalType == null ||
                            partner.CompanyLegalType.Id == null)
                        {
                            errors.Add(
                                string.Format(
                                    ApplicationServicesTexts.EvalSessionReportService_CanCreate_MissingPartnerData,
                                    project.RegNumber));
                            break;
                        }

                        if (partner.Correspondence != null &&
                            partner.Correspondence.Country != null &&
                            !string.IsNullOrEmpty(partner.Correspondence.Country.Code))
                        {
                            if (partner.Correspondence.Country.Code == "BG")
                            {
                                if (partner.Correspondence.Settlement == null ||
                                    string.IsNullOrEmpty(partner.Correspondence.Country.Name) ||
                                    string.IsNullOrEmpty(partner.Correspondence.Settlement.Name) ||
                                    string.IsNullOrEmpty(partner.Correspondence.PostCode) ||
                                    string.IsNullOrEmpty(partner.Correspondence.Street))
                                {
                                    errors.Add(
                                        string.Format(
                                            ApplicationServicesTexts.EvalSessionReportService_CanCreate_MissingPartnerData,
                                            project.RegNumber));
                                    break;
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(partner.Correspondence.FullAddress))
                                {
                                    errors.Add(
                                        string.Format(
                                            ApplicationServicesTexts.EvalSessionReportService_CanCreate_MissingPartnerData,
                                            project.RegNumber));
                                    break;
                                }
                            }
                        }
                        else
                        {
                            errors.Add(
                                string.Format(
                                    ApplicationServicesTexts.EvalSessionReportService_CanCreate_MissingPartnerData,
                                    project.RegNumber));
                            break;
                        }
                    }
                }
            }

            return errors;
        }

        public Eumis.Domain.EvalSessions.EvalSessionReport CreateReport(
            int evalSessionId,
            EvalSessionReportType type,
            string description)
        {
            this.countersRepository.CreateEvalSessionReportCounter(evalSessionId);

            var regNumber = this.countersRepository.GetNextEvalSessionReportNumber(evalSessionId, type);
            var report = new Eumis.Domain.EvalSessions.EvalSessionReport(evalSessionId, regNumber, type, description);

            var session = this.evalSessionsRepository.Find(evalSessionId);

            var projectIds = session.EvalSessionProjects.Select(t => t.ProjectId).ToArray();
            var projects = this.projectsRepository.FindAll(projectIds);
            var regMails = this.registrationsRepository.GetRegistrationEmailsForProjects(projectIds);

            foreach (var sessionProject in session.EvalSessionProjects)
            {
                var project = projects.Where(t => t.ProjectId == sessionProject.ProjectId).Single();
                var projectStanding = session.EvalSessionProjectStandings.SingleOrDefault(ps => ps.ProjectId == project.ProjectId && ps.IsDeleted == false && !ps.IsPreliminary);
                var status = this.GetStatus(projectStanding, sessionProject.IsDeleted, project.RegistrationStatus == ProjectRegistrationStatus.Withdrawn);
                var regEmail = regMails.Where(t => t.Item1 == project.ProjectId).Select(t => t.Item2).SingleOrDefault();

                var projectEvaluations = session.EvalSessionEvaluations.Where(e => e.ProjectId == project.ProjectId && e.IsDeleted == false);
                var adminAdmissEvaluation = projectEvaluations.SingleOrDefault(e => e.EvalTableType == ProcedureEvalTableType.AdminAdmiss);
                var techFinanceEvaluation = projectEvaluations.SingleOrDefault(e => e.EvalTableType == ProcedureEvalTableType.TechFinance);
                var complexEvaluation = projectEvaluations.SingleOrDefault(e => e.EvalTableType == ProcedureEvalTableType.Complex);

                var lastVersion = this.projectVersionXmlsRepository.GetLastProjectVersion(sessionProject.ProjectId);
                if (lastVersion != null)
                {
                    var projectXml = lastVersion.GetDocument();
                    var correspondence = projectXml.Candidate.Correspondence.Country.Code == "BG" ?
                        string.Format("{0}, {1} {2}, {3}", projectXml.Candidate.Correspondence.Country.Name, projectXml.Candidate.Correspondence.Settlement.Name, projectXml.Candidate.Correspondence.PostCode, projectXml.Candidate.Correspondence.Street) :
                        string.Format("{0}, {1}", projectXml.Candidate.Correspondence.Country.Name, projectXml.Candidate.Correspondence.FullAddress);

                    var reportProject = report.AddEvalSessionProject(
                        project.ProjectId,
                        lastVersion.ProjectVersionXmlId,
                        project.RegNumber,
                        project.RegDate,
                        project.RecieveDate,
                        project.RecieveType,
                        projectXml.ProjectBasicData.Name,
                        int.Parse(projectXml.ProjectBasicData.Duration),
                        projectXml.Candidate.GetPublicNomId(d => d.KidCodeProject, this.nomenclaturesDomainService.GetKidCodeNomIdByCode),
                        project.NutsAddressFullPathName,
                        project.TotalBfpAmount,
                        project.CoFinancingAmount,
                        status,
                        projectStanding == null ? null : projectStanding.OrderNum,
                        projectStanding == null ? null : projectStanding.GrandAmount,
                        projectXml.Candidate.Uin,
                        projectXml.Candidate.Name,
                        regEmail,
                        correspondence,
                        adminAdmissEvaluation != null,
                        this.GetEvalResult(adminAdmissEvaluation),
                        adminAdmissEvaluation == null ? null : adminAdmissEvaluation.EvalPoints,
                        techFinanceEvaluation != null,
                        this.GetEvalResult(techFinanceEvaluation),
                        techFinanceEvaluation == null ? null : techFinanceEvaluation.EvalPoints,
                        complexEvaluation != null,
                        this.GetEvalResult(complexEvaluation),
                        complexEvaluation == null ? null : complexEvaluation.EvalPoints);

                    foreach (var partner in projectXml.Partners.PartnerCollection)
                    {
                        var address = partner.Correspondence.Country.Code == "BG" ?
                        string.Format("{0}, {1} {2}, {3}", partner.Correspondence.Country.Name, partner.Correspondence.Settlement.Name, partner.Correspondence.PostCode, partner.Correspondence.Street) :
                        string.Format("{0}, {1}", partner.Correspondence.Country.Name, partner.Correspondence.FullAddress);

                        reportProject.AddPartner(
                            partner.Uin,
                            partner.Name,
                            partner.GetPrivateNomId(d => d.CompanyLegalType, this.nomenclaturesDomainService.GetCompanyLegalTypeNomIdByGid).Value,
                            address,
                            partner.CompanyRepresentativePerson,
                            partner.FinancialContribution);
                    }
                }
                else
                {
                    var candidate = this.companiesRepository.Find(project.CompanyId);
                    report.AddEvalSessionProject(
                        project.ProjectId,
                        null,
                        project.RegNumber,
                        project.RegDate,
                        project.RecieveDate,
                        project.RecieveType,
                        project.Name,
                        null,
                        null,
                        null,
                        null,
                        null,
                        status,
                        projectStanding == null ? null : projectStanding.OrderNum,
                        projectStanding == null ? null : projectStanding.GrandAmount,
                        candidate.Uin,
                        candidate.Name,
                        regEmail,
                        null,
                        adminAdmissEvaluation != null,
                        this.GetEvalResult(adminAdmissEvaluation),
                        adminAdmissEvaluation == null ? null : adminAdmissEvaluation.EvalPoints,
                        techFinanceEvaluation != null,
                        this.GetEvalResult(techFinanceEvaluation),
                        techFinanceEvaluation == null ? null : techFinanceEvaluation.EvalPoints,
                        complexEvaluation != null,
                        this.GetEvalResult(complexEvaluation),
                        complexEvaluation == null ? null : complexEvaluation.EvalPoints);
                }
            }

            return report;
        }

        private EvalSessionEvaluationResult? GetEvalResult(EvalSessionEvaluation evaluation)
        {
            if (evaluation == null)
            {
                return null;
            }

            return evaluation.EvalIsPassed ? EvalSessionEvaluationResult.Passed : EvalSessionEvaluationResult.NotPassed;
        }

        private EvalSessionReportProjectStandingStatus GetStatus(EvalSessionProjectStanding standing, bool isCanceled, bool isWithdrawed)
        {
            EvalSessionReportProjectStandingStatus? status = null;
            if (isCanceled)
            {
                status = EvalSessionReportProjectStandingStatus.Canceled;
            }
            else if (isWithdrawed)
            {
                status = EvalSessionReportProjectStandingStatus.Withdrawed;
            }

            if (standing == null)
            {
                status = EvalSessionReportProjectStandingStatus.WithoutStanding;
            }
            else
            {
                switch (standing.Status)
                {
                    case EvalSessionProjectStandingStatus.Approved:
                        status = EvalSessionReportProjectStandingStatus.Approved;
                        break;
                    case EvalSessionProjectStandingStatus.Reserve:
                        status = EvalSessionReportProjectStandingStatus.Reserve;
                        break;
                    case EvalSessionProjectStandingStatus.Rejected:
                        status = EvalSessionReportProjectStandingStatus.Rejected;
                        break;
                    case EvalSessionProjectStandingStatus.RejectedAtASD:
                        status = EvalSessionReportProjectStandingStatus.RejectedAtASD;
                        break;
                    case EvalSessionProjectStandingStatus.RejectedAtTFO:
                        status = EvalSessionReportProjectStandingStatus.RejectedAtTFO;
                        break;
                    default:
                        throw new InvalidOperationException("Invalid standing status.");
                }
            }

            return status.Value;
        }
    }
}
