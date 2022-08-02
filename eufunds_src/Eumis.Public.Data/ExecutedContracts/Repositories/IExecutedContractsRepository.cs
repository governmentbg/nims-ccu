using Eumis.Public.Data.ExecutedContracts.ViewObjects;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Data.ExecutedContracts.Repositories
{
    public interface IExecutedContractsRepository
    {
        PageVO<ExecutedContractVO> GetExecutedContracts(
            int? programmeId = null,
            int? procedureId = null,
            int? companyId = null,
            int offset = 0,
            int? limit = null);
    }
}
