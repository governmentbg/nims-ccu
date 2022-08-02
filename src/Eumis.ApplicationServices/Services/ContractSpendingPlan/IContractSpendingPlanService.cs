using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractSpendingPlan
{
    public interface IContractSpendingPlanService
    {
        IList<string> CanCreateSpendingPlan(int contractId);

        IList<string> CanCreateSpendingPlan(Guid contractGid);

        void ActivateSpendingPlan(int spendingPlanId, byte[] version);

        Domain.Contracts.ContractSpendingPlanXml CreateSpendingPlanFromAdministrativeAuthority(int contractId, string note);

        Domain.Contracts.ContractSpendingPlanXml CreateSpendingPlanFromBeneficiary(int contractId, string note);
    }
}
