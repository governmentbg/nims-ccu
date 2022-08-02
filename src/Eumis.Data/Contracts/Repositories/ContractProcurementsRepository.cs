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
using Eumis.Domain.Registrations;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractProcurementsRepository : AggregateRepository<ContractProcurementXml>, IContractProcurementsRepository
    {
        public ContractProcurementsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractProcurementXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractProcurementXml, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ContractProcurementVO> GetContractProcurements(int contractId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    where cp.ContractId == contractId && (cp.Source == Source.AdministrativeAuthority || ContractProcurementXml.FinalStatuses.Contains(cp.Status))
                    orderby cp.OrderNum descending
                    select new ContractProcurementVO()
                    {
                        ContractProcurementId = cp.ContractProcurementXmlId,
                        ContactId = cp.ContractId,
                        Source = cp.Source,
                        Status = cp.Status,
                        CreateDate = cp.CreateDate,
                        ModifyDate = cp.ModifyDate,
                    }).ToList();
        }

        public ContractProcurementPlan GetContractProcurement(int procurementId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>()
                    where cp.ContractProcurementPlanId == procurementId
                    select cp).Single();
        }

        public IList<ContractProcurementXml> GetNonArchivedProcurements(int contractId)
        {
            return this.Set()
                .Where(p => p.ContractId == contractId && p.Status != ContractProcurementStatus.Archived)
                .ToList();
        }

        public int GetProcurementId(Guid gid)
        {
            return (from p in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    where p.Gid == gid
                    select p.ContractProcurementXmlId).Single();
        }

        public int GetProjectId(int procurementId)
        {
            return (from p in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on p.ContractId equals c.ContractId
                    where p.ContractProcurementXmlId == procurementId
                    select c.ProjectId).Single();
        }

        public ContractProcurementXml Find(Guid gid, Source source)
        {
            return this.Set()
                .Where(cp => cp.Gid == gid && (cp.Source == source || ContractProcurementXml.FinalStatuses.Contains(cp.Status)))
                .SingleOrDefault();
        }

        public ContractProcurementXml FindForUpdate(Guid gid, Source source, byte[] version)
        {
            var procurement = this.Set()
                .Where(cp => cp.Gid == gid && cp.Source == source)
                .SingleOrDefault();

            this.CheckVersion(procurement.Version, version);

            return procurement;
        }

        public ContractProcurementXml GetActiveProcurementOrDefault(int contractId)
        {
            return this.Set()
                .Where(cp => cp.ContractId == contractId && cp.Status == ContractProcurementStatus.Active)
                .SingleOrDefault();
        }

        public async Task<ContractProcurementXml> GetActiveProcurementOrDefaultAsync(int contractId, CancellationToken ct)
        {
            return await this.Set()
                .Where(cp => cp.ContractId == contractId && cp.Status == ContractProcurementStatus.Active)
                .SingleOrDefaultAsync(ct);
        }

        public ContractProcurementXml GetLastProcurementOrDefault(int contractId)
        {
            return this.Set()
               .Where(cp => cp.ContractId == contractId)
               .OrderByDescending(cp => cp.OrderNum)
               .FirstOrDefault();
        }

        public string GetActualProcurementXml(Guid contractGid)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on cp.ContractId equals c.ContractId
                    where c.Gid == contractGid && cp.Status == ContractProcurementStatus.Active
                    select cp.Xml)
                .SingleOrDefault();
        }

        public int GetContractId(int procurementId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    where cp.ContractProcurementXmlId == procurementId
                    select cp.ContractId).Single();
        }

        public bool HasContractProcurementsInProgress(int contractId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    where cp.ContractId == contractId && !ContractProcurementXml.FinalStatuses.Contains(cp.Status)
                    select cp.ContractProcurementXmlId).Any();
        }

        public PagePVO<ContractProcurementPVO> GetPortalContractProcurements(Guid contractGid, int offset = 0, int? limit = null)
        {
            var query = from cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                        join c in this.unitOfWork.DbContext.Set<Contract>() on cp.ContractId equals c.ContractId
                        where c.Gid == contractGid && (cp.Source == Source.Beneficiary || ContractProcurementXml.FinalStatuses.Contains(cp.Status))
                        orderby cp.OrderNum descending
                        select new
                        {
                            cp.Gid,
                            cp.CreateDate,
                            cp.ModifyDate,
                            cp.Source,
                            cp.Status,
                            cp.OrderNum,
                        };

            return new PagePVO<ContractProcurementPVO>()
            {
                Results = query
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(cp => new ContractProcurementPVO
                    {
                        Gid = cp.Gid,
                        CreateDate = cp.CreateDate,
                        ModifyDate = cp.ModifyDate,
                        OrderNum = cp.OrderNum,
                        Source = new EnumPVO<Source>()
                        {
                            Description = cp.Source,
                            Value = cp.Source,
                        },
                        Status = new EnumPVO<ContractProcurementStatus>()
                        {
                            Description = cp.Status,
                            Value = cp.Status,
                        },
                    }).ToList(),
                Count = query.Count(),
            };
        }

        public IList<ContractProcurementOfferVO> GetContractProcurementOffers(int contractId)
        {
            return (from rox in this.unitOfWork.DbContext.Set<RegOfferXml>()
                    join cdf in this.unitOfWork.DbContext.Set<ContractDifferentiatedPosition>() on rox.ContractDifferentiatedPositionId equals cdf.ContractDifferentiatedPositionId
                    join cpp in this.unitOfWork.DbContext.Set<ContractProcurementPlan>() on cdf.ContractProcurementPlanId equals cpp.ContractProcurementPlanId

                    where cpp.ContractId == contractId
                    orderby rox.CreateDate descending
                    select new
                    {
                        OfferGid = rox.Gid,
                        OfferSubmitDate = rox.CreateDate,
                        DifferentiatedPositionGid = cdf.Gid,
                        ProcurementPlanGid = cpp.Gid,
                    })
                    .ToList()
                    .Distinct()
                    .Select(t => new ContractProcurementOfferVO
                    {
                        OfferGid = t.OfferGid,
                        OfferSubmitDate = t.OfferSubmitDate,
                        DifferentiatedPositionGid = t.DifferentiatedPositionGid,
                        ProcurementPlanGid = t.ProcurementPlanGid,
                    })
                    .ToList();
        }

        public new void Remove(ContractProcurementXml contractProcurement)
        {
            if (contractProcurement.Status != ContractProcurementStatus.Draft)
            {
                throw new DomainValidationException("Cannot delete nondraft contract procurement");
            }

            base.Remove(contractProcurement);
        }

        public int? GetLastActiveContractProcurementId(int contractId)
        {
            return (from cp in this.unitOfWork.DbContext.Set<ContractProcurementXml>()
                    where cp.ContractId == contractId && cp.Status == ContractProcurementStatus.Active
                    orderby cp.OrderNum descending
                    select cp.ContractProcurementXmlId)
                    .FirstOrDefault();
        }
    }
}
