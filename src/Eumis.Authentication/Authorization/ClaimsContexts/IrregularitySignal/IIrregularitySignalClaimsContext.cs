namespace Eumis.Authentication.Authorization.ClaimsContexts.IrregularitySignal
{
    internal delegate IIrregularitySignalClaimsContext IrregularitySignalClaimsContextFactory(int irregularitySignalId);

    internal interface IIrregularitySignalClaimsContext
    {
        int IrregularitySignalId { get; }

        int ProgrammeId { get; }

        int? ContractId { get; }
    }
}
