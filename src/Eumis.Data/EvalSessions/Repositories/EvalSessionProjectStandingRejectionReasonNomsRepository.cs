using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class EvalSessionProjectStandingRejectionReasonNomsRepository : EntityNomsRepository<EvalSessionProjectStandingRejectionReason, EntityNomVO>
    {
        public EvalSessionProjectStandingRejectionReasonNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.EvalSessionProjectStandingRejectionReasonId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.EvalSessionProjectStandingRejectionReasonId,
                    Name = t.Name,
                })
        {
        }
    }
}
