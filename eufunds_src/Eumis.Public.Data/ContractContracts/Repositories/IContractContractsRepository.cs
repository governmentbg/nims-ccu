using Eumis.Public.Data.ContractContracts.ViewObjects;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Data.ContractContracts.Repositories
{
    public interface IContractContractsRepository
    {
        PageVO<ContractContractVO> GetContractContracts(
            int? programmeId = null,
            string beneficiary = null,
            string companyUin = null,
            int? errandLegalActId = null,
            int offset = 0,
            int? limit = null);
    }
}
