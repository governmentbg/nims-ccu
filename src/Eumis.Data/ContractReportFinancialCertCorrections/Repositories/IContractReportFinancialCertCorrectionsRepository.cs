using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialCertCorrections.Repositories
{
    public interface IContractReportFinancialCertCorrectionsRepository : IAggregateRepository<ContractReportFinancialCertCorrection>
    {
        int GetNextOrderNum(int contractId);

        int GetContractReportId(int contractReportFinancialCertCorrectionId);

        IList<ContractReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrections(int[] programmeIds, string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null);

        bool CanCreate(int contractReportId);

        bool IsIncludedInCertReport(int contractReportFinancialCertCorrectionId);

        IList<ContractReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrectionsForProjectDossier(int contractId);
    }
}
