using Eumis.Domain.CertReports;

namespace Eumis.Data.CertReports.Repositories
{
    public interface ICertReportSnapshotsRepository : IAggregateRepository<CertReportSnapshot>
    {
        CertReportSnapshot FindByCertReport(int certReportId);
    }
}
