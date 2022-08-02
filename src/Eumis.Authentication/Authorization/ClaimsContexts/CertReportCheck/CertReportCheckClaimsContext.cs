using Autofac.Features.AttributeFilters;
using Eumis.Data.CertReports.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.CertReportCheck
{
    internal class CertReportCheckClaimsContext : ClaimsContext, ICertReportCheckClaimsContext
    {
        private int certReportId;

        private IClaimsCache claimsCache;
        private ICertReportsRepository certReportsRepository;

        public CertReportCheckClaimsContext(
            int certReportId,
            [KeyFilter(ClaimsCaches.CertReportCheck)]IClaimsCache claimsCache,
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
