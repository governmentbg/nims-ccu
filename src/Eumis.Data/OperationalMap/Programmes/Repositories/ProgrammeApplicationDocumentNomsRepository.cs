using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    internal class ProgrammeApplicationDocumentNomsRepository : EntityNomsRepository<ProgrammeApplicationDocument, EntityNomVO>, IProgrammeApplicationDocumentNomsRepository
    {
        public ProgrammeApplicationDocumentNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.ProgrammeApplicationDocumentId,
                q => q.Name,
                q => new EntityNomVO
                {
                    NomValueId = q.ProgrammeApplicationDocumentId,
                    Name = q.Name,
                })
        {
        }

        public IEnumerable<ProgrammeApplicationDocumentNomVO> GetProgrammeApplicationDocuments(int procedureId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<ProgrammeApplicationDocument>()
                .And(d => d.IsActive)
                .AndStringContains(d => d.Name, term);

            var procedureSharesPredicate = PredicateBuilder.True<ProcedureShare>()
                .AndEquals(s => s.ProcedureId, procedureId);

            var existingDocuments = this.unitOfWork.DbContext.Set<ProcedureApplicationDoc>()
                .Where(d => d.ProcedureId == procedureId)
                .Select(d => d.Name);

            return (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(procedureSharesPredicate)
                    join ad in this.unitOfWork.DbContext.Set<ProgrammeApplicationDocument>().Where(predicate)
                    on ps.ProgrammeId equals ad.ProgrammeId
                    where !existingDocuments.Contains(ad.Name)
                    select new ProgrammeApplicationDocumentNomVO()
                    {
                        NomValueId = ad.ProgrammeApplicationDocumentId,
                        Name = ad.Name,
                        IsSignatureRequired = ad.IsSignatureRequired,
                        Extension = ad.Extension,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
