using Autofac.Extras.Attributed;
using Eumis.Public.Common.Json;
using Eumis.Public.Data.Contracts.ViewObjects;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.Indicators;
using Eumis.Public.Domain.Entities.Umis.Measures;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using Eumis.Public.Model.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.Contracts.Repositories
{
    internal class ContractsRepository : Repository, IContractsRepository
    {
        public ContractsRepository([WithKey(DbKey.Umis)]IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ContractBasicDataVO GetContractBasicData(int contractId, bool isHistoric)
        {
            IQueryable<Contract> contracts =
                from c in this.unitOfWork.DbContext.Set<Contract>()
                where c.ContractId == contractId
                select c;

            var query =
                from c in contracts
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                join p in this.unitOfWork.DbContext.Set<Programme>() on c.ProgrammeId equals p.MapNodeId
                join pr in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals pr.ProcedureId
                where c.ContractStatus == ContractStatus.Entered && ps.IsPrimary
                select new
                {
                    c.ContractId,
                    c.Name,
                    c.NameEN,
                    c.CompanyName,
                    c.CompanyNameAlt,
                    c.CompanyUin,
                    c.CompanyUinType,
                    c.ContractDate,
                    c.StartDate,
                    c.CompletionDate,
                    c.RegNumber,
                    c.ExecutionStatus,
                    c.ProcedureId,

                    c.ProgrammeId,
                    ProgrammeName = p.PortalName,
                    ProgrammeNameEN = p.PortalNameAlt,

                    ProcedureCode = pr.Code,
                    ProcedureName = pr.Name,
                    ProcedureNameAlt = pr.NameAlt,
                    ProcedureGid = pr.Gid,
                    pr.ProcedureStatus,
                };

            var contract = query.Single();
            var locations = this.unitOfWork.DbContext.Set<ContractLocation>().Where(e => e.ContractId == contractId).AsEnumerable();

            IEnumerable<string> funds = this.unitOfWork.DbContext.Set<ProcedureShare>()
                .Where(e => e.ProcedureId == contract.ProcedureId && e.ProgrammeId == contract.ProgrammeId)
                .Select(e => e.FinanceSource).Distinct().AsEnumerable().Select(e => e.GetEnumDescription());

            return new ContractBasicDataVO()
            {
                ContractId = contractId,
                IsHistoric = isHistoric,

                RegNumber = contract.RegNumber,
                Name = contract.Name,
                NameEN = contract.NameEN,
                CompanyName = contract.CompanyName,
                CompanyNameAlt = contract.CompanyNameAlt,
                CompanyUin = contract.CompanyUin,
                CompanyUinType = contract.CompanyUinType,

                Funds = funds,

                ProgrammeId = contract.ProgrammeId,
                ProgrammeName = contract.ProgrammeName,
                ProgrammeNameEN = contract.ProgrammeNameEN,
                ContractDate = contract.ContractDate,
                StartDate = contract.StartDate,
                CompletionDate = contract.CompletionDate,
                ExecutionStatus = contract.ExecutionStatus,

                NutsFullPathNames = locations.Where(e => e.FullPathName != null && e.FullPathName != string.Empty).Select(e => e.FullPathName).Distinct(),
                NutsFullPathNamesEN = locations.Where(e => e.FullPathNameAlt != null && e.FullPathNameAlt != string.Empty).Select(e => e.FullPathNameAlt).Distinct(),

                ProcedureCode = contract.ProcedureCode,
                ProcedureName = contract.ProcedureName,
                ProcedureNameEn = contract.ProcedureNameAlt,
                ProcedureGid = contract.ProcedureGid,
                ProcedureStatus = contract.ProcedureStatus,
            };
        }

        public ContractActivitiesVO GetContractActivities(int contractId, bool isHistoric)
        {
            IQueryable<Contract> contracts =
                from c in this.unitOfWork.DbContext.Set<Contract>()
                where c.ContractId == contractId
                select c;

            var query =
                from c in contracts
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId
                where c.ContractStatus == ContractStatus.Entered && ps.IsPrimary
                select new
                {
                    c.ContractId,
                    c.Description,
                    c.DescriptionEN,
                };

            var contract = query.Single();

            var acceptedcsdbi =
                from csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on csdbi.ContractReportId equals cr.ContractReportId
                where cr.Status == ContractReportStatus.Accepted
                select csdbi;

            var activities =
                from act in this.unitOfWork.DbContext.Set<ContractActivity>()

                join csdbi in acceptedcsdbi.Where(e => e.ContractId == contractId) on act.Gid equals csdbi.ContractActivityGid into gcsdbi
                from csdbi in gcsdbi.DefaultIfEmpty()

                where act.ContractId == contractId

                group new
                {
                    CSDTotalAmount = (decimal?)csdbi.TotalAmount,
                }
                by new
                {
                    act.ContractActivityId,
                    act.Code,
                    act.Name,
                    act.Amount,
                }
                into g
                select new
                {
                    g.Key.Code,
                    g.Key.Name,
                    g.Key.Amount,
                    TotalReportedAmount = g.Sum(p => p.CSDTotalAmount),
                };

            return new ContractActivitiesVO()
            {
                ContractId = contractId,
                IsHistoric = isHistoric,

                Description = contract.Description,
                DescriptionEN = contract.DescriptionEN,
                Activities = activities.Select(e => new ContractActivityVO
                {
                    Title = e.Code + ": " + e.Name,
                    TotalAmount = e.Amount,
                    TotalReportedAmount = e.TotalReportedAmount ?? 0,
                }).ToList(),
            };
        }

        public ContractProcurementsVO GetContractProcurements(int contractId, bool isHistoric)
        {
            var offers =
                from cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>()

                join p in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on cpp.ContractProcurementPlanId equals p.ContractProcurementPlanId into gp
                from p in gp.DefaultIfEmpty()

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on p.ContractContractId equals cc.ContractContractId into gcc
                from cc in gcc.DefaultIfEmpty()

                join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into gcctor
                from cctor in gcctor.DefaultIfEmpty()

                where cpp.ContractId == contractId
                group new
                {
                    HasContractDifferentiatedPosition = p != null,
                    PositionName = p.Name,
                    ContractorName = cctor.Name,
                    ContractorNameAlt = cctor.NameAlt,
                    ContractorUinType = cctor.UinType,
                    TotalFundedValue = (decimal?)cc.TotalFundedValue,
                }
                by new
                {
                    ProcurementPlanName = cpp.Name,
                    cpp.Amount,
                }
                into g
                select new
                {
                    Positions = g.Where(p => p.HasContractDifferentiatedPosition).Select(p => new
                    {
                        p.PositionName,
                        p.ContractorName,
                        p.ContractorNameAlt,
                        p.ContractorUinType,
                        p.TotalFundedValue,
                    }),
                    g.Key.ProcurementPlanName,
                    g.Key.Amount,
                };

            return new ContractProcurementsVO()
            {
                ContractId = contractId,
                IsHistoric = isHistoric,

                Offers = offers.Select(e => new OfferVO()
                {
                    ProcurementPlanName = e.ProcurementPlanName,
                    Amount = e.Amount,
                    ContractDifferentiatedPositions = e.Positions.Select(p => new ContractDifferentiatedPositionVO
                    {
                        Name = p.PositionName,
                        ContractorName = p.ContractorName,
                        ContractorNameAlt = p.ContractorNameAlt,
                        ContractorUinType = p.ContractorUinType,
                        TotalFundedValue = p.TotalFundedValue,
                    }).ToList(),
                }).ToList(),
            };
        }

        public ContractParticipantsVO GetContractParticipants(int contractId, bool isHistoric)
        {
            var acceptedcsdbi =
                from csdbi in this.unitOfWork.DbContext.Set<ContractReportFinancialCSDBudgetItem>()
                join cr in this.unitOfWork.DbContext.Set<ContractReport>() on csdbi.ContractReportId equals cr.ContractReportId
                where cr.Status == ContractReportStatus.Accepted
                select csdbi;

            var partners =
                from p in this.unitOfWork.DbContext.Set<ContractPartner>().Where(e => e.ContractId == contractId)

                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>()
                .Where(e => e.ContractId == contractId && e.CompanyType == CostSupportingDocumentCompanyType.Partner) on p.Gid equals csd.CompanyGid into gcsd
                from csd in gcsd.DefaultIfEmpty()

                join csdbi in acceptedcsdbi on csd.ContractReportFinancialCSDId equals csdbi.ContractReportFinancialCSDId into gcsdbi
                from csdbi in gcsdbi.DefaultIfEmpty()

                group new
                {
                    CsdbiTotalAmount = (decimal?)csdbi.TotalAmount,
                }
                by new
                {
                    p.Gid,
                    p.Name,
                    p.NameAlt,
                    p.Uin,
                    p.UinType,
                    p.FinancialContribution,
                }
                into g
                select new
                {
                    g.Key.Gid,
                    g.Key.Name,
                    g.Key.NameAlt,
                    g.Key.Uin,
                    g.Key.UinType,
                    g.Key.FinancialContribution,
                    CsdbiTotalAmount = g.Sum(p => p.CsdbiTotalAmount),
                };

            var contractors1 =
                from cctor in this.unitOfWork.DbContext.Set<ContractContractor>()
                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on cctor.ContractContractorId equals cc.ContractContractorId
                where cctor.ContractId == contractId
                select new
                {
                    cctor.Gid,
                    cctor.Name,
                    cctor.NameAlt,
                    cctor.Uin,
                    cctor.UinType,
                    ContractTotalFundedValue = cc.TotalFundedValue,
                    CsdbiTotalAmount = 0m,
                };

            var contractors2 =
                from cctor in this.unitOfWork.DbContext.Set<ContractContractor>()
                join csd in this.unitOfWork.DbContext.Set<ContractReportFinancialCSD>() on cctor.Gid equals csd.CompanyGid
                join csdbi in acceptedcsdbi on csd.ContractReportFinancialCSDId equals csdbi.ContractReportFinancialCSDId
                where csd.ContractId == contractId && csd.CompanyType == CostSupportingDocumentCompanyType.Contractor
                select new
                {
                    cctor.Gid,
                    cctor.Name,
                    cctor.NameAlt,
                    cctor.Uin,
                    cctor.UinType,
                    ContractTotalFundedValue = 0m,
                    CsdbiTotalAmount = csdbi.TotalAmount,
                };

            var contractors =
                from c in contractors1.Concat(contractors2)
                group new
                {
                    c.ContractTotalFundedValue,
                    c.CsdbiTotalAmount,
                }
                by new
                {
                    c.Gid,
                    c.Name,
                    c.NameAlt,
                    c.Uin,
                    c.UinType,
                }
                into g
                select new
                {
                    g.Key.Gid,
                    g.Key.Name,
                    g.Key.NameAlt,
                    g.Key.Uin,
                    g.Key.UinType,
                    ContractTotalFundedValue = g.Sum(p => p.ContractTotalFundedValue),
                    CsdbiTotalAmount = g.Sum(p => p.CsdbiTotalAmount),
                };

            var subcontractors =
                from cs in this.unitOfWork.DbContext.Set<ContractSubcontract>()
                join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cs.ContractContractorId equals ccr.ContractContractorId

                where ccr.ContractId == contractId

                group new
                {
                    cs.Amount,
                }

                by new
                {
                    cs.Type,
                    ccr.Name,
                    ccr.NameAlt,
                    ccr.Uin,
                    ccr.UinType,
                }

                into g

                select new
                {
                    g.Key.Type,
                    g.Key.Name,
                    g.Key.NameAlt,
                    g.Key.Uin,
                    g.Key.UinType,
                    TotalAmount = g.Sum(p => p.Amount),
                };

            return new ContractParticipantsVO()
            {
                ContractId = contractId,
                IsHistoric = isHistoric,

                Partners = partners.Select(e => new ContractPartnerVO
                {
                    Name = e.Name,
                    NameAlt = e.NameAlt,
                    Uin = e.Uin,
                    UinType = e.UinType,
                    TotalFinancialContribution = e.FinancialContribution,
                    TotalReportedAmount = e.CsdbiTotalAmount ?? 0,
                }).ToList(),

                Contractors = contractors.Select(e => new ContractContractorVO()
                {
                    Name = e.Name,
                    NameAlt = e.NameAlt,
                    Uin = e.Uin,
                    UinType = e.UinType,
                    TotalContractedAmount = e.ContractTotalFundedValue,
                    TotalReportedAmount = e.CsdbiTotalAmount,
                }).ToList(),

                Subcontractors = subcontractors.Where(e => e.Type == ContractSubcontractType.Subcontractor).Select(e => new ContractSubcontractorVO()
                {
                    Name = e.Name,
                    NameAlt = e.NameAlt,
                    Uin = e.Uin,
                    UinType = e.UinType,
                    TotalContractedAmount = e.TotalAmount,
                }).ToList(),

                Members = subcontractors.Where(e => e.Type == ContractSubcontractType.Member).Select(e => new ContractSubcontractorVO()
                {
                    Name = e.Name,
                    NameAlt = e.NameAlt,
                    Uin = e.Uin,
                    UinType = e.UinType,
                    TotalContractedAmount = e.TotalAmount,
                }).ToList(),
            };
        }

        public ContractFinancialInformationVO GetContractFinancialInformation(int contractId, bool isHistoric)
        {
            IQueryable<Contract> contracts =
                from c in this.unitOfWork.DbContext.Set<Contract>()
                where c.ContractId == contractId
                select c;

            var reimbursedAmounts = from c in contracts
                                    join ra in this.unitOfWork.DbContext.Set<ReimbursedAmount>().Where(cra => cra.Status == ReimbursedAmountStatus.Entered && UmisRepository.ReportsReimbursements.Contains(cra.Reimbursement)) on c.ContractId equals ra.ContractId into j1
                                    from ra in j1.DefaultIfEmpty()
                                    group new
                                    {
                                        EuAmount = ra.PrincipalBfp.EuAmount,
                                        BgAmount = ra.PrincipalBfp.BgAmount,
                                    }
                                   by c.ContractId into g
                                    select new
                                    {
                                        ContractId = g.Key,
                                        EuAmount = g.Sum(i => i.EuAmount),
                                        BgAmount = g.Sum(i => i.BgAmount),
                                    };

            var paidAmounts = from c in contracts
                              join pa in this.unitOfWork.DbContext.Set<ActuallyPaidAmount>().Where(pa => pa.Status == ActuallyPaidAmountStatus.Entered) on c.ContractId equals pa.ContractId into j1
                              from pa in j1.DefaultIfEmpty()
                              group new
                              {
                                  PaidEuAmount = pa.PaidBfpEuAmount,
                                  PaidBgAmount = pa.PaidBfpBgAmount,
                              }
                               by c.ContractId into g
                              select new
                              {
                                  ContractId = g.Key,
                                  PaidEuAmount = g.Sum(i => i.PaidEuAmount),
                                  PaidBgAmount = g.Sum(i => i.PaidBgAmount),
                              };

            var query =
                from c in contracts

                join cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(cba1 => cba1.IsActive) on c.ContractId equals cba.ContractId into j1
                from cba in j1.DefaultIfEmpty()

                join pa in paidAmounts on c.ContractId equals pa.ContractId into j2
                from pa in j2.DefaultIfEmpty()

                join ra in reimbursedAmounts on c.ContractId equals ra.ContractId into j3
                from ra in j3.DefaultIfEmpty()

                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on c.ProcedureId equals ps.ProcedureId

                where c.ContractStatus == ContractStatus.Entered && ps.IsPrimary
                group new
                {
                    ContractedEuAmount = (decimal?)cba.CurrentEuAmount,
                    ContractedBgAmount = (decimal?)cba.CurrentBgAmount,
                    ContractedSelfAmount = (decimal?)cba.CurrentSelfAmount,
                }
                by new
                {
                    pa.PaidEuAmount,
                    pa.PaidBgAmount,
                    ReimbursedEuAmount = ra.EuAmount,
                    ReimbursedBgAmount = ra.BgAmount,
                    ps.EuAmount,
                    ps.BgAmount,
                }
                into g
                select new
                {
                    ContractedEuAmount = g.Sum(i => i.ContractedEuAmount),
                    ContractedBgAmount = g.Sum(i => i.ContractedBgAmount),
                    ContractedSelfAmount = g.Sum(i => i.ContractedSelfAmount),
                    PaidEuAmount = (g.Key.PaidEuAmount ?? 0m) - (g.Key.ReimbursedEuAmount ?? 0m),
                    PaidBgAmount = (g.Key.PaidBgAmount ?? 0m) - (g.Key.ReimbursedBgAmount ?? 0m),

                    EuAmount = g.Key.EuAmount,
                    BgAmount = g.Key.BgAmount,
                };

            var contract = query.Single();

            var financialCorrections =
                from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                join fcv in this.unitOfWork.DbContext.Set<FinancialCorrectionVersion>() on fc.FinancialCorrectionId equals fcv.FinancialCorrectionId

                join ir in this.unitOfWork.DbContext.Set<FinancialCorrectionImposingReason>() on fcv.FinancialCorrectionImposingReasonId equals ir.FinancialCorrectionImposingReasonId into gir
                from ir in gir.DefaultIfEmpty()

                join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into gcc
                from cc in gcc.DefaultIfEmpty()

                join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into gcctor
                from cctor in gcctor.DefaultIfEmpty()

                where fc.ContractId == contractId && fc.Status == FinancialCorrectionStatus.Entered && fcv.Status == FinancialCorrectionVersionStatus.Actual
                select new
                {
                    ImposingReasonName = ir.Name,
                    fcv.Percent,
                    fcv.BfpAmount,
                    fcv.SelfAmount,
                    fcv.TotalAmount,
                    ContractorName = cctor.Name,
                };

            return new ContractFinancialInformationVO()
            {
                ContractId = contractId,
                IsHistoric = isHistoric,

                ContractedEuAmount = contract.ContractedEuAmount ?? 0m,
                ContractedBgAmount = contract.ContractedBgAmount ?? 0m,
                ContractedSelfAmount = contract.ContractedSelfAmount ?? 0m,
                PaidEuAmount = contract.PaidEuAmount,
                PaidBgAmount = contract.PaidBgAmount,

                FinancialCorrections = financialCorrections.Select(fc => new FinancialCorrectionVO
                {
                    ImposingReason = fc.ImposingReasonName,
                    Percent = fc.Percent,
                    BfpAmount = fc.BfpAmount,
                    SelfAmount = fc.SelfAmount,
                    TotalAmount = fc.TotalAmount,
                    ContractorName = fc.ContractorName,
                }),

                ProcedureShareBgAmount = contract.BgAmount,
                ProcedureShareEuAmount = contract.EuAmount,
            };
        }

        public ContractIndicatorsVO GetContractIndicators(int contractId, bool isHistoric)
        {
            var lastContractReportTechnicalId =
                (from cr in this.unitOfWork.DbContext.Set<ContractReport>().Where(p => p.ContractId == contractId && p.Status == ContractReportStatus.Accepted)
                 join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>().Where(p => p.Status == ContractReportTechnicalStatus.Actual) on cr.ContractReportId equals crt.ContractReportId
                 orderby crt.VersionNum descending
                 select (int?)crt.ContractReportTechnicalId).FirstOrDefault();

            var actualContractReportTechnicalCorrectionIndicators =
                from tci in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrectionIndicator>()
                join tc in this.unitOfWork.DbContext.Set<ContractReportTechnicalCorrection>() on tci.ContractReportTechnicalCorrectionId equals tc.ContractReportTechnicalCorrectionId
                where tc.Status == ContractReportTechnicalCorrectionStatus.Ended
                select tci;

            var indicators =
                (from i in this.unitOfWork.DbContext.Set<Indicator>()
                 join m in this.unitOfWork.DbContext.Set<Measure>() on i.MeasureId equals m.MeasureId
                 join ci in this.unitOfWork.DbContext.Set<ContractIndicator>() on i.IndicatorId equals ci.IndicatorId
                 join cri in this.unitOfWork.DbContext.Set<ContractReportIndicator>().Where(p => p.ContractReportTechnicalId == lastContractReportTechnicalId) on ci.ContractIndicatorId equals cri.ContractIndicatorId into j1
                 from cri in j1.DefaultIfEmpty()

                 join tci in actualContractReportTechnicalCorrectionIndicators on cri.ContractReportIndicatorId equals tci.ContractReportIndicatorId into g0
                 from tci in g0.DefaultIfEmpty()

                 where ci.ContractId == contractId

                 select new
                 {
                     Name = i.Name,
                     NameAlt = i.NameAlt,
                     MeasureShortName = m.ShortName,
                     MeasureNameAlt = m.NameAlt,
                     ci.BaseTotalValue,
                     TargetAmount = (decimal?)ci.TargetTotalValue,
                     ApprovedCumulativeAmountTotal = tci == null ? cri.ApprovedCumulativeAmountTotal : tci.CorrectedApprovedCumulativeAmountTotal,
                 }).AsEnumerable();

            return new ContractIndicatorsVO()
            {
                ContractId = contractId,
                IsHistoric = isHistoric,

                Indicators = indicators.Select(e => new ContractIndicatorVO()
                {
                    Name = e.Name,
                    NameAlt = e.NameAlt,
                    MeasureShortName = e.MeasureShortName,
                    MeasureNameAlt = e.MeasureNameAlt,
                    BaseTotalValue = e.BaseTotalValue ?? 0,
                    TargetAmount = e.TargetAmount ?? 0m,
                    CumulativeAmountTotal = e.ApprovedCumulativeAmountTotal ?? 0m,
                }),
            };
        }
    }
}
