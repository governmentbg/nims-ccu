using System.Collections.Generic;
using Eumis.Data.EuReimbursedAmounts.ViewObjects;
using Eumis.Domain.EuReimbursedAmounts;

namespace Eumis.Data.EuReimbursedAmounts.Repositories
{
    public interface IEuReimbursedAmountsRepository : IAggregateRepository<EuReimbursedAmount>
    {
        IList<EuReimbursedAmountVO> GetEuReimbursedAmounts(int[] programmeIds, EuReimbursedAmountStatus? status = null);

        EuReimbursedAmountInfoVO GetInfo(int euReimbursedAmountId);

        IList<EuReimbursedAmountCertReportVO> GetCertReports(int euReimbursedAmountId);

        IList<EuReimbursedAmountCertReportVO> GetNotIncludedCertReports(int euReimbursedAmountId);

        int GetProgrammeId(int euReimbursedAmountId);
    }
}
