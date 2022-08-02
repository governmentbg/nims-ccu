using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.Repositories
{
    public interface IFinancialCorrectionNomsRepository : IEntityNomsRepository<FinancialCorrection, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetNoms(int contractId, string term, int offset = 0, int? limit = null);
    }
}