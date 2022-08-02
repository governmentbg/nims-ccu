using Eumis.Common.Db;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractSpendingPlansRepository : AggregateRepository<ContractSpendingPlanXml>, IContractSpendingPlansRepository
    {
        public ContractSpendingPlansRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractSpendingPlanVO> GetContractSpendingPlans(int contractId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                    where cp.ContractId == contractId && (cp.Source == Source.AdministrativeAuthority || ContractSpendingPlanXml.FinalStatuses.Contains(cp.Status))
                    orderby cp.OrderNum descending
                    select new ContractSpendingPlanVO()
                    {
                        ContractSpendingPlanId = cp.ContractSpendingPlanXmlId,
                        ContactId = cp.ContractId,
                        Source = cp.Source,
                        Status = cp.Status,
                        CreateDate = cp.CreateDate,
                        ModifyDate = cp.ModifyDate,
                    }).ToList();
        }

        public IList<ContractSpendingPlanXml> GetNonArchivedSpendingPlans(int contractId)
        {
            return this.Set()
                .Where(p => p.ContractId == contractId && p.Status != ContractSpendingPlanStatus.Archived)
                .ToList();
        }

        public int GetSpendingPlanId(Guid gid)
        {
            return (from p in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                    where p.Gid == gid
                    select p.ContractSpendingPlanXmlId).Single();
        }

        public int GetProjectId(int spendingPlanId)
        {
            return (from sp in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on sp.ContractId equals c.ContractId
                    where sp.ContractSpendingPlanXmlId == spendingPlanId
                    select c.ProjectId).Single();
        }

        public ContractSpendingPlanXml Find(Guid gid, Source source)
        {
            return this.Set()
                .Where(cp => cp.Gid == gid && (cp.Source == source || ContractSpendingPlanXml.FinalStatuses.Contains(cp.Status)))
                .SingleOrDefault();
        }

        public ContractSpendingPlanXml FindForUpdate(Guid gid, Source source, byte[] version)
        {
            var spendingPlan = this.Set()
                .Where(cp => cp.Gid == gid && cp.Source == source)
                .SingleOrDefault();

            this.CheckVersion(spendingPlan.Version, version);

            return spendingPlan;
        }

        public ContractSpendingPlanXml GetLastSpendingPlanOrDefault(int contractId)
        {
            return this.Set()
                .Where(cp => cp.ContractId == contractId)
                .OrderByDescending(cp => cp.OrderNum)
                .FirstOrDefault();
        }

        public string GetActualSpendingPlanXml(Guid contractGid)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cp.ContractId equals c.ContractId
                    where c.Gid == contractGid && cp.Status == ContractSpendingPlanStatus.Active
                    select cp.Xml)
                .SingleOrDefault();
        }

        public int GetContractId(int spendingPlanId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                    where cp.ContractSpendingPlanXmlId == spendingPlanId
                    select cp.ContractId).Single();
        }

        public bool HasContractSpendingPlansInProgress(int contractId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                    where cp.ContractId == contractId && !ContractSpendingPlanXml.FinalStatuses.Contains(cp.Status)
                    select cp.ContractSpendingPlanXmlId).Any();
        }

        public PagePVO<ContractSpendingPlanPVO> GetPortalContractSpendingPlans(Guid contractGid, int offset = 0, int? limit = null)
        {
            var query = from cp in this.unitOfWork.DbContext.Set<ContractSpendingPlanXml>()
                        join c in this.unitOfWork.DbContext.Set<Contract>() on cp.ContractId equals c.ContractId
                        where c.Gid == contractGid && (cp.Source == Source.Beneficiary || ContractSpendingPlanXml.FinalStatuses.Contains(cp.Status))
                        orderby cp.OrderNum descending
                        select new
                        {
                            cp.Gid,
                            cp.CreateDate,
                            cp.ModifyDate,
                            cp.Source,
                            cp.Status,
                        };

            return new PagePVO<ContractSpendingPlanPVO>()
            {
                Results = query
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(cp => new ContractSpendingPlanPVO
                    {
                        Gid = cp.Gid,
                        CreateDate = cp.CreateDate,
                        ModifyDate = cp.ModifyDate,
                        Source = new EnumPVO<Source>()
                        {
                            Description = cp.Source,
                            Value = cp.Source,
                        },
                        Status = new EnumPVO<ContractSpendingPlanStatus>()
                        {
                            Description = cp.Status,
                            Value = cp.Status,
                        },
                    }).ToList(),
                Count = query.Count(),
            };
        }

        public new void Remove(ContractSpendingPlanXml contractSpendingPlan)
        {
            if (contractSpendingPlan.Status != ContractSpendingPlanStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete nondraft contract SpendingPlan");
            }

            base.Remove(contractSpendingPlan);
        }
    }
}
