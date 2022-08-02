using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Indicators;

namespace Eumis.Data.ContractReportTechnicalCorrections.Repositories
{
    internal class ContractReportTechnicalCorrectionIndicatorsRepository : AggregateRepository<ContractReportTechnicalCorrectionIndicator>, IContractReportTechnicalCorrectionIndicatorsRepository
    {
        public ContractReportTechnicalCorrectionIndicatorsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportTechnicalCorrectionIndicator> FindContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId)
        {
            return this.Set()
                .Where(tci => tci.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId)
                .ToList();
        }

        public ContractReportTechnicalCorrectionIndicator FindActualContractReportTechnicalCorrectionIndicator(int contractReportIndicatorId)
        {
            return (from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                    join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                    where tci.ContractReportIndicatorId == contractReportIndicatorId && tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                    select tci).SingleOrDefault();
        }

        public ContractReportTechnicalCorrectionIndicator FindPreviousCorrection(int contractReportId, int contractIndicatorId)
        {
            return (from crtci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                    join cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>() on crtci.ContractReportIndicatorId equals cri.ContractReportIndicatorId
                    where cri.ContractReportId == contractReportId && cri.ContractIndicatorId == contractIndicatorId
                    orderby crtci.ContractReportTechnicalCorrectionId descending
                    select crtci).FirstOrDefault();
        }

        public IList<ContractReportTechnicalCorrectionIndicatorVO> GetContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId)
        {
            return (from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                    join cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>() on tci.ContractReportIndicatorId equals cri.ContractReportIndicatorId
                    join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
                    join i in this.unitOfWork.DbContext.Set<Indicator>() on ci.IndicatorId equals i.IndicatorId
                    where tci.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId
                    select new ContractReportTechnicalCorrectionIndicatorVO
                    {
                        ContractReportTechnicalCorrectionIndicatorId = tci.ContractReportTechnicalCorrectionIndicatorId,
                        ContractReportIndicatorId = tci.ContractReportIndicatorId,
                        ContractReportTechnicalCorrectionId = tci.ContractReportTechnicalCorrectionId,
                        ContractReportTechnicalId = tci.ContractReportTechnicalId,
                        ContractReportId = tci.ContractReportId,
                        ContractId = tci.ContractId,
                        Status = tci.Status,
                        IndicatorName = i.Name,
                        ApprovedPeriodAmountMen = cri.ApprovedPeriodAmountMen,
                        ApprovedPeriodAmountWomen = cri.ApprovedPeriodAmountWomen,
                        ApprovedPeriodAmountTotal = cri.ApprovedPeriodAmountTotal,
                        ApprovedCumulativeAmountMen = cri.ApprovedCumulativeAmountMen,
                        ApprovedCumulativeAmountWomen = cri.ApprovedCumulativeAmountWomen,
                        ApprovedCumulativeAmountTotal = cri.ApprovedCumulativeAmountTotal,
                        ApprovedResidueAmountMen = cri.ApprovedResidueAmountMen,
                        ApprovedResidueAmountWomen = cri.ApprovedResidueAmountWomen,
                        ApprovedResidueAmountTotal = cri.ApprovedResidueAmountTotal,
                        CorrectedApprovedPeriodAmountMen = tci.CorrectedApprovedPeriodAmountMen,
                        CorrectedApprovedPeriodAmountWomen = tci.CorrectedApprovedPeriodAmountWomen,
                        CorrectedApprovedPeriodAmountTotal = tci.CorrectedApprovedPeriodAmountTotal,
                        CorrectedApprovedCumulativeAmountMen = tci.CorrectedApprovedCumulativeAmountMen,
                        CorrectedApprovedCumulativeAmountWomen = tci.CorrectedApprovedCumulativeAmountWomen,
                        CorrectedApprovedCumulativeAmountTotal = tci.CorrectedApprovedCumulativeAmountTotal,
                        CorrectedApprovedResidueAmountMen = tci.CorrectedApprovedResidueAmountMen,
                        CorrectedApprovedResidueAmountWomen = tci.CorrectedApprovedResidueAmountWomen,
                        CorrectedApprovedResidueAmountTotal = tci.CorrectedApprovedResidueAmountTotal,
                        Version = tci.Version,
                    })
                    .ToList();
        }

        public bool HasContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId)
        {
            return (from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                    where tci.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId
                    select tci.ContractReportTechnicalCorrectionIndicatorId).Any();
        }

        public bool HasDraftContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId)
        {
            return (from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                    where
                        tci.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId &&
                        tci.Status == ContractReportTechnicalCorrectionIndicatorStatus.Draft
                    select tci.ContractReportTechnicalCorrectionIndicatorId).Any();
        }
    }
}
