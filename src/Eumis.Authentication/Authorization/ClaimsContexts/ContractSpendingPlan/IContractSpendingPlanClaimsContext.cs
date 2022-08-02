namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractSpendingPlan
{
    internal delegate IContractSpendingPlanClaimsContext ContractSpendingPlanClaimsContextFactory(int spendingPlanId);

    internal interface IContractSpendingPlanClaimsContext
    {
        int ContractSpendingPlanId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
