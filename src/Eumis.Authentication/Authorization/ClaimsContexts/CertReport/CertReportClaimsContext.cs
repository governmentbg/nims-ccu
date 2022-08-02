using Autofac.Features.AttributeFilters;
using Eumis.Data.CertReports.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.CertReport
{
    internal class CertReportClaimsContext : ClaimsContext, ICertReportClaimsContext
    {
        private int certReportId;

        private IClaimsCache claimsCache;
        private ICertReportsRepository certReportsRepository;

        public CertReportClaimsContext(
            int certReportId,
            [KeyFilter(ClaimsCaches.CertReport)]IClaimsCache claimsCache,
            ICertReportsRepository certReportsRepository)
            : base(claimsCache)
        {
            this.certReportId = certReportId;
            this.claimsCache = claimsCache;
            this.certReportsRepository = certReportsRepository;
        }

        public int CertReportId
        {
            get
            {
                return this.certReportId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.certReportId,
                    new ClaimKey("ProgrammeId"),
                    () => this.certReportsRepository.GetProgrammeId(this.CertReportId));
            }
        }
    }
}
