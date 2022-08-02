using Autofac.Features.AttributeFilters;
using Eumis.Data.AnnualAccountReports.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization.ClaimsContexts.AnnualAccountReport
{
    internal class AnnualAccountReportClaimsContext : ClaimsContext, IAnnualAccountReportClaimsContext
    {
        private readonly int annualAccountReportId;

        private IClaimsCache claimsCache;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;

        public AnnualAccountReportClaimsContext(
            int annualAccountReportId,
            [KeyFilter(ClaimsCaches.AnnualAccountReport)]IClaimsCache claimsCache,
            IAnnualAccountReportsRepository annualAccountReportsRepository)
            : base(claimsCache)
        {
            this.annualAccountReportId = annualAccountReportId;
            this.claimsCache = claimsCache;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
        }

        public int AnnualAccountReportId
        {
            get
            {
                return this.annualAccountReportId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.annualAccountReportId,
                    new ClaimKey("ProgrammeId"),
                    () => this.annualAccountReportsRepository.GetProgrammeId(this.AnnualAccountReportId));
            }
        }
    }
}
