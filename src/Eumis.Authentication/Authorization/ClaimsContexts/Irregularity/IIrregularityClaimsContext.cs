namespace Eumis.Authentication.Authorization.ClaimsContexts.Irregularity
{
    internal delegate IIrregularityClaimsContext IrregularityClaimsContextFactory(int irregularityId);

    internal interface IIrregularityClaimsContext
    {
        int IrregularityId { get; }

        int ProgrammeId { get; }

        int? ContractId { get; }

        bool IsIrregularityAssociatedWithFinancialCorrection();
    }
}
