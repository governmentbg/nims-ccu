using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class FinancialCorrectionImposingReasonNomsRepository : EntityNomsRepository<FinancialCorrectionImposingReason, EntityNomVO>
    {
        public FinancialCorrectionImposingReasonNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.FinancialCorrectionImposingReasonId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.FinancialCorrectionImposingReasonId,
                    Name = t.Name,
                })
        {
        }
    }
}
