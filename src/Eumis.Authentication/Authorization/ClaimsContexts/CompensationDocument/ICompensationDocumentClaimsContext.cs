namespace Eumis.Authentication.Authorization.ClaimsContexts.CompensationDocument
{
    internal delegate ICompensationDocumentClaimsContext CompensationDocumentClaimsContextFactory(int compensationDocumentId);

    internal interface ICompensationDocumentClaimsContext
    {
        int CompensationDocumentId { get; }

        int ProgrammeId { get; }
    }
}
