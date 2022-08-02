using System;
using System.Collections.Generic;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;

namespace Eumis.Data.ContractReportTechnicalCorrections.Repositories
{
    public interface IContractReportTechnicalCorrectionsRepository : IAggregateRepository<ContractReportTechnicalCorrection>
    {
        ContractReportTechnicalCorrection FindEndedContractReportTechnicalCorrection(int contractReportId);

        ContractReportTechnicalCorrection FindLastArchivedContractReportTechnicalCorrection(int contractReportId);

        int GetNextOrderNum(int contractId);

        int GetContractReportId(int contractReportTechnicalCorrectionId);

        IList<ContractReportTechnicalCorrectionVO> GetContractReportTechnicalCorrections(int[] programmeIds, int userId, string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null);

        bool ExistsCorrectionForContractReport(int contractReportId);

        bool ExistsDraftCorrectionForContractReport(int contractReportId);

        bool ExistsEndedCorrectionForContractReport(int contractReportId);

        IList<ContractReportTechnicalCorrectionVO> GetContractReportTechnicalCorrectionsForProjectDossier(int contractId);
    }
}
