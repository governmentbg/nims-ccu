using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization.ClaimsContexts.AnnualAccountReport
{
    internal delegate IAnnualAccountReportClaimsContext AnnualAccountReportClaimsContextFactory(int annualAccountReportId);

    internal interface IAnnualAccountReportClaimsContext
    {
        int AnnualAccountReportId { get; }

        int ProgrammeId { get; }
    }
}
