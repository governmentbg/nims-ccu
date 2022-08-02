using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories
{
    public interface IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository : IAggregateRepository<ContractReportRevalidationCertAuthorityFinancialCorrection>
    {
        int GetNextOrderNum(int contractId);

        int GetContractReportId(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        IList<ContractReportRevalidationCertAuthorityFinancialCorrectionVO> GetContractReportRevalidationCertAuthorityFinancialCorrections(int[] programmeIds, string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null);

        bool CanCreate(int contractReportId);

        bool IsIncludedInCertReport(int contractReportRevalidationCertAuthorityFinancialCorrectionId);
    }
}
