using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportTypeNomsRepository : IEnumNomsRepository<ContractReportType>
    {
        IList<EnumNomVO<ContractReportType>> GetNoms(int contractId, string term, int offset = 0, int? limit = null);
    }
}
