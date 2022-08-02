using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractVersionsRepository : IAggregateRepository<ContractVersionXml>
    {
        ContractVersionXml FindWithIncludedAmounts(int contractVersionXmlId);

        IList<ContractVersionVO> GetContractVersions(int contractId);

        IList<ContractVersionXml> GetNonArchivedVersions(int contractId);

        int GetVersionId(Guid gid);

        int GetProjectId(int versionId);

        ContractVersionXml Find(Guid xmlGid);

        ContractVersionXml FindForUpdate(Guid xmlGid, byte[] version);

        ContractVersionXml GetLastVersion(int contractId);

        ContractVersionXml GetActiveVersion(int contractId);

        Task<ContractVersionXml> GetActiveVersionAsync(int contractId, CancellationToken ct);

        string GetActualVersionXml(Guid contractGid);

        ContractVersionXml FindForDraftContract(int contractId);

        ContractVersionXml FindForDraftContractForUpdate(int contractId, byte[] version);

        string GetAnnexRegNumber(int contractId, int versionNum);

        bool HasContractVersionsInProgress(int contractId);

        bool HasContractBlockingVersionsInProgress(int contractId);

        Task<bool> HasContractBlockingVersionsInProgressAsync(int contractId, CancellationToken ct);

        int GetContractId(int versionId);

        PagePVO<ContractVersionPVO> GetPortalContractVersions(Guid contractGid, int offset = 0, int? limit = null);

        ContractVersionSAPDataVO GetContractVersionSAPData(int contractVersionId);
    }
}
