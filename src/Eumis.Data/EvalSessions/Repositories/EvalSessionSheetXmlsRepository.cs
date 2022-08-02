using System;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.PortalViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Users;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionSheetXmlsRepository : AggregateRepository<EvalSessionSheetXml>, IEvalSessionSheetXmlsRepository
    {
        public EvalSessionSheetXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<EvalSessionSheetXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<EvalSessionSheetXml, object>>[]
                {
                    p => p.Files,
                };
            }
        }

        public EvalSessionSheetXml FindByEvalSessionSheetId(int evalSessionSheetId)
        {
            return this.Set()
                .Where(s => s.EvalSessionSheetId == evalSessionSheetId)
                .Single();
        }

        public EvalSessionSheetXml FindByGid(Guid gid)
        {
            return this.Set()
                .Where(s => s.Gid == gid)
                .Single();
        }

        public EvalSessionSheetXml FindForUpdateByGid(Guid gid, byte[] version)
        {
            var evalSessionSheetXml = this.FindByGid(gid);
            this.CheckVersion(evalSessionSheetXml.Version, version);

            return evalSessionSheetXml;
        }

        public EvalSessionSheetXmlData GetDataByGid(Guid gid)
        {
            return (from sxml in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>()
                    join s in this.unitOfWork.DbContext.Set<EvalSessionSheet>() on new { sxml.EvalSessionId, sxml.EvalSessionSheetId } equals new { s.EvalSessionId, s.EvalSessionSheetId }
                    join esu in this.unitOfWork.DbContext.Set<EvalSessionUser>() on s.EvalSessionUserId equals esu.EvalSessionUserId
                    join p in this.unitOfWork.DbContext.Set<Project>() on s.ProjectId equals p.ProjectId
                    join u in this.unitOfWork.DbContext.Set<User>() on esu.UserId equals u.UserId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals proc.ProcedureId
                    where sxml.Gid == gid
                    select new EvalSessionSheetXmlData
                    {
                        ModifyDate = sxml.ModifyDate,
                        Xml = sxml.Xml,
                        Version = sxml.Version,
                        SheetData = new EvalSessionSheetData
                        {
                            ProjectId = s.ProjectId,
                            AssessorName = u.Fullname,
                            ProjectName = p.Name,
                            ProjectNameAlt = p.NameAlt,
                            ProjectRegNumber = p.RegNumber,
                            ProcedureName = proc.Name,
                            ProcedureNameAlt = proc.NameAlt,
                            CompanyName = p.CompanyName,
                            CompanyNameAlt = p.CompanyNameAlt,
                            EvalTableType = s.EvalTableType,
                            Status = s.Status,
                        },
                    }).Single();
        }

        public int GetEvalSessionSheetId(Guid gid)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>()
                    where ess.Gid == gid
                    select ess.EvalSessionSheetId).Single();
        }

        public int GetEvalSessionId(Guid gid)
        {
            return (from ess in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>()
                    where ess.Gid == gid
                    select ess.EvalSessionId).Single();
        }

        public int GetProjectId(Guid gid)
        {
            return (from essx in this.unitOfWork.DbContext.Set<EvalSessionSheetXml>()
                    join ess in this.unitOfWork.DbContext.Set<EvalSessionSheet>() on essx.EvalSessionSheetId equals ess.EvalSessionSheetId
                    where essx.Gid == gid
                    select ess.ProjectId).Single();
        }
    }
}
