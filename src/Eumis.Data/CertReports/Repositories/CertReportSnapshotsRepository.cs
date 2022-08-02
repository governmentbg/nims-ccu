using Eumis.Common.Db;
using Eumis.Domain.CertReports;
using System.Linq;

namespace Eumis.Data.CertReports.Repositories
{
    internal class CertReportSnapshotsRepository : AggregateRepository<CertReportSnapshot>, ICertReportSnapshotsRepository
    {
        public CertReportSnapshotsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public CertReportSnapshot FindByCertReport(int certReportId)
        {
            return this.Set().Where(t => t.CertReportId == certReportId).Single();
        }
    }
}
