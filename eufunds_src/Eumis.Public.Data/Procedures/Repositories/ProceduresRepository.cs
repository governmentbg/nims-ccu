using Autofac.Extras.Attributed;
using Eumis.Public.Common.Localization;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.Linq;
using Eumis.Public.Data.Procedures.ViewObjects;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using Eumis.Public.Domain.Entities.Umis.Projects;
using System;
using System.Linq;

namespace Eumis.Public.Data.Procedures.Repositories
{
    internal class ProceduresRepository : Repository, IProceduresRepository
    {
        public ProceduresRepository([WithKey(DbKey.Umis)]IUnitOfWork uow)
            : base(uow)
        {
        }

        public PageVO<ProcedureVO> GetProcedures(
            int? settlementId = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            var procedurePredicate = PredicateBuilder.True<Procedure>()
                .And(p =>
                p.ProcedureStatus != ProcedureStatus.Canceled &&
                p.ProcedureStatus != ProcedureStatus.Terminated &&
                p.ProcedureStatus != ProcedureStatus.Ended);

            if (settlementId != null)
            {
                var settlementPath = this.unitOfWork.DbContext.Set<Settlement>().Single(s => s.SettlementId == settlementId).FullPath;

                var procedureLocationPaths = from p in this.unitOfWork.DbContext.Set<Procedure>().Where(procedurePredicate)
                                             join pl in this.unitOfWork.DbContext.Set<ProcedureLocation>() on p.ProcedureId equals pl.ProcedureId
                                             join c in this.unitOfWork.DbContext.Set<Country>() on pl.CountryId equals c.CountryId into j1
                                             from c in j1.DefaultIfEmpty()
                                             join n1 in this.unitOfWork.DbContext.Set<Nuts1>() on pl.Nuts1Id equals n1.Nuts1Id into j2
                                             from n1 in j2.DefaultIfEmpty()
                                             join n2 in this.unitOfWork.DbContext.Set<Nuts2>() on pl.Nuts2Id equals n2.Nuts2Id into j3
                                             from n2 in j3.DefaultIfEmpty()
                                             join d in this.unitOfWork.DbContext.Set<District>() on pl.DistrictId equals d.DistrictId into j4
                                             from d in j4.DefaultIfEmpty()
                                             join m in this.unitOfWork.DbContext.Set<Municipality>() on pl.MunicipalityId equals m.MunicipalityId into j5
                                             from m in j5.DefaultIfEmpty()
                                             join s in this.unitOfWork.DbContext.Set<Settlement>() on pl.SettlementId equals s.SettlementId into j6
                                             from s in j6.DefaultIfEmpty()
                                             group new
                                             {
                                                 Path = c.NutsCode ?? n1.FullPath ?? n2.FullPath ?? d.FullPath ?? m.FullPath ?? s.FullPath ?? string.Empty,
                                             }
                                             by p.ProcedureId
                                             into g
                                             select new
                                             {
                                                 ProcedureId = g.Key,
                                                 Paths = g.Select(x => x.Path),
                                             };

                var procedureIds = procedureLocationPaths.Where(p => p.Paths.Any(pp => settlementPath.Contains(pp))).Select(p => p.ProcedureId).ToList();

                procedurePredicate = PredicateBuilder.True<Procedure>().And(p => procedureIds.Contains(p.ProcedureId));
            }

            var primaryProcedureSharePredicate = PredicateBuilder.True<ProcedureShare>()
                .And(ps => ps.IsPrimary == true);

            var projectPredicate = PredicateBuilder.True<Project>().And(p => p.RegistrationStatus == ProjectRegistrationStatus.Registered);

            var procedures = (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(procedurePredicate)

                              join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId

                              join tl in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>().Where(tl => tl.EndDate >= DateTime.Now) on p.ProcedureId equals tl.ProcedureId

                              join pps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(primaryProcedureSharePredicate) on p.ProcedureId equals pps.ProcedureId
                              join prg in this.unitOfWork.DbContext.Set<Programme>() on pps.ProgrammeId equals prg.MapNodeId

                              join iawp in this.unitOfWork.DbContext.Set<ProcedureIndicativeAnnualWorkingProgramme>() on p.ProcedureId equals iawp.ProcedureId

                              join iawpc in this.unitOfWork.DbContext.Set<ProcedureIndicativeAnnualWorkingProgrammeCandidate>() on iawp.ProcedureIndicativeAnnualWorkingProgrammeId equals iawpc.ProcedureIndicativeAnnualWorkingProgrammeId into j2
                              from iawpc in j2.DefaultIfEmpty()
                              join ct in this.unitOfWork.DbContext.Set<CompanyType>() on iawpc.CompanyTypeId equals ct.CompanyTypeId into j3
                              from ct in j3.DefaultIfEmpty()
                              join clt in this.unitOfWork.DbContext.Set<CompanyLegalType>() on iawpc.CompanyLegalTypeId equals clt.CompanyLegalTypeId into j4
                              from clt in j4.DefaultIfEmpty()

                              join iawpb in this.unitOfWork.DbContext.Set<ProcedureIndicativeAnnualWorkingProgrammeCompany>() on iawp.ProcedureIndicativeAnnualWorkingProgrammeId equals iawpb.ProcedureIndicativeAnnualWorkingProgrammeId into j5
                              from iawpb in j5.DefaultIfEmpty()
                              join c in this.unitOfWork.DbContext.Set<Company>() on iawpb.CompanyId equals c.CompanyId into j6
                              from c in j6.DefaultIfEmpty()
                              group new
                              {
                                  ProcedureShare = ps,
                                  EndDate = tl.EndDate,
                                  Company = new
                                  {
                                      Name = c.Name,
                                      NameAlt = c.NameAlt,
                                  },
                                  Candidate = new
                                  {
                                      TypeName = ct.Name,
                                      TypeNameAlt = ct.NameAlt,
                                      TypeId = iawpc.CompanyTypeId,
                                      LegalTypeName = clt.Name,
                                      LegalTypeNameAlt = clt.NameAlt,
                                      LegalTypeId = iawpc.CompanyLegalTypeId,
                                  },
                              }
                              by new
                              {
                                  ProcedureId = p.ProcedureId,
                                  ProcedureGid = p.Gid,
                                  ProcedureName = p.Name,
                                  ProcedureNameAlt = p.NameAlt,
                                  ProgrammeName = prg.Name,
                                  ProgrammeNameAlt = prg.NameAlt,
                                  ProcedureStatus = p.ProcedureStatus,
                                  MaxPercentCoFinancing = iawp.MaxPercentCoFinancing,
                                  EligibleActivities = iawp.EligibleActivities,
                                  EligibleActivitiesAlt = iawp.EligibleActivitiesAlt,
                                  EligibleCosts = iawp.EligibleCosts,
                                  EligibleCostsAlt = iawp.EligibleCostsAlt,
                              }
                              into g
                              select new
                              {
                                  ProcedureId = g.Key.ProcedureId,
                                  ProcedureGid = g.Key.ProcedureGid,
                                  ProcedureName = g.Key.ProcedureName,
                                  ProcedureNameAlt = g.Key.ProcedureNameAlt,
                                  ProgrammeName = g.Key.ProgrammeName,
                                  ProgrammeNameAlt = g.Key.ProgrammeNameAlt,
                                  EndingDate = g.Min(b => b.EndDate),
                                  Status = g.Key.ProcedureStatus,
                                  BudgetTotal = g.Select(b => b.ProcedureShare).Distinct().Sum(b => b.BgAmount + b.EuAmount),
                                  MaxPercentCoFinancing = g.Key.MaxPercentCoFinancing,
                                  EligibleActivities = g.Key.EligibleActivities,
                                  EligibleActivitiesAlt = g.Key.EligibleActivitiesAlt,
                                  EligibleCosts = g.Key.EligibleCosts,
                                  EligibleCostsAlt = g.Key.EligibleCostsAlt,
                                  Companies = g.Select(b => new
                                  {
                                      Name = b.Company.Name,
                                      NameAlt = b.Company.NameAlt,
                                  })
                                  .Distinct(),
                                  Candidates = g.Select(b => new
                                  {
                                      TypeName = b.Candidate.TypeName,
                                      TypeNameAlt = b.Candidate.TypeNameAlt,
                                      TypeId = b.Candidate.TypeId,
                                      LegalTypeName = b.Candidate.LegalTypeName,
                                      LegalTypeNameAlt = b.Candidate.LegalTypeNameAlt,
                                      LegalTypeId = b.Candidate.LegalTypeId,
                                  })
                                  .Distinct(),
                              })
                              .ToList()
                              .Where(p => p.Candidates.Any(c => (companyTypeId == null || c.TypeId == companyTypeId) && (companyLegalTypeId == null || c.LegalTypeId == companyLegalTypeId)))
                              .Select(p => new
                              {
                                  ProcedureId = p.ProcedureId,
                                  ProcedureGid = p.ProcedureGid,
                                  ProcedureName = p.ProcedureName,
                                  ProcedureNameAlt = p.ProcedureNameAlt,
                                  ProgrammeName = p.ProgrammeName,
                                  ProgrammeNameAlt = p.ProgrammeNameAlt,
                                  EndingDate = p.EndingDate,
                                  Status = p.Status,
                                  BudgetTotal = p.BudgetTotal,
                                  MaxPercentCoFinancing = p.MaxPercentCoFinancing,
                                  EligibleActivities = this.GetTransName(p.EligibleActivities, p.EligibleActivitiesAlt),
                                  EligibleCosts = this.GetTransName(p.EligibleCosts, p.EligibleCostsAlt),
                                  Companies = p.Companies.Where(c => !string.IsNullOrWhiteSpace(this.GetTransName(c.Name, c.NameAlt)))
                                  .Select(c => this.GetTransName(c.Name, c.NameAlt)),
                                  Candidates = p.Candidates.Where(c => !string.IsNullOrWhiteSpace(this.GetTransName(c.TypeName, c.TypeNameAlt)))
                                  .Select(c => this.GetTransName(c.TypeName, c.TypeNameAlt) + (!string.IsNullOrWhiteSpace(this.GetTransName(c.LegalTypeName, c.LegalTypeNameAlt)) ? $"({this.GetTransName(c.LegalTypeName, c.LegalTypeNameAlt)})" : string.Empty)),
                              })
                              .Select(p => new ProcedureVO()
                              {
                                  ProcedureId = p.ProcedureId,
                                  ProcedureGid = p.ProcedureGid,
                                  ProcedureName = p.ProcedureName,
                                  ProcedureNameAlt = p.ProcedureNameAlt,
                                  ProgrammeName = p.ProgrammeName,
                                  ProgrammeNameAlt = p.ProgrammeNameAlt,
                                  EndingDate = p.EndingDate,
                                  Status = p.Status,
                                  BudgetTotal = p.BudgetTotal,
                                  MaxPercentCoFinancing = p.MaxPercentCoFinancing,
                                  EligibleActivities = p.EligibleActivities,
                                  EligibleCosts = p.EligibleCosts,
                                  Candidates =
                                  string.Join($",{Environment.NewLine}", p.Companies) +
                                  (p.Candidates.Any() && p.Companies.Any() ? $",{Environment.NewLine}" : string.Empty) +
                                  string.Join($",{Environment.NewLine}", p.Candidates),
                              })
                              .OrderByDescending(p => p.Status)
                              .ThenBy(p => p.EndingDate)
                              .ToList();

            var proceduresWithOffsetAndLimit = procedures
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = new PageVO<ProcedureVO>() { Count = procedures.Count(), Results = proceduresWithOffsetAndLimit };

            return result;
        }

        private string GetTransName(string name, string nameAlt)
        {
            if (string.IsNullOrEmpty(nameAlt))
            {
                return name;
            }

            return SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English ? nameAlt : name;
        }
    }
}
