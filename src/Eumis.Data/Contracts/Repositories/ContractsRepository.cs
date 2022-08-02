using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Contracts.Views;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Users;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractsRepository : AggregateRepository<Contract>, IContractsRepository
    {
        public ContractsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Contract, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Contract, object>>[]
                {
                    c => c.ContractRegistrations.Select(e => e.File),
                    c => c.ContractPartners,
                    c => c.ContractLocations,
                    c => c.ContractBudgetLevel3Amounts,
                    c => c.ContractActivities.Select(ca => ca.ContractActivityCompanies),
                    c => c.ContractContractors,
                    c => c.ContractContracts.Select(cc => cc.ContractSubcontracts),
                    c => c.ContractContracts.Select(cc => cc.ContractContractActivities),
                    c => c.ContractProcurementPlans.Select(cc => cc.ContractDifferentiatedPositions),
                    c => c.ContractIndicators,
                    c => c.ContractGrantDocuments.Select(cd => cd.File),
                    c => c.ContractProcurementDocuments.Select(ccd => ccd.File),
                    c => c.ContractUsers,
                };
            }
        }

        public Contract Find(Guid gid)
        {
            var c = this.Set().Where(t => t.Gid == gid).SingleOrDefault();

            if (c == null)
            {
                throw new DataObjectNotFoundException(typeof(Contract).Name, gid);
            }

            return c;
        }

        public async Task<Contract> FindAsync(Guid gid, CancellationToken ct)
        {
            var c = await this.Set().Where(t => t.Gid == gid).SingleOrDefaultAsync(ct);

            if (c == null)
            {
                throw new DataObjectNotFoundException(typeof(Contract).Name, gid);
            }

            return c;
        }

        public Contract FindByRegNumber(string regNumber)
        {
            return this.Set()
                .Where(p => p.RegNumber == regNumber)
                .SingleOrDefault();
        }

        public IList<ContractVO> GetContracts(
            int[] programmeIds,
            int? programmePriorityId,
            int? procedureId = null,
            bool includeDrafts = false,
            string contractNumber = null,
            int? userId = null)
        {
            var predicate = PredicateBuilder.True<Contract>();

            var externalUserPredicate = predicate.AndStringContains(c => c.RegNumber, contractNumber);

            predicate = externalUserPredicate
                .AndEquals(c => c.ProcedureId, procedureId)
                .And(c => programmeIds.Contains(c.ProgrammeId));

            if (!includeDrafts)
            {
                predicate = predicate.And(c => c.ContractStatus != ContractStatus.Draft);
            }

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                            select ps.ProcedureId).Distinct();

            var externalVerificatorContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                               join c in this.unitOfWork.DbContext.Set<Contract>().Where(externalUserPredicate) on cu.ContractId equals c.ContractId
                                               join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

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
                                                   c.TotalBfpAmount,
                                                   c.TotalSelfAmount,
                                               };

            return (from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where programmeIds.Contains(c.ProgrammeId) && (procedureId.HasValue || subquery.Contains(p.ProcedureId))
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
                        c.TotalBfpAmount,
                        c.TotalSelfAmount,
                    })
                    .ToList()
                    .Union(externalVerificatorContracts)
                    .Select(o => new ContractVO
                    {
                        ContractId = o.ContractId,
                        ProcedureId = o.ProcedureId,
                        ProgrammeId = o.ProgrammeId,
                        ContractStatus = o.ContractStatus,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                        TotalBfpAmount = o.TotalBfpAmount,
                        TotalSelfAmount = o.TotalSelfAmount,
                    })
                    .Distinct()
                    .ToList();
        }

        public IList<ContractPhysicalExecutionActivityVO> GetContractPhysicalExecutionActivitiesForProjectDossier(int contractId)
        {
            var financialReports = (
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                join crf in this.unitOfWork.DbContext.Set<ContractReportFinancial>() on cr.ContractReportId equals crf.ContractReportId
                where cr.ContractId == contractId && cr.Status == ContractReportStatus.Accepted && crf.Status == ContractReportFinancialStatus.Actual
                select crf)
                .ToArray();

            var financialReportTotalAmounts = new List<(string gid, decimal totalAmount)>();
            foreach (var financialReport in financialReports)
            {
                var financialReportDoc = financialReport.GetDocument();
                financialReportTotalAmounts.AddRange(financialReportDoc
                    .CostSupportingDocuments
                    .CostSupportingDocumentCollection
                    .SelectMany(csd => csd.FinanceReportBudgetItemDataCollection)
                    .Select(frbi => (gid: frbi.ContractActivity.Id, totalAmount: frbi.TotalAmount))
                    .ToArray());
            }

            var financialReportTotalAmountsLookup = financialReportTotalAmounts
                .GroupBy(ta => ta.gid)
                .Select(g => new
                {
                    Gid = g.Key,
                    TotalAmounts = g.Sum(s => s.totalAmount),
                })
                .ToLookup(ta => ta.Gid);

            var lastAcceptedContractReportWithTechnicalReport =
                from cr in this.unitOfWork.DbContext.Set<ContractReport>()
                join crt in this.unitOfWork.DbContext.Set<ContractReportTechnical>() on cr.ContractReportId equals crt.ContractReportId
                where cr.ContractId == contractId && cr.Status == ContractReportStatus.Accepted && crt.Status == ContractReportTechnicalStatus.Actual
                group new { cr.ContractReportId, cr.OrderNum } by cr.ContractId into g
                select g.OrderByDescending(t => t.OrderNum).FirstOrDefault().ContractReportId;

            var technicalReport = this.unitOfWork.DbContext.Set<ContractReportTechnical>()
                .Where(t => lastAcceptedContractReportWithTechnicalReport.Contains(t.ContractReportId) && t.Status == ContractReportTechnicalStatus.Actual)
                .SingleOrDefault();

            if (technicalReport != null)
            {
                var technicalReportDoc = technicalReport.GetDocument();
                var technicalReportActivities = technicalReportDoc
                    .Activities
                    .ActivityCollection
                    .Select(ca => new
                    {
                        Gid = ca.BFPContractActivity.gid,
                        ca.Status.Description,
                        ca.BFPContractActivity.StartDate,
                        ca.BFPContractActivity.EndDate,
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
                var contractActivities = contractDoc
                    .BFPContractContractActivities
                    .BFPContractContractActivityCollection
                    .Select(ca => new
                    {
                        Gid = ca.gid,
                        ca.Code,
                        ca.Amount,
                    })
                    .ToArray();

                return (
                    from tra in technicalReportActivities
                    join ca in contractActivities on tra.Gid equals ca.Gid
                    select new ContractPhysicalExecutionActivityVO
                    {
                        ContractVersionId = activeContractVersion.ContractVersionId,
                        ContractId = activeContractVersion.ContractId,
                        ContractRegNum = activeContractVersion.ContractRegNum,
                        ActivityName = ca.Code,
                        StatusDesc = tra.Description,
                        StartDate = tra.StartDate,
                        EndDate = tra.EndDate,
                        Amount = ca.Amount,
                        TotalAmount = financialReportTotalAmountsLookup[tra.Gid].Any() ? financialReportTotalAmountsLookup[tra.Gid].First().TotalAmounts : 0,
                    })
                    .ToArray();
            }

            return new List<ContractPhysicalExecutionActivityVO>();
        }

        public IList<string> CanDeleteContract(int contractId)
        {
            var errors = new List<string>();

            var contract = this.FindWithoutIncludes(contractId);

            if (contract.ContractStatus != ContractStatus.Draft)
            {
                errors.Add("Не можете да изтриете договора, защото статуса е различен от 'Чернова'");
            }

            if (contract.ContractDate != null)
            {
                errors.Add("Не можете да изтриете договора, защото вече е бил активиран");
            }

            return errors;
        }

        public IList<ContractVO> GetUserAvailableContracts(int[] programmeIds, int userId, string contractNumber)
        {
            var predicate = PredicateBuilder.True<Contract>();

            predicate = predicate
                .AndStringContains(c => c.RegNumber, contractNumber)
                .And(c => c.ContractStatus != ContractStatus.Draft);

            var programmeContractsPredicate = predicate.And(t => programmeIds.Contains(t.ProgrammeId));

            var externalUserContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                        join c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate) on cu.ContractId equals c.ContractId
                                        select c;

            return (from c in this.unitOfWork.DbContext.Set<Contract>().Where(programmeContractsPredicate).Union(externalUserContracts)
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

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
                        c.TotalBfpAmount,
                        c.TotalSelfAmount,
                    })
                    .Distinct()
                    .ToList()
                    .Select(o => new ContractVO
                    {
                        ContractId = o.ContractId,
                        ProcedureId = o.ProcedureId,
                        ProgrammeId = o.ProgrammeId,
                        ContractStatus = o.ContractStatus,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                        TotalBfpAmount = o.TotalBfpAmount,
                        TotalSelfAmount = o.TotalSelfAmount,
                    }).ToList();
        }

        public PagePVO<ContractPVO> GetPortalContractsForRegistration(int contractRegistrationId, int offset = 0, int? limit = null)
        {
            var query = this.CreatePortalContractsQuery(ccr => ccr.IsActive && ccr.ContractRegistrationId == contractRegistrationId, c => true);

            return new PagePVO<ContractPVO>
            {
                Results = query
                    .OrderByDescending(c => c.ContractDate)
                    .ThenBy(c => c.ContractId)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList(),
                Count = query.Count(),
            };
        }

        public PagePVO<ContractPVO> GetPortalContractsForAccessCode(int contractId, int offset = 0, int? limit = null)
        {
            var query = this.CreatePortalContractsQuery(ccr => true, c => c.ContractId == contractId);

            return new PagePVO<ContractPVO>
            {
                Results = query
                    .OrderByDescending(c => c.ContractDate)
                    .ThenBy(c => c.ContractId)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList(),
                Count = query.Count(),
            };
        }

        public ContractPVO GetPortalContractForRegistration(Guid contractGid, int contractRegistrationId)
        {
            return this.CreatePortalContractsQuery(ccr => ccr.IsActive && ccr.ContractRegistrationId == contractRegistrationId, c => c.Gid == contractGid).Single();
        }

        public ContractPVO GetPortalContractForAccessCode(Guid contractGid, int contractId)
        {
            return this.CreatePortalContractsQuery(c => true, c => c.Gid == contractGid && c.ContractId == contractId).Single();
        }

        private IQueryable<ContractPVO> CreatePortalContractsQuery(
            Expression<Func<ContractsContractRegistration, bool>> regPredicate,
            Expression<Func<Contract, bool>> contractPredicate)
        {
            var predicate = contractPredicate;

            if (!regPredicate.IsTrueLambdaExpr())
            {
                // if the contract regs predicate is non trivial (of the type (cr => True))
                // append it to the contracts predicate
                var contractsContractRegistrationsSet = this.unitOfWork.DbContext.Set<ContractsContractRegistration>().AsQueryable();
                predicate = predicate.And(c =>
                    contractsContractRegistrationsSet
                    .Where(regPredicate)
                    .Where(ccr => ccr.ContractId == c.ContractId)
                    .Any());
            }

            var contractsSet = this.unitOfWork.DbContext.Set<Contract>().AsQueryable();

            IQueryable<Contract> contracts;
            if (predicate.IsTrueLambdaExpr())
            {
                contracts = contractsSet;
            }
            else
            {
                // if the contract predicate is non trivial (of the type (cr => True))
                // filter the contracts set with it
                contracts = contractsSet.Where(predicate);
            }

            return from c in contracts
                   join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(ps1 => ps1.IsPrimary) on c.ProcedureId equals ps.ProcedureId
                   join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId
                   select new ContractPVO
                   {
                       ContractId = c.ContractId,
                       Gid = c.Gid,
                       ContractDate = c.ContractDate,
                       RegistrationNumber = c.RegNumber,
                       ProgrammeName = prog.Name,
                       ProcedureName = p.Name,
                       ProcedureCode = p.Code,
                       ProjectName = c.Name,
                       CompanyName = c.CompanyName,
                   };
        }

        public int GetProgrammeId(int contractId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == contractId
                    select c.ProgrammeId).SingleOrDefault();
        }

        public ContractStatus GetContractStatus(int contractId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == contractId
                    select c.ContractStatus).Single();
        }

        public int GetProcedureId(int contractId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == contractId
                    select c.ProcedureId).SingleOrDefault();
        }

        public string GetRegNumber(int contractId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == contractId
                    select c.RegNumber).SingleOrDefault();
        }

        public bool ProjectHasContractForProgramme(int projectId, int programmeId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ProjectId == projectId && c.ProgrammeId == programmeId
                    select c.ContractId).Any();
        }

        public IList<ContractContractRegistrationsVO> GetContractContractRegistrations(int contractId)
        {
            return (from ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>()
                    join cr in this.unitOfWork.DbContext.Set<ContractRegistration>() on ccr.ContractRegistrationId equals cr.ContractRegistrationId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ccr.ContractId equals c.ContractId
                    where ccr.ContractId == contractId
                    select new ContractContractRegistrationsVO
                    {
                        ContractsContractRegistrationId = ccr.ContractsContractRegistrationId,
                        Email = cr.Email,
                        FirstName = cr.FirstName,
                        LastName = cr.LastName,
                        Phone = cr.Phone,
                        IsActive = ccr.IsActive,
                        Version = c.Version,
                        File = new FileVO
                        {
                            Key = ccr.BlobKey,
                        },
                    })
                    .ToList();
        }

        public IList<ContractGrantDocumentsVO> GetContractGrantDocuments(int contractId)
        {
            return (from cd in this.unitOfWork.DbContext.Set<ContractGrantDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on cd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where cd.ContractId == contractId
                    select new ContractGrantDocumentsVO
                    {
                        ContractId = cd.ContractId,
                        ContractGrantDocumentId = cd.ContractGrantDocumentId,
                        Name = cd.Name,
                        Description = cd.Description,
                        File = (b.Key == null) ? null : new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                    }).ToList();
        }

        public IList<ContractProcurementDocumentsVO> GetContractProcurementDocuments(int contractId)
        {
            return (from ccd in this.unitOfWork.DbContext.Set<ContractProcurementDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on ccd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where ccd.ContractId == contractId
                    select new ContractProcurementDocumentsVO
                    {
                        ContractId = ccd.ContractId,
                        ContractProcurementDocumentId = ccd.ContractProcurementDocumentId,
                        Name = ccd.Name,
                        Description = ccd.Description,
                        File = (b.Key == null) ? null : new FileVO
                        {
                            Key = b.Key,
                            Name = b.FileName,
                        },
                    }).ToList();
        }

        public int GetContractContractRegistrationId(Guid contractGid, int contractRegistrationId)
        {
            return (from ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ccr.ContractId equals c.ContractId
                    where c.Gid == contractGid && ccr.ContractRegistrationId == contractRegistrationId
                    select ccr.ContractsContractRegistrationId)
                .Single();
        }

        public IList<string> GetContractAccessCodesEmails(Guid contractGid)
        {
            return (from ac in this.unitOfWork.DbContext.Set<VwAccessCode>()
                    where ac.ContractGid == contractGid
                    select ac.Email)
                .ToList();
        }

        public Task<VwAccessCode> GetContractAccessCodeAsync(string email, string regNumber, string code)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    join ac in this.unitOfWork.DbContext.Set<VwAccessCode>() on cv.ContractId equals ac.ContractId
                    where cv.RegNumber == regNumber && ac.Email == email && ac.Code == code
                    select ac)
                    .Distinct()
                    .SingleOrDefaultAsync();
        }

        public int GetContractId(Guid contractGid)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.Gid == contractGid
                    select c.ContractId)
                   .Single();
        }

        public async Task<int> GetContractIdAsync(Guid contractGid, CancellationToken ct)
        {
            var result = await (from c in this.unitOfWork.DbContext.Set<Contract>()
                                where c.Gid == contractGid
                                select c.ContractId)
                                .SingleAsync(ct);

            return result;
        }

        public int GetContractContractContractId(int contractContractId)
        {
            return (from cc in this.unitOfWork.DbContext.Set<ContractContract>()
                    where cc.ContractContractId == contractContractId
                    select cc.ContractId).Single();
        }

        public ContractDataDO GetContractData(int projectId, int programmeId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Project>()
                    join pr in this.unitOfWork.DbContext.Set<Procedure>() on p.ProcedureId equals pr.ProcedureId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId

                    join c in this.unitOfWork.DbContext.Set<Company>() on prog.CompanyId equals c.CompanyId into g
                    from c in g.DefaultIfEmpty()

                    where p.ProjectId == projectId &&
                       ps.ProgrammeId == programmeId
                    group ps by new { p.RegNumber, prog.Code, c.Uin, UinType = (UinType?)c.UinType } into g
                    select new ContractDataDO
                    {
                        ProjectRegNumber = g.Key.RegNumber,
                        ProgrammeCode = g.Key.Code,
                        AuthorityUin = g.Key.Uin,
                        AuthorityUinType = g.Key.UinType,
                        IsPrimaryProgramme = g.Any(ps1 => ps1.IsPrimary),
                    })
                    .Single();
        }

        public ContractDataDO GetContractData(int contractId)
        {
            return (from p in this.unitOfWork.DbContext.Set<Project>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on p.ProjectId equals c.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals pr.ProcedureId
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pr.ProcedureId equals ps.ProcedureId
                    join prog in this.unitOfWork.DbContext.Set<Programme>() on ps.ProgrammeId equals prog.MapNodeId

                    join company in this.unitOfWork.DbContext.Set<Company>() on prog.CompanyId equals company.CompanyId into g
                    from company in g.DefaultIfEmpty()

                    where c.ContractId == contractId && prog.MapNodeId == c.ProgrammeId
                    group ps by new
                    {
                        ProjectRegNumber = p.RegNumber,
                        ProgrammeCode = prog.Code,
                        company.Uin,
                        UinType = (UinType?)company.UinType,
                        c.ProgrammeId,
                        ContractRegNumber = c.RegNumber,
                        ProgrammeName = prog.Name,
                        ProcedureName = pr.Name,
                        ProcedureCode = pr.Code,
                        c.Name,
                    }
                    into g
                    select new ContractDataDO
                    {
                        ProjectRegNumber = g.Key.ProjectRegNumber,
                        ProgrammeCode = g.Key.ProgrammeCode,
                        AuthorityUin = g.Key.Uin,
                        AuthorityUinType = g.Key.UinType,
                        IsPrimaryProgramme = g.Any(ps1 => ps1.IsPrimary),
                        ContractRegNumber = g.Key.ContractRegNumber,
                        ProgrammeId = g.Key.ProgrammeId,
                        ProgrammeName = g.Key.ProgrammeName,
                        ProcedureName = g.Key.ProcedureName,
                        ProcedureCode = g.Key.ProcedureCode,
                        ContractName = g.Key.Name,
                    }).Single();
        }

        public bool IsContractNumExisting(string contractNum, int? procedureId, int? projectId, int? programmeId = null)
        {
            var predicate = PredicateBuilder.True<Contract>()
                .AndStringMatches(p => p.RegNumber, contractNum, true)
                .AndEquals(p => p.ProcedureId, procedureId)
                .AndEquals(p => p.ProjectId, projectId)
                .AndEquals(c => c.ProgrammeId, programmeId);

            return (from p in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                    select p.ContractId).Any();
        }

        public IList<InterventionCategory> GetInterventionCategories(IList<Tuple<Dimension, string>> categories)
        {
            var cts = categories.Select(c => Enum.GetName(typeof(Dimension), c.Item1) + "$$" + c.Item2);

            return (from c in this.unitOfWork.DbContext.Set<InterventionCategory>()
                    where cts.Contains(c.Dimension.ToString() + "$$" + c.Code)
                    select c)
                    .ToList();
        }

        public IList<ContractItemVO> GetContractItems(int programmeId, int[] exceptIds)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    where !exceptIds.Contains(c.ContractId) && c.ProgrammeId == programmeId && c.ContractStatus == ContractStatus.Entered
                    orderby c.CreateDate descending
                    select new
                    {
                        c.ContractId,
                        ProcedureName = p.Name,
                        c.Name,
                        c.RegNumber,
                        c.ContractDate,
                        c.ExecutionStatus,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    }).ToList()
                    .Select(o => new ContractItemVO
                    {
                        ItemId = o.ContractId,
                        ProcedureName = o.ProcedureName,
                        Name = o.Name,
                        RegNumber = o.RegNumber,
                        ContractDate = o.ContractDate,
                        ExecutionStatus = o.ExecutionStatus,
                        Company = string.Format("{0} ({1}: {2})", o.CompanyName, o.CompanyUinType.GetEnumDescription(), o.CompanyUin),
                    }).ToList();
        }

        public IList<ContractContractItemVO> GetContractContractItems(int contractId, int[] exceptIds)
        {
            return (from cc in this.unitOfWork.DbContext.Set<ContractContract>()
                    join ccr in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals ccr.ContractContractorId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cc.ContractId equals c.ContractId
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId

                    join country in this.unitOfWork.DbContext.Set<Country>() on ccr.SeatCountryId equals country.CountryId into g2
                    from country in g2.DefaultIfEmpty()

                    join set in this.unitOfWork.DbContext.Set<Settlement>() on ccr.SeatSettlementId equals set.SettlementId into g3
                    from set in g3.DefaultIfEmpty()

                    where !exceptIds.Contains(cc.ContractContractId) && c.ContractId == contractId
                    select new
                    {
                        cc.ContractContractId,
                        cc.SignDate,
                        cc.Number,
                        ccr.Uin,
                        ccr.UinType,
                        ccr.Name,
                        ccr.SeatPostCode,
                        ccr.SeatStreet,
                        ccr.SeatAddress,
                        CountryName = country.Name,
                        CountryNutsCode = country.NutsCode,
                        SettlementName = set.Name,
                        ProcedureName = p.Name,
                        ContractName = c.Name,
                        ContractRegNumber = c.RegNumber,
                        c.CompanyName,
                        c.CompanyUinType,
                        c.CompanyUin,
                    })
                .ToList()
                .Select(t => new ContractContractItemVO()
                {
                    ItemId = t.ContractContractId,
                    SignDate = t.SignDate,
                    Number = t.Number,
                    ContractContractorCompany = string.Format("{0} ({1}: {2})", t.Name, t.UinType.GetEnumDescription(), t.Uin),
                    Seat = t.CountryNutsCode == "BG" ? t.SettlementName + " " + t.SeatPostCode + " " + t.SeatStreet : t.CountryName + " " + t.SeatAddress,
                    ProcedureName = t.ProcedureName,
                    ContractName = t.ContractName,
                    ContractRegNumber = t.ContractRegNumber,
                    ContractCompany = string.Format("{0} ({1}: {2})", t.CompanyName, t.CompanyUinType.GetEnumDescription(), t.CompanyUin),
                }).ToList();
        }

        public PagePVO<ContractDifferentiatedPositionPVO> GetAnnouncedContractDifferentiatedPositions(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? offersDeadlineDate = null,
            DateTime? noticeDate = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            var results = this.GetContractDifferentiatedPositionsInternal(offset, limit, dpName, name, companyName, offersDeadlineDate, noticeDate, null, false, sortBy, sortOrder);
            return new PagePVO<ContractDifferentiatedPositionPVO>
            {
                Results = results.Item1,
                Count = results.Item2,
            };
        }

        public PagePVO<ContractDifferentiatedPositionPVO> GetArchivedContractDifferentiatedPositions(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null)
        {
            var results = this.GetContractDifferentiatedPositionsInternal(offset, limit, dpName, name, companyName, archived: true);
            return new PagePVO<ContractDifferentiatedPositionPVO>
            {
                Results = results.Item1,
                Count = results.Item2,
            };
        }

        public ContractDifferentiatedPositionPVO GetContractDifferentiatedPosition(Guid dpGid)
        {
            return this.GetContractDifferentiatedPositionsInternal(dpGid: dpGid).Item1.Single();
        }

        private Tuple<IList<ContractDifferentiatedPositionPVO>, int> GetContractDifferentiatedPositionsInternal(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? offersDeadlineDate = null,
            DateTime? noticeDate = null,
            Guid? dpGid = null,
            bool? archived = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            DateTime currentDate = DateTime.Now.Date;

            var dpPredicate = PredicateBuilder.True<ContractDifferentiatedPosition>()
                .AndEquals(c => c.Gid, dpGid)
                .AndStringContains(t => t.Name, dpName);

            var cppPredicate = PredicateBuilder.True<ContractProcurementPlan>()
                .AndStringContains(t => t.Name, name)
                .And(x => x.AnnouncedDate.HasValue)
                .AndDateTimeLessThanOrEqual(t => t.OffersDeadlineDate, offersDeadlineDate?.Date.AddDays(1).AddMilliseconds(-1))
                .AndDateTimeGreaterThanOrEqual(t => t.NoticeDate, noticeDate?.Date);

            if (archived.HasValue)
            {
                cppPredicate = archived.Value ?
                  cppPredicate.And(cpp => DbFunctions.DiffDays(currentDate, cpp.OffersDeadlineDate) < 0 || cpp.TerminatedDate.HasValue) :
                  cppPredicate.And(cpp => DbFunctions.DiffDays(currentDate, cpp.OffersDeadlineDate) >= 0 && !cpp.TerminatedDate.HasValue);
            }

            var cPredicate = PredicateBuilder.True<Contract>()
                .AndStringContains(t => t.CompanyName, companyName);

            var query = (from cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>().Where(dpPredicate)
                         join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>().Where(cppPredicate) on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                         join ea in this.unitOfWork.DbContext.Set<ErrandArea>() on cpp.ErrandAreaId equals ea.ErrandAreaId
                         join ela in this.unitOfWork.DbContext.Set<ErrandLegalAct>().Where(x => x.Gid == ErrandLegalAct.PmsGid) on cpp.ErrandLegalActId equals ela.ErrandLegalActId
                         join et in this.unitOfWork.DbContext.Set<ErrandType>() on cpp.ErrandTypeId equals et.ErrandTypeId
                         join c in this.unitOfWork.DbContext.Set<Contract>().Where(cPredicate) on cpp.ContractId equals c.ContractId

                         orderby cpp.Name descending
                         select new
                         {
                             cdf.ContractDifferentiatedPositionId,
                             c.CompanyName,
                             c.CompanyUin,
                             c.CompanyUinType,
                             cpp.Name,
                             ErrandAreaCode = ea.Code,
                             ErrandAreaName = ea.Name,
                             ErrandLegalActGid = ela.Gid,
                             ErrandLegalActName = ela.Name,
                             ErrandTypeCode = et.Code,
                             ErrandTypeName = et.Name,
                             cpp.ExpectedAmount,
                             cpp.NoticeDate,
                             cpp.OffersDeadlineDate,
                             cpp.Description,
                             cpp.AnnouncedDate,
                             cpp.TerminatedDate,

                             DpGid = cdf.Gid,
                             DpName = cdf.Name,
                         })
                    .ToList()
                    .GroupBy(t => new
                    {
                        t.ContractDifferentiatedPositionId,
                        t.CompanyName,
                        t.CompanyUin,
                        t.CompanyUinType,
                        t.Name,
                        t.ErrandAreaCode,
                        t.ErrandAreaName,
                        t.ErrandLegalActGid,
                        t.ErrandLegalActName,
                        t.ErrandTypeCode,
                        t.ErrandTypeName,
                        t.ExpectedAmount,
                        t.NoticeDate,
                        t.OffersDeadlineDate,
                        t.Description,
                        t.AnnouncedDate,
                        t.TerminatedDate,

                        t.DpGid,
                        t.DpName,
                    });

            var count = query.Count();
            var results = query
                    .Select(t => new ContractDifferentiatedPositionPVO
                    {
                        CompanyName = t.Key.CompanyName,
                        CompanyUin = t.Key.CompanyUin,
                        CompanyUinType = new EnumPVO<UinType>()
                        {
                            Description = t.Key.CompanyUinType,
                            Value = t.Key.CompanyUinType,
                        },
                        Name = t.Key.Name,
                        ErrandArea = new EntityCodeNomVO()
                        {
                            Code = t.Key.ErrandAreaCode,
                            Name = t.Key.ErrandAreaName,
                        },
                        ErrandLegalAct = new EntityGidNomVO()
                        {
                            Gid = t.Key.ErrandLegalActGid,
                            Name = t.Key.ErrandLegalActName,
                        },
                        ErrandType = new EntityCodeNomVO
                        {
                            Code = t.Key.ErrandTypeCode,
                            Name = t.Key.ErrandTypeName,
                        },
                        ExpectedAmount = t.Key.ExpectedAmount,
                        NoticeDate = t.Key.NoticeDate,
                        OffersDeadlineDate = t.Key.OffersDeadlineDate,
                        Description = t.Key.Description,
                        AnnouncedDate = t.Key.AnnouncedDate,
                        TerminatedDate = t.Key.TerminatedDate,

                        DpGid = t.Key.DpGid,
                        DpName = t.Key.DpName,
                    })
                    .WithSort(sortBy, sortOrder)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();

            return new Tuple<IList<ContractDifferentiatedPositionPVO>, int>(results, count);
        }

        public ContractDifferentiatedPosition GetContractDifferentiatedPosition(int contractDifferentiatedPositionId)
        {
            return this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()
                .Where(t => t.ContractDifferentiatedPositionId == contractDifferentiatedPositionId)
                .Single();
        }

        public ContractDifferentiatedPosition FindContractDifferentiatedPosition(Guid dpGid)
        {
            return this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()
                .Where(t => t.Gid == dpGid)
                .Single();
        }

        public (string ContractVersionXml, string ContractProcurementXml, Guid ContractGid, Guid ProcurementsGid, Guid PlanGid, int ContractDifferentiatedPositionId) GetInfoForContractDifferentiatedPosition(Guid dpGid)
        {
            return (from cdp in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>()
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdp.ContractProcurementPlanId equals cpp.ContractProcurementPlanId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cpp.ContractId equals c.ContractId
                    join cv in this.unitOfWork.DbContext.Set<ContractVersionXml>() on c.ContractId equals cv.ContractId
                    join cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>() on c.ContractId equals cp.ContractId

                    where cdp.Gid == dpGid && cv.Status == ContractVersionStatus.Active && cp.Status == ContractProcurementStatus.Active
                    select new
                    {
                        cdp.ContractDifferentiatedPositionId,
                        ContractVersionXml = cv.Xml,
                        ContractProcurementXml = cp.Xml,
                        ContractGid = c.Gid,
                        ProcurementsGid = cp.Gid,
                        PlanGid = cpp.Gid,
                    })
                .ToList()
                .Select(t => (
                    t.ContractVersionXml,
                    t.ContractProcurementXml,
                    t.ContractGid,
                    t.ProcurementsGid,
                    t.PlanGid,
                    t.ContractDifferentiatedPositionId))
                .Single();
        }

        public IList<(int contractId, string regNumber, int programmeId)> GetEnteredContractData()
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractStatus == ContractStatus.Entered
                    select new { c.ContractId, c.RegNumber, c.ProgrammeId })
                    .ToList()
