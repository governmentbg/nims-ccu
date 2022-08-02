using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionSheetStatusEnumNomsRepository : EnumNomsRepository<EvalSessionSheetStatus>, IEvalSessionSheetStatusEnumNomsRepository
    {
        private IUnitOfWork unitOfWork;

        public EvalSessionSheetStatusEnumNomsRepository(IUnitOfWork unitOfWork)
            : base()
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<EnumNomVO<EvalSessionSheetStatus>> GetNoms(EvalSessionSheetStatus[] ids, string term)
        {
            var results = Enum.GetValues(typeof(EvalSessionSheetStatus))
                .Cast<EvalSessionSheetStatus>()
                .Select(e => new EnumNomVO<EvalSessionSheetStatus>(e));

            if (ids.Length != 0)
            {
                return results.Where(p => ids.Contains(p.NomValueId)).ToList();
            }
            else
            {
                return results.ToList();
            }
        }
    }
}
