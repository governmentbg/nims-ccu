using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    internal class ProgrammeAppFormDeclarationNomsRepository : EntityNomsRepository<ProgrammeAppFormDeclaration, EntityNomVO>, IProgrammeAppFormDeclarationNomsRepository
    {
        public ProgrammeAppFormDeclarationNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.ProgrammeDeclarationId,
                q => q.Name,
                q => q.NameAlt,
                q => new EntityNomVO
                {
                    NomValueId = q.ProgrammeDeclarationId,
                    Name = q.Name,
                    NameAlt = q.NameAlt,
                })
        {
        }

        public IEnumerable<EntityNomVO> GetDeclarations(int programmeId, int procedureId, string term, int offset = 0, int? limit = null)
        {
            var attachedDeclarations =
                from p in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>()
                where p.ProcedureId == procedureId
                select p.ProgrammeDeclarationId;

            var predicate = PredicateBuilder.True<ProgrammeDeclaration>()
                .AndEquals(d => d.ProgrammeId, programmeId)
                .AndEquals(d => d.IsActive, true)
                .AndAnyStringContains(d => d.Name, d => d.NameAlt, term)
                .And(d => !attachedDeclarations.Contains(d.ProgrammeDeclarationId));

            return (from ed in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>().Where(predicate)
                    select new EntityNomVO()
                    {
                        NomValueId = ed.ProgrammeDeclarationId,
                        Name = ed.Name,
                        NameAlt = ed.NameAlt,
                    })
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IEnumerable<EntityNomVO> GetNSIProgrammeDeclarations(int procedureId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<ProcedureAppFormDeclaration>()
                .AndEquals(d => d.ProcedureId, procedureId)
                .AndEquals(d => d.IsActive, true);

            return (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(predicate)
                    join ed in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>() on pd.ProgrammeDeclarationId equals ed.ProgrammeDeclarationId
                    where ed.IsConsentForNSIDataProviding
                    select new EntityNomVO()
                    {
                        NomValueId = ed.ProgrammeDeclarationId,
                        Name = ed.Name,
                        NameAlt = ed.NameAlt,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IEnumerable<EntityNomVO> GetNSIProgrammeDeclarationsFromProjectVersionXML(int projectVersionXmlId, string term, int offset = 0, int? limit = null)
        {
            var selectSqlParams = new List<SqlParameter>();
            selectSqlParams.Add(new SqlParameter("@projectVersionXmlId", projectVersionXmlId));

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
                      WHERE ProjectVersionXmlId = @projectVersionXmlId) tb1 WHERE tb1.Gid IS NOT NULL AND tb1.Accept IS NOT NULL";

            var declarationGids = this.SqlQuery<Guid>(query, selectSqlParams).ToList();

            var predicate = PredicateBuilder.True<ProcedureAppFormDeclaration>()
                .And(pd => declarationGids.Contains(pd.Gid))
                .AndEquals(d => d.IsActive, true);

            return (from pd in this.unitOfWork.DbContext.Set<ProcedureAppFormDeclaration>().Where(predicate)
                    join ed in this.unitOfWork.DbContext.Set<ProgrammeDeclaration>() on pd.ProgrammeDeclarationId equals ed.ProgrammeDeclarationId
                    where ed.IsConsentForNSIDataProviding
                    select new EntityNomVO()
                    {
                        NomValueId = ed.ProgrammeDeclarationId,
                        Name = ed.Name,
                        NameAlt = ed.NameAlt,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
