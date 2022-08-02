using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.Irregularities;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularitiesRepository : AggregateRepository<Irregularity>, IIrregularitiesRepository
    {
        public IrregularitiesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Irregularity, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Irregularity, object>>[]
                {
                    i => i.FinancialCorrections,
                    i => i.Documents,
                };
            }
        }

        public IList<IrregularityVO> GetIrregularities(int[] programmeIds, int userId)
        {
            var predicate = PredicateBuilder.True<Irregularity>()
                .And(irr => programmeIds.Contains(irr.ProgrammeId));

            var externalVerificatorIrregularities = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                                    join ir in this.unitOfWork.DbContext.Set<Irregularity>() on cu.ContractId equals ir.ContractId
                                                    select ir;

            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>().Where(predicate).Union(externalVerificatorIrregularities)
                    join irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on irr.IrregularitySignalId equals irrs.IrregularitySignalId

                    join c in this.unitOfWork.DbContext.Set<Contract>() on irr.ContractId equals c.ContractId into g0
                    from c in g0.DefaultIfEmpty()

                    join proj in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals proj.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on irr.ProgrammeId equals pr.MapNodeId
                    orderby irr.CreateDate descending
                    select new
                    {
                        irr.IrregularityId,
                        SignalNum = irrs.RegNumber,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        irr.Status,
                        irr.RegNumber,
                        CompanyName = c.CompanyName ?? proj.CompanyName,
                        CompanyUinType = ((UinType?)c.CompanyUinType) ?? proj.CompanyUinType,
                        CompanyUin = c.CompanyUin ?? proj.CompanyUin,
                    })
                    .Distinct()
                    .ToList()
                    .Select(o => new IrregularityVO
                    {
                        IrregularityId = o.IrregularityId,
                        SignalNum = o.SignalNum,
                        ProgrammeName = o.ProgrammeName,
                        ContractRegNumber = o.ContractRegNumber,
                        Status = o.Status,
                        RegNumber = o.RegNumber,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IrrByQuarterReportVO GetIrrByQuarterReport(Year year, Quarter quarter, int programmeId)
        {
            var newIrregularities = this.GetIrrByQuarterReportQuery(
                    programmeId,
                    pv => pv.OrderNum == 1 && pv.ReportYear == year && pv.ReportQuarter == quarter);

            var subsequentVersions = this.GetIrrByQuarterReportQuery(
                    programmeId,
                    pv => pv.OrderNum != 1 && pv.ReportYear == year && pv.ReportQuarter == quarter);

            var previousVersions = this.GetIrrByQuarterReportQuery(
                    programmeId,
                    pv => pv.ReportYear < year || (pv.ReportYear == year && pv.ReportQuarter < quarter));

            return new IrrByQuarterReportVO
            {
                NewReportedIrregularities = newIrregularities.Where(i => i.ShouldReportToOlaf).ToList(),
                NewNotReportedIrregularities = newIrregularities.Where(i => !i.ShouldReportToOlaf).ToList(),
                SubsequentReportedVersions = subsequentVersions.Where(i => i.ShouldReportToOlaf).ToList(),
                SubsequentNotReportedVersions = subsequentVersions.Where(i => !i.ShouldReportToOlaf).ToList(),
                PreviousReportedVersions = previousVersions.Where(i => i.ShouldReportToOlaf).ToList(),
                PreviousNotReportedVersions = previousVersions.Where(i => !i.ShouldReportToOlaf).ToList(),
            };
        }

        private IList<ReportIrregularityVersionVO> GetIrrByQuarterReportQuery(int programmeId, Expression<Func<IrregularityVersion, bool>> predicate)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>().Where(predicate)
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on iv.IrregularityId equals irr.IrregularityId
                    where irr.ProgrammeId == programmeId && irr.Status == IrregularityStatus.Entered && iv.Status != IrregularityVersionStatus.Draft
                    select new ReportIrregularityVersionVO
                    {
                        IrregularityId = iv.IrregularityId,
                        IrregularityVersionId = iv.IrregularityVersionId,
                        IrregularityRegNumber = irr.RegNumber,
                        OrderNum = iv.OrderNum,
                        ReportQuarter = iv.ReportQuarter,
                        ReportYear = iv.ReportYear,
                        ShouldReportToOlaf = iv.ShouldReportToOlaf,
                        ReasonNotReportingToOlaf = iv.ReasonNotReportingToOlaf,
                    }).ToList();
        }

        public IList<IrrReportItemVO> GetIrrReport(
            int[] programmeIds,
            Year? reportYear = null,
            Quarter? reportQuarter = null,
            IrregularityCaseState? caseState = null)
        {
            var versionPredicate = PredicateBuilder.True<IrregularityVersion>()
                .AndEquals(iv => iv.ReportYear, reportYear)
                .AndEquals(iv => iv.ReportQuarter, reportQuarter)
                .AndEquals(iv => iv.CaseState, caseState)
                .And(iv => iv.Status == IrregularityVersionStatus.Active);

            var irrPredicate = PredicateBuilder.True<Irregularity>()
                .And(i => i.Status == IrregularityStatus.Entered)
                .And(i => programmeIds.Contains(i.ProgrammeId));

            return (from i in this.unitOfWork.DbContext.Set<Irregularity>().Where(irrPredicate)
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on i.ProgrammeId equals pr.MapNodeId
                    join irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on i.IrregularitySignalId equals irrs.IrregularitySignalId
                    join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                    join iv in this.unitOfWork.DbContext.Set<IrregularityVersion>().Where(versionPredicate) on i.IrregularityId equals iv.IrregularityId
                    join fs in this.unitOfWork.DbContext.Set<IrregularityFinancialStatus>() on iv.FinancialStatusId equals fs.IrregularityFinancialStatusId into g0
                    from fs in g0.DefaultIfEmpty()
                    join ic in this.unitOfWork.DbContext.Set<IrregularityCategory>() on iv.IrregularityCategoryId equals ic.IrregularityCategoryId into g1
                    from ic in g1.DefaultIfEmpty()
                    join it in this.unitOfWork.DbContext.Set<IrregularityType>() on iv.IrregularityTypeId equals it.IrregularityTypeId into g2
                    from it in g2.DefaultIfEmpty()
                    join sc in this.unitOfWork.DbContext.Set<IrregularitySanctionCategory>() on iv.Sanction.SanctionCategoryId equals sc.IrregularitySanctionCategoryId into g3
                    from sc in g3.DefaultIfEmpty()
                    join st in this.unitOfWork.DbContext.Set<IrregularitySanctionType>() on iv.Sanction.SanctionTypeId equals st.IrregularitySanctionTypeId into g4
                    from st in g4.DefaultIfEmpty()
                    select new IrrReportItemVO
                    {
                        IrregularityId = i.IrregularityId,
                        IrregularityVersionId = iv.IrregularityVersionId,
                        RegNumber = i.RegNumber,
                        ProgrammeCode = pr.Code,
                        ProgrammeName = pr.Name,
                        ProjectName = p.Name,
                        ProjectRegNumber = p.RegNumber,
                        SignalNumber = irrs.RegNumber,
                        SignalRegDate = irrs.RegDate,
                        SignalSource = irrs.SignalSource,
                        SignalActRegNum = irrs.ActRegNum,
                        SignalActRegDate = irrs.ActRegDate,
                        CreateDate = iv.CreateDate,
                        ModifyDate = iv.ModifyDate,
                        Rapporteur = iv.Rapporteur,
                        ReportQuarter = iv.ReportQuarter,
                        ReportYear = iv.ReportYear,
                        ShouldReportToOlaf = iv.ShouldReportToOlaf,
                        ReasonNotReportingToOlaf = iv.ReasonNotReportingToOlaf,
                        IsNewUnlawfulPractice = iv.IsNewUnlawfulPractice,
                        ShouldInformOther = iv.ShouldInformOther,
                        ProcedureStatus = iv.ProcedureStatus,
                        FinancialStatus = fs.Name,
                        CaseState = iv.CaseState,
                        IrregularityEndDate = iv.IrregularityEndDate,
                        EndingActRegNum = iv.EndingActRegNum,
                        EndingActDate = iv.EndingActDate,
                        ImpairedRegulationAct = iv.ImpairedRegulation.ImpairedRegulationAct,
                        ImpairedRegulationNum = iv.ImpairedRegulation.ImpairedRegulationNum,
                        ImpairedRegulationYear = iv.ImpairedRegulation.ImpairedRegulationYear,
                        ImpairedRegulation = iv.ImpairedRegulation.ImpairedRegulation,
                        ImpairedNationalRegulation = iv.ImpairedRegulation.ImpairedNationalRegulation,
                        IrregularityDateFrom = iv.IrregularityDateFrom,
                        IrregularityDateTo = iv.IrregularityDateTo,
                        IrregularityClassification = iv.IrregularityClassification,
                        IrregularityCategory = ic.Name,
                        IrregularityType = it.Name,
                        AppliedPractices = iv.AppliedPractices,
                        BeneficiaryData = iv.BeneficiaryData,
                        AdminAscertainments = iv.AdminAscertainments,
                        IrregularityDetectedBy = iv.IrregularityDetectedBy,
                        AdminProcedures = iv.AdminProcedures,
                        PenaltyProcedures = iv.PenaltyProcedures,
                        CheckTime = iv.CheckTime,
                        EUCoFinancingPercent = iv.EUCoFinancingPercent,
                        ExpensesLvBfpEuAmount = iv.ExpensesLv.BfpEuAmount,
                        ExpensesLvBfpBgAmount = iv.ExpensesLv.BfpBgAmount,
                        ExpensesLvBfpTotalAmount = iv.ExpensesLv.BfpTotalAmount,
                        ExpensesLvSelfAmount = iv.ExpensesLv.SelfAmount,
                        ExpensesLvTotalAmount = iv.ExpensesLv.TotalAmount,
                        IrregularExpensesLvEuAmount = iv.IrregularExpensesLv.EuAmount,
                        IrregularExpensesLvBgAmount = iv.IrregularExpensesLv.BgAmount,
                        IrregularExpensesLvTotalAmount = iv.IrregularExpensesLv.TotalAmount,
                        CertifiedExpensesLvEuAmount = iv.CertifiedExpensesLv.EuAmount,
                        CertifiedExpensesLvBgAmount = iv.CertifiedExpensesLv.BgAmount,
                        CertifiedExpensesLvTotalAmount = iv.CertifiedExpensesLv.TotalAmount,
                        ShouldDecertifyIrregularExpenses = iv.ShouldDecertifyIrregularExpenses,
                        DecertificationComments = iv.DecertificationComments,
                        SanctionProcedureType = iv.Sanction.ProcedureType,
                        SanctionProcedureKind = iv.Sanction.ProcedureKind,
                        SanctionProcedureStartDate = iv.Sanction.ProcedureStartDate,
                        SanctionProcedureExpectedEndDate = iv.Sanction.ProcedureExpectedEndDate,
                        SanctionProcedureEndDate = iv.Sanction.ProcedureEndDate,
                        SanctionProcedureStatus = iv.Sanction.ProcedureStatus,
                        SanctionCategory = sc.Name,
                        SanctionType = st.Name,
                        Fines = iv.Sanction.Fines,
                        RapporteurComments = iv.RapporteurComments,
                    }).ToList();
        }

        public IList<IrrRegisterItemVO> GetIrrRegister(
            int[] programmeIds,
            Year? reportYearFrom = null,
            Quarter? reportQuarterFrom = null,
            Year? reportYearTo = null,
            Quarter? reportQuarterTo = null)
        {
            var predicate = PredicateBuilder.True<IrregularityVersion>();

            if (reportYearFrom.HasValue)
            {
                predicate = predicate.And(i => i.ReportYear >= reportYearFrom);

                if (reportQuarterFrom.HasValue)
                {
                    predicate = predicate.And(i => i.ReportQuarter >= reportQuarterFrom);
                }
            }

            if (reportYearTo.HasValue)
            {
                predicate = predicate.And(i => i.ReportYear <= reportYearTo);

                if (reportQuarterTo.HasValue)
                {
                    predicate = predicate.And(i => i.ReportQuarter <= reportQuarterTo);
                }
            }

            return (from irrv in this.unitOfWork.DbContext.Set<IrregularityVersion>().Where(predicate)
                    join i in this.unitOfWork.DbContext.Set<Irregularity>() on irrv.IrregularityId equals i.IrregularityId
                    join irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on i.IrregularitySignalId equals irrs.IrregularitySignalId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on i.ProgrammeId equals pr.MapNodeId
                    join proj in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals proj.ProjectId
                    where programmeIds.Contains(i.ProgrammeId) && i.Status == IrregularityStatus.Entered && irrv.Status != IrregularityVersionStatus.Draft
                    select new
                    {
                        i.IrregularityId,
                        irrv.IrregularityVersionId,
                        irrv.RegNumber,
                        SignalNumber = irrs.RegNumber,
                        SignalActRegNum = irrs.ActRegNum,
                        SignalActRegDate = irrs.ActRegDate,
                        i.FirstReportYear,
                        i.FirstReportQuarter,
                        irrv.ReportYear,
                        irrv.ReportQuarter,
                        ProgrammeCode = pr.Code,
                        irrv.Rapporteur,
                        ProjectName = proj.Name,
                        ProjectNumber = proj.RegNumber,
                        irrv.IrregularityClassification,
                        irrv.AppliedPractices,
                        irrv.ExpensesLv,
                        irrv.IrregularExpensesLv,
                        irrv.PaidLv,
                        irrv.ContractDebtStatus,
                        irrv.AdminProcedures,
                        irrv.PenaltyProcedures,
                        irrv.CaseState,
                        EndingActRegNum = irrv.EndingActRegNum,
                        EndingActDate = irrv.EndingActDate,
                        irrv.RapporteurComments,
                    }).ToList()
                    .Select(o => new IrrRegisterItemVO
                    {
                        IrregularityId = o.IrregularityId,
                        VersionId = o.IrregularityVersionId,
                        RegNumber = o.RegNumber,
                        SignalNumber = o.SignalNumber,
                        SignalActRegNum = o.SignalActRegNum,
                        SignalActRegDate = o.SignalActRegDate,
                        FirstReportYear = o.FirstReportYear.Value,
                        FirstReportQuarter = o.FirstReportQuarter.Value,
                        ReportYear = o.ReportYear,
                        ReportQuarter = o.ReportQuarter,
                        ProgrammeCode = o.ProgrammeCode,
                        Rapporteur = o.Rapporteur,
                        ProjectName = o.ProjectName,
                        ProjectNumber = o.ProjectNumber,
                        ProjectOtherNumber = null,
                        IrregularityClassification = o.IrregularityClassification,
                        AppliedPractices = o.AppliedPractices,
                        ExpensesLvEuAmount = o.ExpensesLv.BfpEuAmount,
                        ExpensesLvBgAmount = o.ExpensesLv.BfpBgAmount,
                        ExpensesLvBfpAmount = o.ExpensesLv.BfpTotalAmount,
                        ExpensesLvSelfAmount = o.ExpensesLv.SelfAmount,
                        ExpensesLvTotalAmount = o.ExpensesLv.TotalAmount,
                        IrregularExpensesLvEuAmount = o.IrregularExpensesLv.EuAmount,
                        IrregularExpensesLvBgAmount = o.IrregularExpensesLv.BgAmount,
                        IrregularExpensesLvBfpAmount = o.IrregularExpensesLv.TotalAmount,
                        PaidIrregularExpensesLvEuAmount = o.PaidLv.EuAmount,
                        PaidIrregularExpensesLvBgAmount = o.PaidLv.BgAmount,
                        PaidIrregularExpensesLvBfpAmount = o.PaidLv.TotalAmount,
                        ContractDebtStatus = o.ContractDebtStatus,
                        AdminProcedures = o.AdminProcedures,
                        PenaltyProcedures = o.PenaltyProcedures,
                        CaseState = o.CaseState,
                        EndingActRegNum = o.EndingActRegNum,
                        EndingActDate = o.EndingActDate,
                        RapporteurComments = o.RapporteurComments,
                    }).ToList();
        }

        public IList<IrrReportInvolvedPersonVO> GetReportInvolvedPersons(int[] versionIds)
        {
            return (from ivp in this.unitOfWork.DbContext.Set<IrregularityVersionInvolvedPerson>()
                    join irrv in this.unitOfWork.DbContext.Set<IrregularityVersion>() on ivp.IrregularityVersionId equals irrv.IrregularityVersionId
                    where versionIds.Contains(irrv.IrregularityVersionId)
                    select new IrrReportInvolvedPersonVO
                    {
                        PersonId = ivp.IrregularityVersionInvolvedPersonId,
                        IrregularityVersionId = ivp.IrregularityVersionId,
                        IrregularityVersionNum = irrv.RegNumber,
                        Uin = ivp.Uin,
                        UinType = ivp.UinType,
                        LegalType = ivp.LegalType,
                        FirstName = ivp.FirstName,
                        MiddleName = ivp.MiddleName,
                        LastName = ivp.LastName,
                        CompanyName = ivp.CompanyName,
                        HoldingName = ivp.HoldingName,
                        TradeName = ivp.TradeName,
                    }).ToList();
        }

        public IList<IrrReportVersionDataVO> GetVersionsData(int[] irregularityIds)
        {
            return (from iv in this.unitOfWork.DbContext.Set<IrregularityVersion>()
                    join i in this.unitOfWork.DbContext.Set<Irregularity>() on iv.IrregularityId equals i.IrregularityId
                    where irregularityIds.Contains(i.IrregularityId) && iv.Status != IrregularityVersionStatus.Draft
                    select new IrrReportVersionDataVO
                    {
                        IrregularityId = i.IrregularityId,
                        VersionId = iv.IrregularityVersionId,
                        IrregularityNum = i.RegNumber,
                        ReportQuarter = iv.ReportQuarter,
                        ReportYear = iv.ReportYear,
                    }).ToList();
        }

        public IList<IrregularityDocVO> GetDocuments(int irregularityId)
        {
            return (from isd in this.unitOfWork.DbContext.Set<IrregularityDoc>()
                    where isd.IrregularityId == irregularityId
                    orderby isd.IrregularityDocId descending
                    select new IrregularityDocVO
                    {
                        DocumentId = isd.IrregularityDocId,
                        Description = isd.Description,
                        File = new FileVO
                        {
                            Key = isd.FileKey,
                            Name = isd.FileName,
                        },
                    }).ToList();
        }

        public IList<IrregularityFinancialCorrectionVO> GetFinancialCorrections(int irregularityId)
        {
            return (from item in this.unitOfWork.DbContext.Set<IrregularityFinancialCorrection>()
                    join fc in this.unitOfWork.DbContext.Set<FinancialCorrection>() on item.FinancialCorrectionId equals fc.FinancialCorrectionId
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into g1
                    from cc in g1.DefaultIfEmpty()

                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g2
                    from cctor in g2.DefaultIfEmpty()

                    join cbla in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fc.ContractBudgetLevel3AmountId equals cbla.ContractBudgetLevel3AmountId into g3
                    from cbla in g3.DefaultIfEmpty()

                    where item.IrregularityId == irregularityId
                    orderby fc.OrderNum descending
                    select new
                    {
                        item.IrregularityFinancialCorrectionId,
                        item.FinancialCorrectionId,
                        fc.OrderNum,
                        fc.ImpositionDate,
                        fc.Status,
                        ContractContractNumber = cc.Number,
                        ContractContractorName = cctor.Name,
                        ContractContractorUinType = (UinType?)cctor.UinType,
                        ContractContractorUin = cctor.Uin,
                        ContractBudgetLevel3Name = cbla.Name,
                    }).ToList()
                    .Select(o => new IrregularityFinancialCorrectionVO
                    {
                        IrregularityItemId = o.IrregularityFinancialCorrectionId,
                        FinancialCorrectionId = o.FinancialCorrectionId,
                        OrderNum = o.OrderNum,
                        Status = o.Status,
                        ImpositionDate = o.ImpositionDate,
                        ContractContractNumber = o.ContractContractNumber,
                        ContractContractorCompany = o.ContractContractorUinType.HasValue ?
                            string.Format("{0} ({1}: {2})", o.ContractContractorName, o.ContractContractorUinType.GetEnumDescription(), o.ContractContractorUin) :
                            null,
                        ContractBudgetLevel3Name = o.ContractBudgetLevel3Name,
                    }).ToList();
        }

        public IList<IrregularityFinancialCorrectionVO> GetNotIncludedFinancialCorrections(int irregularityId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<IrregularityFinancialCorrection>()
                           where item.IrregularityId == irregularityId
                           select item.FinancialCorrectionId;

            return (from fc in this.unitOfWork.DbContext.Set<FinancialCorrection>()
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on fc.ContractId equals irr.ContractId

                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on fc.ContractContractId equals cc.ContractContractId into g1
                    from cc in g1.DefaultIfEmpty()

                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId into g2
                    from cctor in g2.DefaultIfEmpty()

                    join cbla in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>() on fc.ContractBudgetLevel3AmountId equals cbla.ContractBudgetLevel3AmountId into g3
                    from cbla in g3.DefaultIfEmpty()
                    where !subquery.Contains(fc.FinancialCorrectionId) && irr.IrregularityId == irregularityId && fc.Status == FinancialCorrectionStatus.Entered
                    orderby fc.OrderNum descending
                    select new
                    {
                        fc.FinancialCorrectionId,
                        fc.OrderNum,
                        fc.ImpositionDate,
                        fc.Status,
                        ContractContractNumber = cc.Number,
                        ContractContractorName = cctor.Name,
                        ContractContractorUinType = (UinType?)cctor.UinType,
                        ContractContractorUin = cctor.Uin,
                        ContractBudgetLevel3Name = cbla.Name,
                    }).ToList()
                    .Select(o => new IrregularityFinancialCorrectionVO
                    {
                        FinancialCorrectionId = o.FinancialCorrectionId,
                        OrderNum = o.OrderNum,
                        Status = o.Status,
                        ImpositionDate = o.ImpositionDate,
                        ContractContractNumber = o.ContractContractNumber,
                        ContractContractorCompany = o.ContractContractorUinType.HasValue ?
                            string.Format("{0} ({1}: {2})", o.ContractContractorName, o.ContractContractorUinType.GetEnumDescription(), o.ContractContractorUin) :
                            null,
                        ContractBudgetLevel3Name = o.ContractBudgetLevel3Name,
                    }).ToList();
        }

        public IrregularityInfoVO GetInfo(int irregularityId)
        {
            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>()

                    join c in this.unitOfWork.DbContext.Set<Contract>() on irr.ContractId equals c.ContractId into g0
                    from c in g0.DefaultIfEmpty()

                    where irr.IrregularityId == irregularityId
                    select new IrregularityInfoVO
                    {
                        ContractNum = c.RegNumber,
                        Status = irr.Status,
                        RegNumber = irr.RegNumber,
                        Version = irr.Version,
                    }).Single();
        }

        public IrregularityDataVO GetData(int irregularityId)
        {
            var data = (from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                        join irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on irr.IrregularitySignalId equals irrs.IrregularitySignalId
                        join p in this.unitOfWork.DbContext.Set<Project>() on irrs.ProjectId equals p.ProjectId
                        where irr.IrregularityId == irregularityId
                        select new IrregularityDataVO
                        {
                            IrregularityId = irr.IrregularityId,
                            RegNumber = irr.RegNumber,
                            CaseState = irr.CaseState,
                            Status = irr.Status,
                            IsRemoved = irr.Status == IrregularityStatus.Removed,
                            EndDate = irr.IrregularityEndDate,
                            ProgrammeId = irr.ProgrammeId,
                            ProcedureId = irrs.ProcedureId,
                            ProjectRegNumber = p.RegNumber,
                            ProjectName = p.Name,
                            FirstReportYear = irr.FirstReportYear,
                            FirstReportQuarter = irr.FirstReportQuarter,
                            LastReportYear = irr.LastReportYear,
                            LastReportQuarter = irr.LastReportQuarter,
                            SignalNumber = irrs.RegNumber,
                            SignalRegDate = irrs.RegDate,
                            SignalSource = irrs.SignalSource,
                            SignalActRegNum = irrs.ActRegNum,
                            SignalActRegDate = irrs.ActRegDate,
                            DeleteNote = irr.DeleteNote,
                            Version = irr.Version,
                        }).Single();

            data.ProgrammePeriod = "2014-2020 г";

            return data;
        }

        public int GetProgrammeId(int irregularityId)
        {
            return (from i in this.unitOfWork.DbContext.Set<Irregularity>()
                    where i.IrregularityId == irregularityId
                    select i.ProgrammeId).Single();
        }

        public IrregularityStatus GetIrregularityStatus(int irregularityId)
        {
            return (from i in this.unitOfWork.DbContext.Set<Irregularity>()
                    where i.IrregularityId == irregularityId
                    select i.Status).Single();
        }

        public new void Remove(Irregularity irregularity)
        {
            if (irregularity.Status != IrregularityStatus.New)
            {
                throw new DomainValidationException("Cannot delete irregularity with status different from new.");
            }

            base.Remove(irregularity);
        }

        public bool HasFinancialCorrections(int irregularityId)
        {
            return (from ifc in this.unitOfWork.DbContext.Set<IrregularityFinancialCorrection>()
                    where ifc.IrregularityId == irregularityId
                    select ifc.IrregularityId)
                .Any();
        }

        public bool HasNonRemovedIrregularityWithTheSameNumber(int programmeId, int irregularityId, string regNumber)
        {
            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                    where irr.ProgrammeId == programmeId && irr.Status != IrregularityStatus.Removed && irr.RegNumber == regNumber
                    select irr.RegNumber).Any();
        }

        public bool HasRemovedRemovedIrregularityWithTheSameNumber(int programmeId, int irregularityId, string regNumber)
        {
            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                    where irr.ProgrammeId == programmeId && irr.Status == IrregularityStatus.Removed && irr.RegNumber == regNumber
                    select irr.RegNumber).Any();
        }

        public IList<IrregularityVO> GetFinancialCorrectionIrregularities(int financialCorrectionId)
        {
            return (from ifc in this.unitOfWork.DbContext.Set<IrregularityFinancialCorrection>()
                    join irr in this.unitOfWork.DbContext.Set<Irregularity>() on ifc.IrregularityId equals irr.IrregularityId
                    join irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on irr.IrregularitySignalId equals irrs.IrregularitySignalId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irr.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on irr.ProgrammeId equals pr.MapNodeId
                    where ifc.FinancialCorrectionId == financialCorrectionId && irr.Status != IrregularityStatus.New
                    orderby irr.CreateDate descending
                    select new
                    {
                        irr.IrregularityId,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        irr.Status,
                        irr.RegNumber,
                        SignalNum = irrs.RegNumber,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    }).ToList()
                    .Select(o => new IrregularityVO
                    {
                        IrregularityId = o.IrregularityId,
                        ProgrammeName = o.ProgrammeName,
                        ContractRegNumber = o.ContractRegNumber,
                        SignalNum = o.SignalNum,
                        Status = o.Status,
                        RegNumber = o.RegNumber,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<IrregularityVO> GetIrregularitiesForProjectDossier(int contractId)
        {
            return (from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                    join irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>() on irr.IrregularitySignalId equals irrs.IrregularitySignalId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on irr.ContractId equals c.ContractId
                    join pr in this.unitOfWork.DbContext.Set<Programme>() on irr.ProgrammeId equals pr.MapNodeId
                    where irr.ContractId == contractId && irr.Status != IrregularityStatus.New
                    orderby irr.CreateDate descending
                    select new
                    {
                        irr.IrregularityId,
                        SignalNum = irrs.RegNumber,
                        ProgrammeName = pr.Name,
                        ContractRegNumber = c.RegNumber,
                        irr.Status,
                        irr.RegNumber,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    }).ToList()
                    .Select(o => new IrregularityVO
                    {
                        IrregularityId = o.IrregularityId,
                        SignalNum = o.SignalNum,
                        ProgrammeName = o.ProgrammeName,
                        ContractRegNumber = o.ContractRegNumber,
                        Status = o.Status,
                        RegNumber = o.RegNumber,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public int? GetContractId(int irregularityId)
        {
            return (from i in this.unitOfWork.DbContext.Set<Irregularity>()
                    where i.IrregularityId == irregularityId
                    select i.ContractId).Single();
        }
    }
}