#pragma warning disable SA1101 // Prefix local calls with this // value tuples break this rule
                    .Select(c => (contractId: c.ContractId, regNumber: c.RegNumber, programmeId: c.ProgrammeId))
#pragma warning restore SA1101 // Prefix local calls with this
                    .ToList();
        }

        public ContractIndicator GetContractIndicator(int contractId, int contractIndicatorId)
        {
            return
                this.unitOfWork.DbContext.Set<ContractIndicator>()
                .Where(ci => ci.ContractId == contractId && ci.ContractIndicatorId == contractIndicatorId)
                .Single();
        }

        public IList<ContractUserVO> GetContractUsers(int contractId)
        {
            return (from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.ContractId == contractId)
                    join u in this.unitOfWork.DbContext.Set<User>() on cu.UserId equals u.UserId
                    select new ContractUserVO
                    {
                        ContractId = cu.ContractId,
                        ContractUserId = cu.ContractUserId,
                        UserId = cu.UserId,
                        Fullname = u.Fullname,
                        Username = u.Username,
                    }).ToList();
        }

        public bool IsUserAssociatedWithContract(int contractId, int userId)
        {
            return this.unitOfWork.DbContext.Set<ContractUser>()
                .Where(x => x.ContractId == contractId && x.UserId == userId)
                .Any();
        }

        public bool IsUserAssociatedWithAnyContract(int userId)
        {
            return this.unitOfWork.DbContext.Set<ContractUser>()
                .Where(x => x.UserId == userId)
                .Any();
        }

        public ContractInfoVO GetContractInfo(int contractId)
        {
            var info = (from c in this.Set().Where(x => x.ContractId == contractId)
                        join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                        join pas in this.unitOfWork.DbContext.Set<ProcedureApplicationSection>().Where(x => x.Section == ApplicationSectionType.Indicators) on p.ProcedureId equals pas.ProcedureId
                        select new ContractInfoVO
                        {
                            ContractId = c.ContractId,
                            Name = c.Name,
                            ContractStatus = c.ContractStatus,
                            ProcedureKind = p.ProcedureKind,
                            RegNumber = c.RegNumber,
                            IsIndicatorSectionVisible = pas.IsSelected,
                        }).Single();

            return info;
        }

        public int GetContractProgrammePriority(int contractId)
        {
            return (from cbl3a in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                    join pl2 in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cbl3a.ProcedureBudgetLevel2Id equals pl2.ProcedureBudgetLevel2Id
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pl2.ProcedureShareId equals ps.ProcedureShareId

                    where cbl3a.ContractId == contractId && ps.IsPrimary
                    select ps.ProgrammePriorityId)
                .Distinct()
                .Single();
        }
    }
}
