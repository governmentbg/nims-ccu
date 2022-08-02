using Eumis.Domain.Contracts;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialCSDs.Repositories
{
    public interface IContractReportFinancialCSDsRepository : IAggregateRepository<ContractReportFinancialCSD>
    {
        IList<int> GetContractReportFinancialCSDsInDraft(int contractReportFinancialId);

        IList<ContractReportFinancialCSD> FindAll(int contractReportFinancialId);

        IList<ContractReportFinancialCSDFile> GetContractReportFinancialCSDFiles(int contractReportFinancialId);
    }
}
