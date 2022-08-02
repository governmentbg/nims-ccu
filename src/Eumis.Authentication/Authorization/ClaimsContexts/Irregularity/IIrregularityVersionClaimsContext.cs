namespace Eumis.Authentication.Authorization.ClaimsContexts.Irregularity
{
    internal delegate IIrregularityVersionClaimsContext IrregularityVersionClaimsContextFactory(int irregularityVersionId);

    internal interface IIrregularityVersionClaimsContext
    {
        int IrregularityVersionId { get; }

        int IrregularityId { get; }

        int ProgrammeId { get; }

        bool IsIrregularityAssociatedWithFinancialCorrection();
    }
}
