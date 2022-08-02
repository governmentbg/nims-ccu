using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractReportStatusWithoutDraftNomsRepository : EnumNomsRepository<ContractReportStatus>, IContractReportStatusWithoutDraftNomsRepository
    {
        public new IList<EnumNomVO<ContractReportStatus>> GetNoms(string term)
        {
            return base.GetNoms(term).Where(p => p.NomValueId != ContractReportStatus.Draft).ToList();
        }
    }
}
