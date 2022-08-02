using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractSpendingPlansRepository : IAggregateRepository<ContractSpendingPlanXml>
    {
        IList<ContractSpendingPlanVO> GetContractSpendingPlans(int contractId);

        IList<ContractSpendingPlanXml> GetNonArchivedSpendingPlans(int contractId);

        int GetSpendingPlanId(Guid gid);

        int GetProjectId(int spendingPlanId);

        ContractSpendingPlanXml Find(Guid gid, Source source);

        ContractSpendingPlanXml FindForUpdate(Guid gid, Source source, byte[] version);

        ContractSpendingPlanXml GetLastSpendingPlanOrDefault(int contractId);

        string GetActualSpendingPlanXml(Guid contractGid);

        int GetContractId(int spendingPlanId);

        bool HasContractSpendingPlansInProgress(int contractId);

        PagePVO<ContractSpendingPlanPVO> GetPortalContractSpendingPlans(Guid contractGid, int offset = 0, int? limit = null);
    }
}
