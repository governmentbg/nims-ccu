using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.ContractReportIndicator
{
    public interface IContractReportIndicatorService
    {
        void CreateContractReportIndicators(ContractReportTechnical technical);

        Task CreateContractReportIndicatorsAsync(ContractReportTechnical technical, CancellationToken ct);

        void DeleteContractReportIndicatorsInDraft(ContractReportTechnical technical);

        void UpdateContractReportEndedIndicators(int contractReportId, int oldContractReportTechnicalId, int newContractReportTechnicalId);

        Eumis.Domain.Contracts.ContractReportIndicator UpdateContractReportIndicator(
            int contractReportIndicatorId,
            byte[] version,
            ContractReportIndicatorApproval? approval,
            string notes,
            decimal? approvedPeriodAmountMen,
            decimal? approvedPeriodAmountWomen,
            decimal? approvedPeriodAmountTotal,
            decimal? approvedCumulativeAmountMen,
            decimal? approvedCumulativeAmountWomen,
            decimal? approvedCumulativeAmountTotal,
            decimal? approvedResidueAmountMen,
            decimal? approvedResidueAmountWomen,
            decimal? approvedResidueAmountTotal,
            decimal? lastReportCumulativeAmountMen,
            decimal? lastReportCumulativeAmountWomen,
            decimal lastReportCumulativeAmountTotal);

        void ChangeContractReportIndicatorStatus(
            int contractReportIndicatorId,
            byte[] version,
            ContractReportIndicatorStatus status);

        IList<string> CanChangeContractReportIndicatorStatusToEnded(int contractReportIndicatorId);
    }
}
