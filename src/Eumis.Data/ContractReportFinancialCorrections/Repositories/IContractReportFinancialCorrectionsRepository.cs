using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialCorrections.Repositories
{
    public interface IContractReportFinancialCorrectionsRepository : IAggregateRepository<ContractReportFinancialCorrection>
    {
        int GetNextOrderNum(int contractId);

        int GetContractReportId(int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionVO> GetContractReportFinancialCorrections(int[] programmeIds, int userId, string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null);

        bool CanCreate(int contractReportId);

        IList<ContractReportFinancialCorrectionVO> GetFinancialCorrectionContractReportFinancialCorrections(int financialCorrectionId);

        bool IsIncludedInCertReport(int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionVO> GetContractReportFinancialCorrectionsForProjectDossier(int contractId);

        IList<ContractReportCertifiedAmountFinancialCorrectionVO> GetContractReportCertifiedAmountFinancialCorrectionsForProjectDossier(int contractId);
    }
}
