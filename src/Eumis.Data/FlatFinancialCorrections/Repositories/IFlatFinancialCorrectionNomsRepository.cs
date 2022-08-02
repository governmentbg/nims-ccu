using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;

namespace Eumis.Data.FlatFinancialCorrections.Repositories
{
    public interface IFlatFinancialCorrectionNomsRepository : IEntityNomsRepository<FlatFinancialCorrection, EntityNomVO>
    {
    }
}