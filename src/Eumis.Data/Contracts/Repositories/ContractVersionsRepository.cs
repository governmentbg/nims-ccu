using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Eumis.Common.Db;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractVersionsRepository : AggregateRepository<ContractVersionXml>, IContractVersionsRepository
    {
        private static decimal euroExchangeRates = 1.95583m;

        public ContractVersionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractVersionXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractVersionXml, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public ContractVersionXml FindWithIncludedAmounts(int contractVersionXmlId)
        {
            return this.unitOfWork.DbContext.Set<ContractVersionXml>()
                .Where(t => t.ContractVersionXmlId == contractVersionXmlId)
                .Include(t => t.ContractVersionXmlAmounts)
                .Single();
        }

        public IList<ContractVersionVO> GetContractVersions(int contractId)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    where cv.ContractId == contractId
                    orderby cv.OrderNum descending
                    select new
                    {
                        cv.ContractVersionXmlId,
                        cv.ContractId,
                        cv.VersionType,
                        cv.VersionNum,
                        cv.VersionSubNum,
                        cv.RegNumber,
                        cv.CreateNote,
                        cv.TotalBfpAmount,
                        cv.ContractDate,
                        cv.Status,
                    }).ToList()
                    .Select(o => new ContractVersionVO()
                    {
                        ContractVersionId = o.ContractVersionXmlId,
                        ContractId = o.ContractId,
                        VersionType = o.VersionType,
                        VersionNumber = string.Format("{0}.{1}", o.VersionNum, o.VersionSubNum),
                        RegNumber = o.RegNumber,
                        CreateNote = o.CreateNote,
                        TotalBfpAmount = o.TotalBfpAmount,
                        ContractDate = o.ContractDate,
                        Status = o.Status,
                    }).ToList();
        }

        public IList<ContractVersionXml> GetNonArchivedVersions(int contractId)
        {
            return this.Set()
                .Where(p => p.ContractId == contractId && p.Status != ContractVersionStatus.Archived)
                .ToList();
        }

        public int GetVersionId(Guid gid)
        {
            return (from v in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    where v.Gid == gid
                    select v.ContractVersionXmlId).Single();
        }

        public int GetProjectId(int versionId)
        {
            return (from v in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on v.ContractId equals c.ContractId
                    where v.ContractVersionXmlId == versionId
                    select c.ProjectId).Single();
        }

        public ContractVersionXml Find(Guid xmlGid)
        {
            return this.Set()
                .Where(cv => cv.Gid == xmlGid)
                .SingleOrDefault();
        }

        public ContractVersionXml FindForUpdate(Guid xmlGid, byte[] version)
        {
            var contractVersion = this.Find(xmlGid);

            this.CheckVersion(contractVersion.Version, version);

            return contractVersion;
        }

        public ContractVersionXml GetLastVersion(int contractId)
        {
            return this.Set()
                .Where(cv => cv.ContractId == contractId)
                .OrderByDescending(c => c.VersionNum)
                .ThenByDescending(c => c.VersionSubNum)
                .First();
        }

        public ContractVersionXml GetActiveVersion(int contractId)
        {
            return this.Set().Single(cv => cv.ContractId == contractId && cv.Status == ContractVersionStatus.Active);
        }

        public async Task<ContractVersionXml> GetActiveVersionAsync(int contractId, CancellationToken ct)
        {
            return await this.Set().SingleAsync(cv => cv.ContractId == contractId && cv.Status == ContractVersionStatus.Active, ct);
        }

        public string GetActualVersionXml(Guid contractGid)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cv.ContractId equals c.ContractId
                    where c.Gid == contractGid && cv.Status == ContractVersionStatus.Active
                    select cv.Xml)
                .SingleOrDefault();
        }

        public ContractVersionXml FindForDraftContract(int contractId)
        {
            return this.Set()
                .Where(cv => cv.ContractId == contractId)
                .SingleOrDefault();
        }

        public ContractVersionXml FindForDraftContractForUpdate(int contractId, byte[] version)
        {
            var contractVersion = this.FindForDraftContract(contractId);

            this.CheckVersion(contractVersion.Version, version);

            return contractVersion;
        }

        public string GetAnnexRegNumber(int contractId, int versionNum)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    where cv.ContractId == contractId && cv.VersionNum == versionNum && cv.VersionType == ContractVersionType.Annex
                    select cv.RegNumber).SingleOrDefault();
        }

        public bool HasContractVersionsInProgress(int contractId)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    where cv.ContractId == contractId && !ContractVersionXml.FinalStatuses.Contains(cv.Status)
                    select cv.ContractVersionXmlId).Any();
        }

        public bool HasContractBlockingVersionsInProgress(int contractId)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    where cv.ContractId == contractId && !ContractVersionXml.FinalStatuses.Contains(cv.Status) && !ContractVersionXml.NonBlockingTypes.Contains(cv.VersionType)
                    select cv.ContractVersionXmlId).Any();
        }

        public async Task<bool> HasContractBlockingVersionsInProgressAsync(int contractId, CancellationToken ct)
        {
            var result = await (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                                where cv.ContractId == contractId && !ContractVersionXml.FinalStatuses.Contains(cv.Status) && !ContractVersionXml.NonBlockingTypes.Contains(cv.VersionType)
                                select cv.ContractVersionXmlId).AnyAsync(ct);

            return result;
        }

        public int GetContractId(int versionId)
        {
            return (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                    where cv.ContractVersionXmlId == versionId
                    select cv.ContractId).SingleOrDefault();
        }

        public new void Remove(ContractVersionXml contractVersion)
        {
            if (contractVersion.Status != ContractVersionStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete non draft contract version");
            }

            base.Remove(contractVersion);
        }

        public PagePVO<ContractVersionPVO> GetPortalContractVersions(Guid contractGid, int offset = 0, int? limit = null)
        {
            var query = from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>()
                        join c in this.unitOfWork.DbContext.Set<Contract>() on cv.ContractId equals c.ContractId
                        where c.Gid == contractGid && ContractVersionXml.FinalStatuses.Contains(cv.Status)
                        orderby cv.OrderNum descending
                        select cv;

            return new PagePVO<ContractVersionPVO>
            {
                Results = query
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(cv => new ContractVersionPVO
                    {
                        Gid = cv.Gid,
                        VersionType = new EnumPVO<ContractVersionType>()
                        {
                            Description = cv.VersionType,
                            Value = cv.VersionType,
                        },
                        VersionNum = cv.VersionNum,
                        VersionSubNum = cv.VersionSubNum,
                        ContractDate = cv.ContractDate,
                        RegNumber = cv.RegNumber,
                        Status = new EnumPVO<ContractVersionStatus>()
                        {
                            Description = cv.Status,
                            Value = cv.Status,
                        },
                    }).ToList(),
                Count = query.Count(),
            };
        }

        public ContractVersionSAPDataVO GetContractVersionSAPData(int contractVersionId)
        {
            var contractId = this.GetContractId(contractVersionId);

            var firstContractVersionCode = (from cv in this.unitOfWork.DbContext.Set<ContractVersionXml>().Where(x => x.ContractId == contractId)
                                            group cv by cv.ContractId into g
                                            let minVersion = g.Min(x => x.OrderNum)
                                            from cv in g
                                            where minVersion == cv.OrderNum
                                            select cv.RegNumber)
                                            .Single();

            var prioritySourceAmounts = from cva in this.unitOfWork.DbContext.Set<ContractVersionXmlAmount>().Where(x => x.ContractVersionXmlId == contractVersionId && x.IsActive == true)
                                        join pb in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cva.ProcedureBudgetLevel2Id equals pb.ProcedureBudgetLevel2Id
                                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pb.ProcedureShareId equals ps.ProcedureShareId
                                        join prpr in this.unitOfWork.DbContext.Set<MapNode>() on ps.ProgrammePriorityId equals prpr.MapNodeId
                                        group new
                                        {
                                            cva.CurrentBgAmount,
                                            cva.CurrentEuAmount,
                                            cva.CurrentSelfAmount,
                                        }
                                        by new
                                        {
                                            prpr.Code,
                                            cva.ContractVersionXmlId,
                                        }
                                        into g
                                        select new
                                        {
                                            BgAmount = g.Sum(cv => cv.CurrentBgAmount),
                                            EuAmount = g.Sum(cv => cv.CurrentEuAmount),
                                            SelfAmount = g.Sum(cv => cv.CurrentSelfAmount),
                                            TotalAmount = g.Sum(cv => cv.CurrentBgAmount + cv.CurrentEuAmount + cv.CurrentSelfAmount),
                                            BfpTotalAmount = g.Sum(cv => cv.CurrentBgAmount + cv.CurrentEuAmount),
                                            ProgrammePriorityCode = g.Key.Code,
                                            ContractVersionXmlId = g.Key.ContractVersionXmlId,
                                        };

            return (from cvx in this.unitOfWork.DbContext.Set<ContractVersionXml>().Where(x => x.ContractVersionXmlId == contractVersionId)
                    join pa in prioritySourceAmounts on cvx.ContractVersionXmlId equals pa.ContractVersionXmlId
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cvx.ContractId equals c.ContractId
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals proc.ProcedureId
                    join p in this.unitOfWork.DbContext.Set<Project>() on c.ProjectId equals p.ProjectId
                    join pr in this.unitOfWork.DbContext.Set<MapNode>() on c.ProgrammeId equals pr.MapNodeId
                    group pa by new
                    {
                        ContractVersionId = cvx.ContractVersionXmlId,
                        ContractId = c.ContractId,
                        ProgrammeCode = pr.Code,
                        ProcedureCode = proc.Code,
                        ProjectRegNumber = p.RegNumber,
                        CompanyNameBg = c.CompanyName,
                        CompanyNameEn = c.CompanyNameAlt,
                        CompanyUin = c.CompanyUin,
                        CompanyUinType = c.CompanyUinType,
                        StartDate = c.StartDate,
                        CompletionDate = c.CompletionDate,
                    }
                    into g
                    select new ContractVersionSAPDataVO
                    {
                        ContractVersionId = g.Key.ContractVersionId,
                        ContractId = g.Key.ContractId,
                        ProgrammeCode = g.Key.ProgrammeCode,
                        ProgrammePriorityCodes = g.Select(x => x.ProgrammePriorityCode).Distinct(),
                        ProcedureCode = g.Key.ProcedureCode,
                        ProjectRegNumber = g.Key.ProjectRegNumber,
                        ContractRegNumber = firstContractVersionCode,
                        CompanyNameBg = g.Key.CompanyNameBg,
                        CompanyNameEn = g.Key.CompanyNameEn,
                        CompanyUin = g.Key.CompanyUin,
                        CompanyUinType = g.Key.CompanyUinType,
                        StartDate = g.Key.StartDate,
                        CompletionDate = g.Key.CompletionDate,
                        TotalAmountBGN = g.Sum(p => p.TotalAmount),
                        TotalAmountEUR = g.Sum(p => p.TotalAmount) / euroExchangeRates,
                        TotalSelfAmountBGN = g.Sum(p => p.SelfAmount),
                        TotalSelfAmountEUR = g.Sum(p => p.SelfAmount) / euroExchangeRates,
                        BfpTotalAmountBGN = g.Sum(p => p.BfpTotalAmount),
                        BfpTotalAmountEUR = g.Sum(p => p.BfpTotalAmount) / euroExchangeRates,
                        BgAmountBGN = g.Sum(p => p.BgAmount),
                        BgAmountEUR = g.Sum(p => p.BgAmount) / euroExchangeRates,
                        EuAmounts = g.Select(p => new FinanceSourceAmountsVO()
                        {
                            AmountBGN = p.BgAmount,
                            AmountEUR = p.EuAmount / euroExchangeRates,
                        }),
                        ProgrammePriorityBudgets = g.GroupBy(ps => ps.ProgrammePriorityCode)
                        .Select(ps => new ProgrammePriorityAmountsVO()
                        {
                            ProgrammePriorityCode = ps.Key,
                            TotalAmountBGN = ps.Sum(p => p.TotalAmount),
                            TotalAmountEUR = ps.Sum(p => p.TotalAmount) / euroExchangeRates,
                            BfpTotalAmountBGN = ps.Sum(p => p.BfpTotalAmount),
                            BfpTotalAmountEUR = ps.Sum(p => p.BfpTotalAmount) / euroExchangeRates,
                            TotalSelfAmountBGN = ps.Sum(p => p.SelfAmount),
                            TotalSelfAmountEUR = ps.Sum(p => p.SelfAmount) / euroExchangeRates,
                            BgAmountBGN = ps.Sum(p => p.BgAmount),
                            BgAmountEUR = ps.Sum(p => p.BgAmount) / euroExchangeRates,
                            EuAmounts = ps.Select(p => new FinanceSourceAmountsVO()
                            {
                                AmountBGN = p.EuAmount,
                                AmountEUR = p.EuAmount / euroExchangeRates,
                            }),
                        }),
                    })
                    .Single();
        }
    }
}
