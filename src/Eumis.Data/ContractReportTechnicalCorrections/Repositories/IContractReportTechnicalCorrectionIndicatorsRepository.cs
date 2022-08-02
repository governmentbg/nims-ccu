using System.Collections.Generic;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;

namespace Eumis.Data.ContractReportTechnicalCorrections.Repositories
{
    public interface IContractReportTechnicalCorrectionIndicatorsRepository : IAggregateRepository<ContractReportTechnicalCorrectionIndicator>
    {
        IList<ContractReportTechnicalCorrectionIndicator> FindContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId);

        ContractReportTechnicalCorrectionIndicator FindActualContractReportTechnicalCorrectionIndicator(int contractReportIndicatorId);

        IList<ContractReportTechnicalCorrectionIndicatorVO> GetContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId);

        ContractReportTechnicalCorrectionIndicator FindPreviousCorrection(int contractReportId, int contractIndicatorId);

        bool HasContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId);

        bool HasDraftContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId);
    }
}
