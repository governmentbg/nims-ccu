using Eumis.Data.ContractReportIndicators.PortalViewObjects;
using Eumis.Data.ContractReportIndicators.ViewObjects;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportIndicators.Repositories
{
    public interface IContractReportIndicatorsRepository : IAggregateRepository<ContractReportIndicator>
    {
        IList<ContractReportIndicatorsVO> GetContractReportIndicators(int contractReportId);

        IList<ContractPhysicalExecutionIndicatorVO> GetContractPhysicalExecutionIndicatorsForProjectDossier(int contractId);

        Task<IList<ContractReportIndicatorsPVO>> GetPortalContractReportIndicatorsAsync(int contractReportTechnicalId, CancellationToken ct);

        IList<ContractReportIndicatorsVO> GetContractReportIndicatorsForContractReportTechnicalCorrection(int contractReportId, int contractReportTechnicalCorrectionId);

        IList<ContractReportIndicator> FindAll(int contractReportId);

        bool HasDraftContractReportIndicators(int contractReportId);

        IList<ContractReportIndicatorsPVO> GetPortalContractReportIndicators(int contractReportTechnicalId);

        Guid GetContractIndicatorGid(int contractIndicatorId);
    }
}
