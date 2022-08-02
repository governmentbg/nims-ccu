using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionStandpointStatusEnumNomsRepository : EnumNomsRepository<EvalSessionStandpointStatus>, IEvalSessionStandpointStatusEnumNomsRepository
    {
        private IUnitOfWork unitOfWork;

        public EvalSessionStandpointStatusEnumNomsRepository(IUnitOfWork unitOfWork)
            : base()
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<EnumNomVO<EvalSessionStandpointStatus>> GetNoms(EvalSessionStandpointStatus[] ids, string term)
        {
            var results = Enum.GetValues(typeof(EvalSessionStandpointStatus))
                .Cast<EvalSessionStandpointStatus>()
                .Select(e => new EnumNomVO<EvalSessionStandpointStatus>(e));

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
