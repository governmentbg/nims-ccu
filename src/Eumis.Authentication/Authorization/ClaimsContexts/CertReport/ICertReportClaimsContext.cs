namespace Eumis.Authentication.Authorization.ClaimsContexts.CertReport
{
    internal delegate ICertReportClaimsContext CertReportClaimsContextFactory(int certReportId);

    internal interface ICertReportClaimsContext
    {
        int CertReportId { get; }

        int ProgrammeId { get; }
    }
}
