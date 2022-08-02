namespace Eumis.Authentication.Authorization.ClaimsContexts.CertReportCheck
{
    internal delegate ICertReportCheckClaimsContext CertReportCheckClaimsContextFactory(int certReportId);

    internal interface ICertReportCheckClaimsContext
    {
        int CertReportId { get; }

        int ProgrammeId { get; }
    }
}
