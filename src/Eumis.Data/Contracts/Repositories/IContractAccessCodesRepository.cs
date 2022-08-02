using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractAccessCodesRepository : IAggregateRepository<ContractAccessCode>
    {
        IList<ContractAccessCodeVO> GetContractAccessCodes(bool isSuperUser);

        IList<ContractAccessCodeVO> GetContractAccessCodes(int contractId, bool isSuperUser);

        PagePVO<ContractAccessCodePVO> GetContractAccessCodes(Guid contractGid, int offset = 0, int? limit = null);

        ContractAccessCode Find(Guid gid);

        ContractAccessCode FindForUpdate(Guid gid, byte[] version);

        int GetContractId(int contractAccessCodeId);
    }
}
