namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractProcurement
{
    internal delegate IContractProcurementClaimsContext ContractProcurementClaimsContextFactory(int procurementId);

    internal interface IContractProcurementClaimsContext
    {
        int ContractProcurementId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
