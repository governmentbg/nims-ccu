using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Common.Db;
using Eumis.Data.ContractReportIndicators.PortalViewObjects;
using Eumis.Data.ContractReportIndicators.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Indicators;

namespace Eumis.Data.ContractReportIndicators.Repositories
{
    internal class ContractReportIndicatorsRepository : AggregateRepository<ContractReportIndicator>, IContractReportIndicatorsRepository
    {
        public ContractReportIndicatorsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractReportIndicatorsVO> GetContractReportIndicators(int contractReportId)
        {
            return (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
                    join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
                    join i in this.unitOfWork.DbContext.Set<Indicator>() on ci.IndicatorId equals i.IndicatorId
                    where cri.ContractReportId == contractReportId
                    select new ContractReportIndicatorsVO
                    {
                        ContractReportIndicatorId = cri.ContractReportIndicatorId,
                        ContractReportTechnicalId = cri.ContractReportTechnicalId,
                        ContractReportId = cri.ContractReportId,
                        ContractId = cri.ContractId,
                        Gid = cri.Gid,
                        Status = cri.Status,

                        Approval = cri.Approval,
                        Notes = cri.Notes,
                        CheckedByUserId = cri.CheckedByUserId,
                        CheckedDate = cri.CheckedDate,

                        PeriodAmount = cri.PeriodAmountTotal,
                        CumulativeAmount = cri.CumulativeAmountTotal,
                        ResidueAmount = cri.ResidueAmountTotal,
                        LastReportCumulativeAmount = cri.LastReportCumulativeAmountTotal,
                        Comment = cri.Comment,

                        ApprovedPeriodAmount = cri.ApprovedPeriodAmountTotal,
                        ApprovedCumulativeAmount = cri.ApprovedCumulativeAmountTotal,
                        ApprovedResidueAmount = cri.ApprovedResidueAmountTotal,
                        IndicatorName = i.Name,
                    })
                .ToList();
        }

        public IList<ContractPhysicalExecutionIndicatorVO> GetContractPhysicalExecutionIndicatorsForProjectDossier(int contractId)
        {
            var technicalCorrectionIndicators = (
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                join crtci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>() on cr.ContractReportId equals crtci.ContractReportId
                where cr.ContractId == contractId && cr.Status == ContractReportStatus.Accepted && crtci.Status == ContractReportTechnicalCorrectionIndicatorStatus.Ended
                select new
                {
                    crtci.ContractReportIndicatorId,
                    crtci.CorrectedApprovedCumulativeAmountTotal,
                })
                .ToLookup(crtci => crtci.ContractReportIndicatorId);

            var lastAcceptedContractReport =
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                where cr.ContractId == contractId && cr.Status == ContractReportStatus.Accepted
                group new { cr.ContractReportId, cr.OrderNum } by cr.ContractId into g
                select g.OrderByDescending(t => t.OrderNum).FirstOrDefault().ContractReportId;

            var contractReportIndicators = (
                from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(r => lastAcceptedContractReport.Contains(r.ContractReportId))
                join cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>() on cr.ContractReportId equals cri.ContractReportId
                join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
                where cri.Status == ContractReportIndicatorStatus.Ended
                select new
                {
                    cri.ContractReportIndicatorId,
                    ci.Gid,
                    cri.CumulativeAmountTotal,
                    cri.ApprovedCumulativeAmountTotal,
                })
                .ToArray();

            var activeContractVersion = (
                from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                join c in this.unitOfWork.DbContext.Set<Contract>() on cv.ContractId equals c.ContractId
                where cv.ContractId == contractId && cv.Status == ContractVersionStatus.Active
                select new
                {
                    ContractVersionId = cv.ContractVersionXmlId,
                    ContractId = c.ContractId,
                    ContractRegNum = c.RegNumber,
                    Version = cv,
                })
                .Single();

            var contractDoc = activeContractVersion.Version.GetDocument();
            var contractIndicators = contractDoc
                .BFPContractIndicators
                .BFPContractIndicatorCollection
                .Select(ci => new
                {
                    Gid = new Guid(ci.gid),
                    ci.SelectedIndicator.Name,
                    ci.SelectedIndicator.MeasureName,
                    ci.BaseTotal,
                    ci.TargetTotal,
                })
                .ToArray();

            return (
                from cri in contractReportIndicators
                join ci in contractIndicators on cri.Gid equals ci.Gid
                select new ContractPhysicalExecutionIndicatorVO
                {
                    ContractVersionId = activeContractVersion.ContractVersionId,
                    ContractId = activeContractVersion.ContractId,
                    ContractRegNum = activeContractVersion.ContractRegNum,
                    IndicatorName = ci.Name,
                    MeasureName = ci.MeasureName,
                    BaseTotal = ci.BaseTotal,
                    TargetTotal = ci.TargetTotal,
                    CumulativeAmount = cri.CumulativeAmountTotal,
                    ApprovedCumulativeAmount = cri.ApprovedCumulativeAmountTotal,
                    CorrectedApprovedCumulativeAmountTotal = technicalCorrectionIndicators[cri.ContractReportIndicatorId].Any() ? (decimal?)technicalCorrectionIndicators[cri.ContractReportIndicatorId].First().CorrectedApprovedCumulativeAmountTotal : null,
                })
                .ToArray();
        }

