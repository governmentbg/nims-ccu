using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class ProcedureEvalTableTypeEnumNomsRepository : Repository, IProcedureEvalTableTypeEnumNomsRepository
    {
        public ProcedureEvalTableTypeEnumNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EnumNomVO<ProcedureEvalTableType> GetNom(ProcedureEvalTableType e)
        {
            return new EnumNomVO<ProcedureEvalTableType>(e);
        }

        public IList<EnumNomVO<ProcedureEvalTableType>> GetNoms(string term)
        {
            return Enum.GetValues(typeof(ProcedureEvalTableType))
                .Cast<ProcedureEvalTableType>()
                .Select(e => new EnumNomVO<ProcedureEvalTableType>(e))
                .ToList();
        }

        public IList<EnumNomVO<ProcedureEvalTableType>> GetEvalSessionSheetTypes(int evalSessionId, string term)
        {
            return (from es in this.unitOfWork.DbContext.Set<EvalSession>()
                    join pet in this.unitOfWork.DbContext.Set<ProcedureEvalTable>() on es.ProcedureId equals pet.ProcedureId
                    where es.EvalSessionId == evalSessionId && pet.IsActivated && pet.IsActive
                    select pet.Type)
                    .ToList()
                    .Select(e => new EnumNomVO<ProcedureEvalTableType>(e))
                    .ToList();
        }
    }
}
