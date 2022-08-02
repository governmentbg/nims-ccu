using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.FlatFinancialCorrections.Repositories
{
    internal class FlatFinancialCorrectionsRepository : AggregateRepository<FlatFinancialCorrection>, IFlatFinancialCorrectionsRepository
    {
        public FlatFinancialCorrectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<FlatFinancialCorrection, object>>[] Includes
        {
            get
            {
                return new Expression<Func<FlatFinancialCorrection, object>>[]
                {
                    e => e.File,
                    e => e.FlatFinancialCorrectionLevelItems,
                };
            }
        }

        public IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrections(int[] programmeIds)
        {
            return (from ffc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>()
                    where programmeIds.Contains(ffc.ProgrammeId)
                    select new FlatFinancialCorrectionVO()
                    {
                        FlatFinancialCorrectionId = ffc.FlatFinancialCorrectionId,
                        Name = ffc.Name,
                        OrderNum = ffc.OrderNum,
                        Level = ffc.Level,
                        Type = ffc.Type,
                        Status = ffc.Status,
                        ImpositionDate = ffc.ImpositionDate,
                        ImpositionNumber = ffc.ImpositionNumber,
                        Description = ffc.Description,
                        CreateDate = ffc.CreateDate,
                        ModifyDate = ffc.ModifyDate,
                        Version = ffc.Version,
                    })
                .ToList();
        }

        public int GetNextOrderNumber()
        {
            var lastOrderNumber = this.Set()
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public int GetProgrammeId(int flatFinancialCorrectionId)
        {
            return (from ffc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>()
                    where ffc.FlatFinancialCorrectionId == flatFinancialCorrectionId
                    select ffc.ProgrammeId).Single();
        }

        public IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrectionsForProjectDossier(int contractId)
        {
            return (from ffc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ffc.ContractId equals c.ContractId
                    where ffc.ContractId == contractId && ffc.Status != FlatFinancialCorrectionStatus.Draft
                    select new FlatFinancialCorrectionVO
                    {
                        FlatFinancialCorrectionId = ffc.FlatFinancialCorrectionId,
                        ContractRegNumber = c.RegNumber,
                        Name = ffc.Name,
                        OrderNum = ffc.OrderNum,
                        Level = ffc.Level,
                        Type = ffc.Type,
                        Status = ffc.Status,
                        ImpositionDate = ffc.ImpositionDate,
                        ImpositionNumber = ffc.ImpositionNumber,
                        Description = ffc.Description,
                        CreateDate = ffc.CreateDate,
                        ModifyDate = ffc.ModifyDate,
                        Version = ffc.Version,
                    }).ToList();
        }

        public IList<FlatFinancialCorrectionProgrammePriorityItemVO> GetFlatFinancialCorrectionProgrammePriorityItems(int flatFinancialCorrectionId)
        {
            return (from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionProgrammePriorityItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on item.ItemId equals pp.MapNodeId
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                    select new FlatFinancialCorrectionProgrammePriorityItemVO()
                    {
                        FlatFinancialCorrectionLevelItemId = item.FlatFinancialCorrectionLevelItemId,
                        FlatFinancialCorrectionId = item.FlatFinancialCorrectionId,
                        Percent = item.Percent,
                        EuAmount = item.EuAmount,
                        BgAmount = item.BgAmount,
                        TotalAmount = item.TotalAmount,
                        ItemId = pp.MapNodeId,
                        Code = pp.Code,
                        Name = pp.Name,
                        NameAlt = pp.NameAlt,
                        ProgrammeName = p.Name,
                        Description = pp.Description,
                        DescriptionAlt = pp.DescriptionAlt,
                    })
                    .ToList();
        }

        public IList<FlatFinancialCorrectionProcedureItemVO> GetFlatFinancialCorrectionProcedureItems(int flatFinancialCorrectionId)
        {
            var proceduresQuery =
                from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionProcedureItem>()
                join proc in this.unitOfWork.DbContext.Set<Procedure>() on item.ItemId equals proc.ProcedureId
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                select new
                {
                    FlatFinancialCorrectionLevelItemId = item.FlatFinancialCorrectionLevelItemId,
                    FlatFinancialCorrectionId = item.FlatFinancialCorrectionId,
                    Percent = item.Percent,
                    EuAmount = item.EuAmount,
                    BgAmount = item.BgAmount,
                    TotalAmount = item.TotalAmount,
                    proc.ProcedureId,
                    proc.Code,
                    proc.Name,
                    proc.ProcedureStatus,
                    ProgramPriorityId = pp.MapNodeId,
                    ProgrammePriorityName = pp.Name,
                    ProgrammeId = prog.MapNodeId,
                    ProgrammeName = prog.Name,
                };

            var proceduresData = proceduresQuery.ToList();

            var procedureTimeLimits =
                (from ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                 group ptl by ptl.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     EndDate = g.Max(ptl => ptl.EndDate),
                 })
                 .ToList();

            return (from pd in proceduresData
                    join ptl in procedureTimeLimits on pd.ProcedureId equals ptl.ProcedureId into g
                    from ptl in g.DefaultIfEmpty()
                    group pd by new
                    {
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                        EndDate = ptl == null ? (DateTime?)null : ptl.EndDate,
                        FlatFinancialCorrectionLevelItemId = pd.FlatFinancialCorrectionLevelItemId,
                        FlatFinancialCorrectionId = pd.FlatFinancialCorrectionId,
                        Percent = pd.Percent,
                        EuAmount = pd.EuAmount,
                        BgAmount = pd.BgAmount,
                        TotalAmount = pd.TotalAmount,
                    }
                    into g
                    select new FlatFinancialCorrectionProcedureItemVO
                    {
                        FlatFinancialCorrectionLevelItemId = g.Key.FlatFinancialCorrectionLevelItemId,
                        FlatFinancialCorrectionId = g.Key.FlatFinancialCorrectionId,
                        Percent = g.Key.Percent,
                        EuAmount = g.Key.EuAmount,
                        BgAmount = g.Key.BgAmount,
                        TotalAmount = g.Key.TotalAmount,
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Key.EndDate,
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public IList<FlatFinancialCorrectionContractItemVO> GetFlatFinancialCorrectionContractItems(int flatFinancialCorrectionId)
        {
            return (from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionContractItem>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on item.ItemId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                    orderby c.CreateDate descending
                    select new
                    {
                        FlatFinancialCorrectionLevelItemId = item.FlatFinancialCorrectionLevelItemId,
                        FlatFinancialCorrectionId = item.FlatFinancialCorrectionId,
                        Percent = item.Percent,
                        EuAmount = item.EuAmount,
                        BgAmount = item.BgAmount,
                        TotalAmount = item.TotalAmount,
                        c.ContractId,
                        p.ProcedureId,
                        c.ProgrammeId,
                        c.ContractStatus,
                        ProcedureName = p.Name,
                        c.Name,
                        c.RegNumber,
                        c.ContractDate,
                        c.ExecutionStatus,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    }).ToList()
                    .Select(o => new FlatFinancialCorrectionContractItemVO
                    {
                        FlatFinancialCorrectionLevelItemId = o.FlatFinancialCorrectionLevelItemId,
                        FlatFinancialCorrectionId = o.FlatFinancialCorrectionId,
                        Percent = o.Percent,
                        EuAmount = o.EuAmount,
                        BgAmount = o.BgAmount,
                        TotalAmount = o.TotalAmount,
                        ItemId = o.ContractId,
                        ProcedureId = o.ProcedureId,
                        ProgrammeId = o.ProgrammeId,
                        ContractStatus = o.ContractStatus,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<FlatFinancialCorrectionContractContractItemVO> GetFlatFinancialCorrectionContractContractItems(int flatFinancialCorrectionId)
        {
            return (from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionContractContractItem>()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on item.ItemId equals cc.ContractContractId
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                    select new { item, cc, ccr, c, p, country, set })
                .ToList()
                .Select(t => new FlatFinancialCorrectionContractContractItemVO()
                {
                    FlatFinancialCorrectionLevelItemId = t.item.FlatFinancialCorrectionLevelItemId,
                    FlatFinancialCorrectionId = t.item.FlatFinancialCorrectionId,
                    Percent = t.item.Percent,
                    EuAmount = t.item.EuAmount,
                    BgAmount = t.item.BgAmount,
                    TotalAmount = t.item.TotalAmount,
                    ItemId = t.cc.ContractContractId,

                    SignDate = t.cc.SignDate,
                    Number = t.cc.Number,
                    TotalAmountExcludingVAT = t.cc.TotalAmountExcludingVAT,
                    VATAmountIfEligible = t.cc.VATAmountIfEligible,
                    TotalFundedValue = t.cc.TotalFundedValue,
                    NumberAnnexes = t.cc.NumberAnnexes,
                    CurrentAnnexTotalAmount = t.cc.CurrentAnnexTotalAmount,

                    Uin = t.ccr.Uin,
                    UinType = t.ccr.UinType,
                    Name = t.ccr.Name,
                    ContractContractorCompany = string.Format("{0} ({1}: {2})", t.ccr.Name, t.ccr.UinType.GetEnumDescription(), t.ccr.Uin),
                    Seat = t.country.NutsCode == "BG" ? t.set.Name + " " + t.ccr.SeatPostCode + " " + t.ccr.SeatStreet : t.country.Name + " " + t.ccr.SeatAddress,

                    ProcedureName = t.p.Name,
                    ContractStatus = t.c.ContractStatus,
                    ContractName = t.c.Name,
                    ContractRegNumber = t.c.RegNumber,
                    ContractCompany = string.Format("{0} ({1}: {2})", t.c.CompanyName, t.c.CompanyUinType.GetEnumDescription(), t.c.CompanyUin),
                })
                .ToList();
        }

        public IList<FlatFinancialCorrectionProgrammePriorityItemVO> GetProgrammePrioritiesForFlatFinancialCorrection(int flatFinancialCorrectionId, int programmeId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionProgrammePriorityItem>()
                           where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                           select item.ItemId;

            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                    join mnr in this.unitOfWork.DbContext.Set<MapNodeRelation>() on pp.MapNodeId equals mnr.MapNodeId
                    join p in this.unitOfWork.DbContext.Set<Programme>() on mnr.ProgrammeId equals p.MapNodeId
                    where !subquery.Contains(pp.MapNodeId) && programmeId == mnr.ProgrammeId.Value
                    select new FlatFinancialCorrectionProgrammePriorityItemVO
                    {
                        ItemId = pp.MapNodeId,
                        Code = pp.Code,
                        Name = pp.Name,
                        NameAlt = pp.NameAlt,
                        Description = pp.Description,
                        DescriptionAlt = pp.DescriptionAlt,
                        ProgrammeName = p.Name,
                    })
                    .ToList();
        }

        public IList<FlatFinancialCorrectionProcedureItemVO> GetProceduresForFlatFinancialCorrection(int flatFinancialCorrectionId, int programmeId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionProcedureItem>()
                           where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                           select item.ItemId;

            var proceduresQuery =
                from proc in this.unitOfWork.DbContext.Set<Procedure>()
                join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on proc.ProcedureId equals ps.ProcedureId
                join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                where !subquery.Contains(proc.ProcedureId) && programmeId == prog.MapNodeId && Procedure.EvalSessionOrProjectCreationStatuses.Contains(proc.ProcedureStatus)
                select new
                {
                    proc.ProcedureId,
                    proc.Code,
                    proc.Name,
                    proc.ProcedureStatus,
                    ProgramPriorityId = pp.MapNodeId,
                    ProgrammePriorityName = pp.Name,
                    ProgrammeId = prog.MapNodeId,
                    ProgrammeName = prog.Name,
                };

            var proceduresData = proceduresQuery.ToList();

            var procedureTimeLimits =
                (from ptl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                 group ptl by ptl.ProcedureId into g
                 select new
                 {
                     ProcedureId = g.Key,
                     EndDate = g.Max(ptl => ptl.EndDate),
                 })
                 .ToList();

            return (from pd in proceduresData
                    join ptl in procedureTimeLimits on pd.ProcedureId equals ptl.ProcedureId into g
                    from ptl in g.DefaultIfEmpty()
                    group pd by new
                    {
                        pd.ProcedureId,
                        pd.Code,
                        pd.Name,
                        pd.ProcedureStatus,
                        EndDate = ptl == null ? (DateTime?)null : ptl.EndDate,
                    }
                    into g
                    select new FlatFinancialCorrectionProcedureItemVO
                    {
                        ItemId = g.Key.ProcedureId,
                        Name = g.Key.Name,
                        Code = g.Key.Code,
                        Status = g.Key.ProcedureStatus,
                        EndingDate = g.Key.EndDate,
                        ProgrammeNames = g.Select(pd => pd.ProgrammeName).Distinct().ToList(),
                    })
                    .ToList();
        }

        public IList<FlatFinancialCorrectionContractItemVO> GetContractsForFlatFinancialCorrection(int flatFinancialCorrectionId, int programmeId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionContractItem>()
                           where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                           select item.ItemId;

            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where !subquery.Contains(c.ContractId) && programmeId == c.ProgrammeId && c.ContractStatus == ContractStatus.Entered
                    orderby c.CreateDate descending
                    select new
                    {
                        c.ContractId,
                        p.ProcedureId,
                        c.ProgrammeId,
                        c.ContractStatus,
                        ProcedureName = p.Name,
                        c.Name,
                        c.RegNumber,
                        c.ContractDate,
                        c.ExecutionStatus,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    }).ToList()
                    .Select(o => new FlatFinancialCorrectionContractItemVO
                    {
                        ItemId = o.ContractId,
                        ProcedureId = o.ProcedureId,
                        ProgrammeId = o.ProgrammeId,
                        ContractStatus = o.ContractStatus,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<FlatFinancialCorrectionContractContractItemVO> GetContractContractsForFlatFinancialCorrection(int flatFinancialCorrectionId, int contractId, int programmeId)
        {
            var subquery = from item in this.unitOfWork.DbContext.Set<FlatFinancialCorrectionLevelItem>().OfType<FlatFinancialCorrectionContractContractItem>()
                           where item.FlatFinancialCorrectionId == flatFinancialCorrectionId
                           select item.ItemId;

            return (from cc in this.unitOfWork.DbContext.Set<ContractContract>()
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where !subquery.Contains(cc.ContractContractId) && programmeId == c.ProgrammeId && cc.ContractId == contractId
                    select new { cc, ccr, c, p, country, set })
                .ToList()
                .Select(t => new FlatFinancialCorrectionContractContractItemVO()
                {
                    ItemId = t.cc.ContractContractId,
                    SignDate = t.cc.SignDate,
                    Number = t.cc.Number,
                    TotalAmountExcludingVAT = t.cc.TotalAmountExcludingVAT,
                    VATAmountIfEligible = t.cc.VATAmountIfEligible,
                    TotalFundedValue = t.cc.TotalFundedValue,
                    NumberAnnexes = t.cc.NumberAnnexes,
                    CurrentAnnexTotalAmount = t.cc.CurrentAnnexTotalAmount,

                    Uin = t.ccr.Uin,
                    UinType = t.ccr.UinType,
                    Name = t.ccr.Name,
                    ContractContractorCompany = string.Format("{0} ({1}: {2})", t.ccr.Name, t.ccr.UinType.GetEnumDescription(), t.ccr.Uin),
                    Seat = t.country.NutsCode == "BG" ? t.set.Name + " " + t.ccr.SeatPostCode + " " + t.ccr.SeatStreet : t.country.Name + " " + t.ccr.SeatAddress,

                    ProcedureName = t.p.Name,
                    ContractStatus = t.c.ContractStatus,
                    ContractName = t.c.Name,
                    ContractRegNumber = t.c.RegNumber,
                    ContractCompany = string.Format("{0} ({1}: {2})", t.c.CompanyName, t.c.CompanyUinType.GetEnumDescription(), t.c.CompanyUin),
                })
                .ToList();
        }

        public IList<string> CanChangeFlatFinancialCorrectionToDraft(int flatFinancialCorrectionId)
        {
            IList<string> errors = new List<string>();

            if (this.unitOfWork
                .DbContext.Set<Domain.Debts.CorrectionDebt>()
                .Any(cd => cd.FlatFinancialCorrectionId == flatFinancialCorrectionId))
            {
                errors.Add($"Не можете да промените статуса на финансовата корекция за системни пропуски на 'Чернова', защото участва в дългове по ФКСП.");
            }

            if (this.unitOfWork
                .DbContext.Set<ContractReportCorrection>()
                .Any(crc => crc.FlatFinancialCorrectionId == flatFinancialCorrectionId))
            {
                errors.Add($"Не можете да промените статуса на финансовата корекция за системни пропуски на 'Чернова', защото участва в коригиране на верифицирани суми на други нива.");
            }

            return errors;
        }
    }
}
