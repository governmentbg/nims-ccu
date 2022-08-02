using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories
{
    public interface IContractReportCertAuthorityFinancialCorrectionsRepository : IAggregateRepository<ContractReportCertAuthorityFinancialCorrection>
    {
        int GetNextOrderNum(int contractId);

        int GetContractReportId(int contractReportCertAuthorityFinancialCorrectionId);

        IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCertAuthorityFinancialCorrections(int[] programmeIds, string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null);

        bool CanCreate(int contractReportId);

        bool IsIncludedInCertReport(int contractReportCertAuthorityFinancialCorrectionId);

        IList<ContractReportCertAuthorityFinancialCorrectionVO> GetContractReportCertAuthorityFinancialCorrectionsForProjectDossier(int contractId);
    }
}
