using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Data.Sebra.ViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;

namespace Eumis.Data.Sebra.Repositories
{
    internal class SebraRepository : Repository, ISebraRepository
    {
        private static readonly Regex ProjectRegNumberRegex = new Regex(@"([A-Z0-9\-]*)-(?<projectNumber>\d\d\d\d)");

        public SebraRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public string GetProcedureCode(int procedureId)
        {
            return this.unitOfWork.DbContext.Set<Procedure>()
                .Where(p => p.ProcedureId == procedureId)
                .Select(p => p.Code)
                .Single();
        }

        public SebraCompanyVO GetProgrammeCompany(int programmeId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Programme>()

                    join c in this.unitOfWork.DbContext.Set<Eumis.Domain.Companies.Company>() on p.CompanyId equals c.CompanyId into g0
                    from c in g0.DefaultIfEmpty()

                    where p.MapNodeId == programmeId
                    select new SebraCompanyVO
                    {
                        Uin = c.Uin ?? string.Empty,
                        Name = c.Name ?? string.Empty,
                    })
                    .Single();
        }

        public List<SebraProjectVO> GetProjects(int[] projectIds)
        {
            var projects = this.unitOfWork.DbContext.Set<Project>()
                .Where(p => projectIds.Contains(p.ProjectId))
                .ToList();

            if (projects.Select(p => p.ProcedureId).Distinct().Count() > 1)
            {
                return new List<SebraProjectVO>();
            }

            var ibans = this.GetProjectIbans(projectIds).ToList();

            var result =
                (from p in projects
                 join iban in ibans on p.ProjectId equals iban.ProjectId
                 join pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on iban.ProjectVersionXmlId equals pv.ProjectVersionXmlId
                 where pv.TotalBfpAmount > 0
                 select new SebraProjectVO
                 {
                     ProjectId = p.ProjectId,
                     RegDate = p.RegDate,
                     RegNumber = p.RegNumber,
                     BeneficiaryName = p.CompanyName,
                     BankAccount = iban.Iban,
                     PaidBfpTotalAmount = pv.TotalBfpAmount,
                 })
                 .ToList();

            return result;
        }

        public List<SebraProjectVO> GetProjects(int procedureId, DateTime fromDate, DateTime toDate, int fromNumber, int toNumber)
        {
            var newToDate = toDate.Date.AddDays(1).AddTicks(-1);

            var predicate = PredicateBuilder.True<Project>()
                .And(p => p.ProcedureId == procedureId)
                .AndDateTimeGreaterThanOrEqual(p => p.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(p => p.RegDate, newToDate)
                .And(p => p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn);

            var projects = this.unitOfWork.DbContext.Set<Project>()
                .Where(predicate)
                .ToList()
                .Select(p =>
                {
                    var match = ProjectRegNumberRegex.Match(p.RegNumber);

                    return new
                    {
                        ProjectId = p.ProjectId,
                        RegDate = p.RegDate,
                        RegNumber = p.RegNumber,
                        BeneficiaryName = p.CompanyName,
                        ProjectNumber = int.Parse(match.Groups["projectNumber"].Value),
                    };
                })
                .ToList();

            var filteredProjectIds = projects
                .Where(t => fromNumber <= t.ProjectNumber && t.ProjectNumber <= toNumber)
                .Select(t => t.ProjectId)
                .ToArray();

            projects = projects.Where(p => filteredProjectIds.Contains(p.ProjectId)).ToList();

            var projectIds = projects.Select(p => p.ProjectId).ToArray();

            var ibans = this.GetProjectIbans(projectIds).ToList();

            var result =
                (from p in projects
                 join iban in ibans on p.ProjectId equals iban.ProjectId
                 join pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on iban.ProjectVersionXmlId equals pv.ProjectVersionXmlId
                 where pv.TotalBfpAmount > 0
                 select new SebraProjectVO
                 {
                     ProjectId = p.ProjectId,
                     RegDate = p.RegDate,
                     RegNumber = p.RegNumber,
                     BeneficiaryName = p.BeneficiaryName,
                     BankAccount = iban.Iban,
                     PaidBfpTotalAmount = pv.TotalBfpAmount,
                 })
                 .ToList();

            return result;
        }

        public SebraProjectInfoVO GetProjectsInfo(int[] projectIds)
        {
            var orderedArray = projectIds.OrderBy(i => i).ToArray();
            var firstProjectId = orderedArray.First();
            var lastProjectId = orderedArray.Last();

            var firstProjet =
                (from p in this.unitOfWork.DbContext.Set<Project>()
                 join pr in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals pr.ProcedureId
                 join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                 join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId

                 join c in this.unitOfWork.DbContext.Set<Eumis.Domain.Companies.Company>() on prog.CompanyId equals c.CompanyId into g0
                 from c in g0.DefaultIfEmpty()

                 where p.ProjectId == firstProjectId && ps.IsPrimary == true
                 select new
                 {
                     RegNumber = p.RegNumber,
                     ProcedureCode = pr.Code,
                     CompanyUin = c.Uin ?? string.Empty,
                     CompanyName = c.Name ?? string.Empty,
                 })
                 .FirstOrDefault();

            var lastProjectRegNumber =
                this.unitOfWork.DbContext.Set<Project>()
                .Where(p => p.ProjectId == lastProjectId)
                .Select(p => p.RegNumber)
                .FirstOrDefault();

            return new SebraProjectInfoVO
            {
                FirstProjectNumber = ProjectRegNumberRegex.Match(firstProjet.RegNumber).Groups["projectNumber"].Value,
                LastProjectNumber = ProjectRegNumberRegex.Match(lastProjectRegNumber).Groups["projectNumber"].Value,
                ProcedureCode = firstProjet.ProcedureCode,
                CompanyUin = firstProjet.CompanyUin,
                CompanyName = firstProjet.CompanyUin,
            };
        }

        public IList<SebraProjectIbanVO> GetProjectIbans(int[] projectIds)
        {
            if (projectIds.Any())
            {
                string whereIn = string.Join(",", projectIds);

                string projectDataQuery = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10017' as R10017,
                    N'http://ereg.egov.bg/segment/R-10019' as R10019
                ),
                ProjectXmlData as
                (
                    SELECT
                        pvxml.ProjectId as ProjectId,
                        pvxml.ProjectVersionXmlId as ProjectVersionXmlId,
                        n.SpecFields.value('(R10017:Id/text())[1]', 'NVARCHAR(MAX)') as [Gid],
                        n.SpecFields.value('(R10017:Value/text())[1]', 'NVARCHAR(MAX)') as [Value]
                    FROM
                        ProjectVersionXmls pvxml
                        OUTER APPLY pvxml.Xml.nodes('(/Project/R10019:ProjectSpecFields/*:ProjectSpecField)') n(SpecFields)
                    WHERE
                        pvxml.ProjectId IN ({whereIn})
                        AND pvxml.Status = 2
                )
                SELECT
                    pxml.ProjectId,
                    pxml.ProjectVersionXmlId,
                    pxml.Value as Iban
                FROM ProjectXmlData pxml
                    JOIN ProcedureSpecFields psf ON pxml.Gid = psf.Gid
                WHERE psf.MaxLength = 0";

                return this.SqlQuery<SebraProjectIbanVO>(projectDataQuery, new List<System.Data.SqlClient.SqlParameter>()).ToList();
            }
            else
            {
                return new List<SebraProjectIbanVO>();
            }
        }
    }
}