        public IList<ContractReportIndicatorsVO> GetContractReportIndicatorsForContractReportTechnicalCorrection(int contractReportId, int contractReportTechnicalCorrectionId)
        {
            var correctedIndicators = from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                                      where tci.ContractReportTechnicalCorrectionId == contractReportTechnicalCorrectionId
                                      select tci.ContractReportIndicatorId;

            return (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
                    join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId
                    join i in this.unitOfWork.DbContext.Set<Indicator>() on ci.IndicatorId equals i.IndicatorId
                    where cri.ContractReportId == contractReportId && !correctedIndicators.Contains(cri.ContractReportIndicatorId)
                    select new ContractReportIndicatorsVO
                    {
                        ContractReportIndicatorId = cri.ContractReportIndicatorId,
                        ContractReportTechnicalId = cri.ContractReportTechnicalId,
                        ContractReportId = cri.ContractReportId,
                        ContractId = cri.ContractId,
                        Gid = cri.Gid,
                        Status = cri.Status,

                        Approval = cri.Approval,
                        Notes = cri.Notes,
                        CheckedByUserId = cri.CheckedByUserId,
                        CheckedDate = cri.CheckedDate,

                        PeriodAmount = cri.PeriodAmountTotal,
                        CumulativeAmount = cri.CumulativeAmountTotal,
                        ResidueAmount = cri.ResidueAmountTotal,
                        LastReportCumulativeAmount = cri.LastReportCumulativeAmountTotal,
                        Comment = cri.Comment,

                        ApprovedPeriodAmount = cri.ApprovedPeriodAmountTotal,
                        ApprovedCumulativeAmount = cri.ApprovedCumulativeAmountTotal,
                        ApprovedResidueAmount = cri.ApprovedResidueAmountTotal,
                        IndicatorName = i.Name,
                    })
                .ToList();
        }

        public IList<ContractReportIndicator> FindAll(int contractReportId)
        {
            return this.Set().Where(t => t.ContractReportId == contractReportId).ToList();
        }

        public bool HasDraftContractReportIndicators(int contractReportId)
        {
            return (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
                    where cri.ContractReportId == contractReportId && cri.Status == ContractReportIndicatorStatus.Draft
                    select cri.ContractReportIndicatorId).Any();
        }

        public IList<ContractReportIndicatorsPVO> GetPortalContractReportIndicators(int contractReportTechnicalId)
        {
            var actualContractReportTechnicalCorrectionIndicators =
                from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                where tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                select tci;

            return (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()

                    join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId

                    join tci in actualContractReportTechnicalCorrectionIndicators on cri.ContractReportIndicatorId equals tci.ContractReportIndicatorId into g0
                    from tci in g0.DefaultIfEmpty()

                    where cri.ContractReportTechnicalId == contractReportTechnicalId

                    select new ContractReportIndicatorsPVO
                    {
                        ContractIndicatorGid = ci.Gid,

                        ApprovedCumulativeAmountMen =
                            ((tci == null ? cri.ApprovedCumulativeAmountMen : tci.CorrectedApprovedCumulativeAmountMen) == null) ?
                            cri.CumulativeAmountMen :
                            tci == null ? cri.ApprovedCumulativeAmountMen : tci.CorrectedApprovedCumulativeAmountMen,
                        ApprovedCumulativeAmountWomen =
                            ((tci == null ? cri.ApprovedCumulativeAmountWomen : tci.CorrectedApprovedCumulativeAmountWomen) == null) ?
                            cri.CumulativeAmountWomen :
                            tci == null ? cri.ApprovedCumulativeAmountWomen : tci.CorrectedApprovedCumulativeAmountWomen,
                        ApprovedCumulativeAmountTotal =
                            ((tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal) == null) ?
                            cri.CumulativeAmountTotal :
                            tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal,
                    })
                .ToList();
        }

        public async Task<IList<ContractReportIndicatorsPVO>> GetPortalContractReportIndicatorsAsync(int contractReportTechnicalId,  CancellationToken ct)
        {
            var actualContractReportTechnicalCorrectionIndicators =
                from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                where tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                select tci;

            var result = (from cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>()
                          join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on cri.ContractIndicatorId equals ci.ContractIndicatorId

                          join tci in actualContractReportTechnicalCorrectionIndicators on cri.ContractReportIndicatorId equals tci.ContractReportIndicatorId into g0
                          from tci in g0.DefaultIfEmpty()

                          where cri.ContractReportTechnicalId == contractReportTechnicalId
                          select new ContractReportIndicatorsPVO
                          {
                              ContractIndicatorGid = ci.Gid,

                              ApprovedCumulativeAmountMen =
                            ((tci == null ? cri.ApprovedCumulativeAmountMen : tci.CorrectedApprovedCumulativeAmountMen) == null) ?
                            cri.CumulativeAmountMen :
                            tci == null ? cri.ApprovedCumulativeAmountMen : tci.CorrectedApprovedCumulativeAmountMen,
                              ApprovedCumulativeAmountWomen =
                            ((tci == null ? cri.ApprovedCumulativeAmountWomen : tci.CorrectedApprovedCumulativeAmountWomen) == null) ?
                            cri.CumulativeAmountWomen :
                            tci == null ? cri.ApprovedCumulativeAmountWomen : tci.CorrectedApprovedCumulativeAmountWomen,
                              ApprovedCumulativeAmountTotal =
                            ((tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal) == null) ?
                            cri.CumulativeAmountTotal :
                            tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal,
                          })
                          .ToListAsync(ct);

            return await result;
        }

        public Guid GetContractIndicatorGid(int contractIndicatorId)
        {
            return this.unitOfWork.DbContext.Set<ContractIndicator>()
                .Single(ci => ci.ContractIndicatorId == contractIndicatorId)
                .Gid;
        }
    }
}
