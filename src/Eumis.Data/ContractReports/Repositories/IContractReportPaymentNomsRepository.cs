using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportPaymentNomsRepository : IEntityNomsRepository<ContractReportPayment, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetNoms(int contractId, int[] ids, string term, int offset = 0, int? limit = null);
    }
}