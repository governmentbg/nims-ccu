using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportMicrosDistrictNomsRepository : IEntityNomsRepository<ContractReportMicrosDistrict, EntityNomVO>
    {
        IList<EntityNomVO> GetAllDistrictNoms();
    }
}
