using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialRevalidations.Repositories
{
    public interface IContractReportFinancialRevalidationsRepository : IAggregateRepository<ContractReportFinancialRevalidation>
    {
        int GetNextOrderNum(int contractId);

        int GetContractReportId(int contractReportFinancialRevalidationId);

        IList<ContractReportFinancialRevalidationVO> GetContractReportFinancialRevalidations(int[] programmeIds, string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null);

        bool CanCreate(int contractReportId);

        bool IsIncludedInCertReport(int contractReportFinancialRevalidationId);

        IList<ContractReportFinancialRevalidationVO> GetContractReportFinancialRevalidationsForProjectDossier(int contractId);
    }
}
