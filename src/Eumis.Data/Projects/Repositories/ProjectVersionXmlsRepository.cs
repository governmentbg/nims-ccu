using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Projects;
using Eumis.Domain.Projects.ViewObjects;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectVersionXmlsRepository : AggregateRepository<ProjectVersionXml>, IProjectVersionXmlsRepository
    {
        public ProjectVersionXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProjectVersionXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProjectVersionXml, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ProjectVersionVO> GetProjectVersions(int projectId, bool finalizedOnly = false)
        {
            var predicate = PredicateBuilder.True<ProjectVersionXml>();

            if (finalizedOnly)
            {
                predicate = predicate.And(p => p.Status != ProjectVersionXmlStatus.Draft);
            }

            var results = (from pvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>().Where(predicate)

                           join pf in this.unitOfWork.DbContext.Set<ProjectFile>() on pvx.ProjectVersionXmlId equals pf.ProjectVersionXmlId into g1
                           from pf in g1.DefaultIfEmpty()

                           join pfs in this.unitOfWork.DbContext.Set<ProjectFileSignature>() on pf.ProjectFileId equals pfs.ProjectFileId into g2
                           from pfs in g2.DefaultIfEmpty()

                           where pvx.ProjectId == projectId
                           select new { pvx, pf, pfs }).ToList();

            return results.GroupBy(g => g.pvx.ProjectVersionXmlId)
                .Select(p => new ProjectVersionVO()
                {
                    ProjectVersionId = p.Key,
                    ProjectId = p.First().pvx.ProjectId,
                    XmlGid = p.First().pvx.Gid,
                    Status = p.First().pvx.Status,
                    CreateDate = p.First().pvx.CreateDate,
                    CreateNoteBg = p.First().pvx.CreateNote,
                    CreateNoteEn = p.First().pvx.CreateNoteAlt,
                    ModifyDate = p.First().pvx.ModifyDate,
                    ProjectFile = p.First().pf != null ? new InternalFileVO(p.First().pf.ProjectFileId, p.First().pf.FileName) : null,
                    ProjectFileSignatures = p.Where(t => t.pfs != null).Select(t => new InternalFileVO(t.pfs.ProjectFileSignatureId, t.pfs.FileName)).ToList(),
                })
                .OrderByDescending(p => p.CreateDate)
                .ToList();
        }

        public IList<ProjectVersionXml> GetNonArchivedProjectVersions(int projectId)
        {
            return this.Set()
                .Where(p => p.ProjectId == projectId && p.Status != ProjectVersionXmlStatus.Archive)
                .ToList();
        }

        public ProjectVersionXml GetActualProjectVersion(int projectId)
        {
            return this.Set()
                .Where(p => p.ProjectId == projectId && p.Status == ProjectVersionXmlStatus.Actual)
                .SingleOrDefault();
        }

        public ProjectVersionXml GetLastProjectVersion(int projectId)
        {
            return this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                .Where(p => p.ProjectId == projectId && p.Status != ProjectVersionXmlStatus.Draft)
                .OrderByDescending(p => p.CreateDate)
                .FirstOrDefault();
        }

        public ProjectVersionXml GetLastArchivedProjectVersion(int projectId)
        {
            return this.Set()
                .Where(p => p.ProjectId == projectId && p.Status == ProjectVersionXmlStatus.Archive)
                .OrderByDescending(p => p.OrderNum)
                .FirstOrDefault();
        }

        public ProjectVersionXml Find(Guid gid)
        {
            return this.Set()
                .Where(p => p.Gid == gid)
                .Single();
        }

        public ProjectVersionXml FindForUpdate(Guid gid, byte[] version)
        {
            var projectVersion = this.Find(gid);

            this.CheckVersion(projectVersion.Version, version);

            return projectVersion;
        }

        public new void Remove(ProjectVersionXml projectVersion)
        {
            if (projectVersion.Status != ProjectVersionXmlStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete nondraft project version");
            }

            base.Remove(projectVersion);
        }

        public ProjectVersionXmlStatus? GetLastVersionStatus(int projectId)
        {
            return (from px in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                    where px.ProjectId == projectId
                    orderby px.OrderNum descending
                    select px.Status)
                    .FirstOrDefault();
        }

        public bool HasProjectWithoutActualProjectVersion(int[] projectIds)
        {
            projectIds = projectIds.Distinct().ToArray();

            var projectsWithActualProjectVersionCount = (from pvx in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                                                         where projectIds.Contains(pvx.ProjectId) && pvx.Status == ProjectVersionXmlStatus.Actual
                                                         select pvx).Count();

            return !(projectsWithActualProjectVersionCount == projectIds.Count());
        }

        public int GetProjectId(int versionId)
        {
            return (from pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                    where pv.ProjectVersionXmlId == versionId
                    select pv.ProjectId).SingleOrDefault();
        }

        public int GetProjectVersionId(Guid gid)
        {
            return (from pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>()
                    where pv.Gid == gid
                    select pv.ProjectVersionXmlId).Single();
        }

        public int GetNextOrderNum(int projectId)
        {
            var lastOrderNumver = this.Set()
                .Where(c => c.ProjectId == projectId)
                .Max(c => (int?)c.OrderNum);

            return lastOrderNumver.HasValue ? lastOrderNumver.Value + 1 : 1;
        }

        public IList<ProjectGrandAmountsVO> GetProgrammeBudgetGrandAmountForActualProjectVersions(int[] projectIds)
        {
            if (projectIds.Length == 0)
            {
                return new List<ProjectGrandAmountsVO>();
            }

            var whereIn = string.Join(",", projectIds);

            string query = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10007' as R10007,
                    N'http://ereg.egov.bg/segment/R-10008' as R10008,
                    N'http://ereg.egov.bg/segment/R-10009' as R10009,
                    N'http://ereg.egov.bg/segment/R-10010' as R10010,
                    N'http://ereg.egov.bg/segment/R-10019' as R10019,
                    N'http://ereg.egov.bg/segment/R-09991' as R09991,
                    N'http://ereg.egov.bg/segment/R-09998' as R09998
                )
                SELECT tb1.ProjectId, 
                       tb1.ProjectVersionXmlId,
                       tb1.ProgrammePriorityCode,
                       tb1.GrandAmount 
                FROM (SELECT ProjectId, 
                             ProjectVersionXmlId,
                             pebPpc.i.value('.', 'varchar(20)') as 'ProgrammePriorityCode',  
                             pebPdebGa.i.value('.', 'MONEY') as 'GrandAmount'
                      FROM ProjectVersionXmls
                      CROSS APPLY Xml.nodes('/Project/R10019:DirectionsBudgetContract/R09998:Budget/R10010:ProgrammeBudget/R10009:ProgrammeExpenseBudget') AS peb(i)
                      OUTER APPLY peb.i.nodes('R10008:ProgrammePriorityCode') pebPpc(i)
                      OUTER APPLY peb.i.nodes('R10008:ProgrammeDetailsExpenseBudget/R10007:GrandAmount') pebPdebGa(i)
                      WHERE Status = 2 AND ProjectId IN ({whereIn})) tb1 WHERE tb1.GrandAmount is not null";

            return this.SqlQuery<ProjectGrandAmountsVO>(query, new List<SqlParameter>()).ToList();
        }

        public IList<ProcedureGrandAmountsVO> GetProgrammeBudgetSpentAmount(int[] projectIds)
        {
            if (projectIds.Length == 0)
            {
                return new List<ProcedureGrandAmountsVO>();
            }

            var whereIn = string.Join(",", projectIds);

            string query = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10007' as R10007,
                    N'http://ereg.egov.bg/segment/R-10008' as R10008,
                    N'http://ereg.egov.bg/segment/R-10009' as R10009,
                    N'http://ereg.egov.bg/segment/R-10010' as R10010,
                    N'http://ereg.egov.bg/segment/R-10019' as R10019,
                    N'http://ereg.egov.bg/segment/R-09991' as R09991,
                    N'http://ereg.egov.bg/segment/R-09998' as R09998
                )
                SELECT tb1.ProgrammePriorityCode,
                       SUM(tb1.GrandAmount) as 'GrandAmount'
                FROM (SELECT pebPpc.i.value('.', 'varchar(20)') as 'ProgrammePriorityCode', 
                             pebPdebGa.i.value('.', 'MONEY') as 'GrandAmount'
                      FROM ProjectVersionXmls
                      CROSS APPLY Xml.nodes('/Project/R10019:DirectionsBudgetContract/R09998:Budget/R10010:ProgrammeBudget/R10009:ProgrammeExpenseBudget') AS peb(i)
                      OUTER APPLY peb.i.nodes('R10008:ProgrammePriorityCode') pebPpc(i)
                      OUTER APPLY peb.i.nodes('R10008:ProgrammeDetailsExpenseBudget/R10007:GrandAmount') pebPdebGa(i)
                      WHERE Status = 2 AND ProjectId IN ({whereIn})) tb1 WHERE tb1.GrandAmount is not null GROUP BY ProgrammePriorityCode";

            return this.SqlQuery<ProcedureGrandAmountsVO>(query, new List<SqlParameter>()).ToList();
        }

        public int? GetActualProjectVersionId(int projectId)
        {
            return this.Set()
                .Where(p => p.ProjectId == projectId && p.Status == ProjectVersionXmlStatus.Actual)
                .Select(p => (int?)p.ProjectVersionXmlId)
                .SingleOrDefault();
        }

        public int? GetProjectVersionXmlFileId(int projectVersionXmlId, Guid typeGid)
        {
            int? projectVersionXmlFileId = null;

            var selectSqlParams = new List<SqlParameter>();
            selectSqlParams.Add(new SqlParameter("@projectVersionXmlId", projectVersionXmlId));
            selectSqlParams.Add(new SqlParameter("@typeGid", typeGid));

            string query = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10019' as R10019,
                    N'http://ereg.egov.bg/segment/R-10018' as R10018,
                    N'http://ereg.egov.bg/segment/R-10000' as R10000,
                    N'http://ereg.egov.bg/segment/R-09992' as R09992
                )
                SELECT tb1.BlobContentId
                FROM (SELECT
                        ProjectVersionXmlId,
                        pd.i.value('(R10018:Type/R10000:Id/text())[1]', 'UNIQUEIDENTIFIER') AS [TypeGid],
                        pd.i.value('(R10018:AttachedDocumentContent/R09992:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobContentId]
                      FROM ProjectVersionXmls
                      OUTER APPLY Xml.nodes('(/Project/R10019:AttachedDocuments/R10019:AttachedDocument)') AS pd(i)
                      WHERE ProjectVersionXmlId = @projectVersionXmlId) tb1 WHERE tb1.TypeGid = @typeGid";

            var blobContentId = this.SqlQuery<Guid?>(query, selectSqlParams).LastOrDefault();

            if (blobContentId.HasValue)
            {
                projectVersionXmlFileId = this.unitOfWork.DbContext.Set<ProjectVersionXmlFile>()
                    .Where(f => f.BlobKey == blobContentId && f.ProjectVersionXmlId == projectVersionXmlId)
                    .Select(f => f.FileId)
                    .FirstOrDefault();
            }

            return projectVersionXmlFileId;
        }

        public Guid? GetProjectVersionDeclarationGid(int projectVersionXmlId, Guid declarationGid)
        {
            var selectSqlParams = new List<SqlParameter>();
            selectSqlParams.Add(new SqlParameter("@projectVersionXmlId", projectVersionXmlId));
            selectSqlParams.Add(new SqlParameter("@declarationGid", declarationGid));

            string query = $@"WITH XMLNAMESPACES (
                    N'http://ereg.egov.bg/segment/R-10019' as R10019,
                    N'http://ereg.egov.bg/segment/R-10098' as R10098
                )
                SELECT tb1.Gid
                FROM (SELECT
                        ProjectVersionXmlId,
                        pd.i.value('(R10098:Gid/text())[1]', 'UNIQUEIDENTIFIER') AS [Gid],
                        pd.i.value('(R10098:FieldValue/text())[1]', 'NVARCHAR(MAX)') AS [Accept]
                      FROM ProjectVersionXmls
                      OUTER APPLY Xml.nodes('(/Project/R10019:ElectronicDeclarations/R10019:ElectronicDeclaration)') AS pd(i)
                      WHERE ProjectVersionXmlId = @projectVersionXmlId) tb1 WHERE tb1.Gid = @declarationGid AND tb1.Accept IS NOT NULL";

            var projectDeclarationGid = this.SqlQuery<Guid?>(query, selectSqlParams).SingleOrDefault();

            return projectDeclarationGid;
        }
    }
}
