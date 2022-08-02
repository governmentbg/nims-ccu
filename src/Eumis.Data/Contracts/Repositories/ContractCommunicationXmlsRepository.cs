using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common;
using Eumis.Common.Db;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractCommunicationXmlsRepository : AggregateRepository<ContractCommunicationXml>, IContractCommunicationXmlsRepository
    {
        public ContractCommunicationXmlsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ContractCommunicationXml, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ContractCommunicationXml, object>>[]
                {
                    c => c.Files,
                };
            }
        }

        public IList<ContractCommunicationVO> GetContractCommunications(int contractId, ContractCommunicationType type)
        {
            return (from ccx in this.unitOfWork.DbContext.Set<ContractCommunicationXml>()
                    where ccx.ContractId == contractId &&
                          ccx.Type == type &&
                          (ccx.Status == ContractCommunicationStatus.Sent || (ccx.Status == ContractCommunicationStatus.Draft && ccx.Source == Source.AdministrativeAuthority))
                    select ccx)
                .OrderByDescending(t => t.SendDate ?? DateTime.MaxValue)
                .ThenByDescending(t => t.ModifyDate)
                .ToList()
                .Select(ccx => new ContractCommunicationVO
                {
                    ContractCommunicationXmlId = ccx.ContractCommunicationXmlId,
                    XmlGid = ccx.Gid,
                    ContractId = ccx.ContractId,
                    Subject = ccx.Subject,
                    Status = ccx.Status,
                    StatusNote = ccx.StatusNote,
                    Source = ccx.DisplaySource,
                    RegNumber = ccx.RegNumber,
                    ReadDate = ccx.ReadDate,
                    SendDate = ccx.SendDate,
                    ModifyDate = ccx.ModifyDate,
                }).ToList();
        }

        public IList<AdminAuthorityContractCommunicationVO> GetAllCommunications(
            int[] programmeIds,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Source? source = null)
        {
            if (toDate.HasValue)
            {
                toDate = toDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            var contractPredicate = PredicateBuilder.True<Domain.Contracts.Contract>()
                .AndEquals(c => c.ProgrammeId, programmeId)
                .AndEquals(c => c.ProcedureId, procedureId)
                .And(c => c.ContractStatus == ContractStatus.Entered);

            var contractCommunicationPredicate = PredicateBuilder.True<Domain.Contracts.ContractCommunicationXml>()
                .AndDateTimeGreaterThanOrEqual(cc => cc.SendDate, fromDate)
                .AndDateTimeLessThanOrEqual(cc => cc.SendDate, toDate)
                .AndEquals(cc => cc.Source, source);

            var subqueryPredicate = PredicateBuilder.True<ProcedureShare>();

            if (programmePriorityId.HasValue)
            {
                subqueryPredicate = subqueryPredicate.And(ps => ps.ProgrammePriorityId == programmePriorityId);
            }

            var subquery = (from ps in this.unitOfWork.DbContext.Set<ProcedureShare>().Where(subqueryPredicate)
                            select ps.ProcedureId).Distinct();

            if (!procedureId.HasValue)
            {
                contractPredicate = contractPredicate.And(c => subquery.Contains(c.ProcedureId));
            }

            return (from ccx in this.unitOfWork.DbContext.Set<ContractCommunicationXml>().Where(contractCommunicationPredicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on ccx.ContractId equals c.ContractId
                    where ccx.Type == ContractCommunicationType.Administrative && ccx.Status == ContractCommunicationStatus.Sent &&
                        programmeIds.Contains(c.ProgrammeId)
                    select new { c, ccx })
                .OrderByDescending(t => t.ccx.SendDate ?? DateTime.MaxValue)
                .ThenByDescending(t => t.ccx.ModifyDate)
                .ToList()
                .Select(r => new AdminAuthorityContractCommunicationVO
                {
                    ContractRegNumber = r.c.RegNumber,
                    ProgrammeId = r.c.ProgrammeId,
                    ProcedureId = r.c.ProcedureId,
                    ContractCommunicationXmlId = r.ccx.ContractCommunicationXmlId,
                    XmlGid = r.ccx.Gid,
                    ContractId = r.ccx.ContractId,
                    Subject = r.ccx.Subject,
                    Status = r.ccx.Status,
                    StatusNote = r.ccx.StatusNote,
                    Source = r.ccx.DisplaySource,
                    RegNumber = r.ccx.RegNumber,
                    ReadDate = r.ccx.ReadDate,
                    SendDate = r.ccx.SendDate,
                    ModifyDate = r.ccx.ModifyDate,
                }).ToList();
        }

        public PagePVO<ContractCommunicationPVO> GetPortalContractCommunications(Guid contractGid, ContractCommunicationType type, int offset = 0, int? limit = null)
        {
            var query = from ccx in this.unitOfWork.DbContext.Set<ContractCommunicationXml>()
                        join c in this.unitOfWork.DbContext.Set<Contract>() on ccx.ContractId equals c.ContractId
                        where c.Gid == contractGid &&
                               ccx.Type == type &&
                              (ccx.Status == ContractCommunicationStatus.Sent || (ccx.Status == ContractCommunicationStatus.Draft && ccx.Source == Source.Beneficiary))
                        select ccx;

            return new PagePVO<ContractCommunicationPVO>
            {
                Results = query
                    .OrderByDescending(t => t.SendDate ?? DateTime.MaxValue)
                    .ThenByDescending(t => t.ModifyDate)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(t => new ContractCommunicationPVO()
                    {
                        XmlGid = t.Gid,
                        Status = new EnumPVO<ContractCommunicationStatus>()
                        {
                            Description = t.Status,
                            Value = t.Status,
                        },
                        StatusNote = t.StatusNote,
                        Source = new EnumPVO<ContractCommunicationSource>()
                        {
                            Description = t.DisplaySource,
                            Value = t.DisplaySource,
                        },
                        RegNumber = t.RegNumber,
                        ReadDate = t.ReadDate,
                        SendDate = t.SendDate,
                        ModifyDate = t.ModifyDate,
                        Subject = t.Subject.TruncateWithEllipsis(100),
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public ContractCommunicationXml Find(Guid gid)
        {
            return this.Set()
                .Where(pc => pc.Gid == gid)
                .Single();
        }

        public ContractCommunicationXml FindForUpdate(Guid gid, byte[] version)
        {
            var contractCommunication = this.Find(gid);

            this.CheckVersion(contractCommunication.Version, version);

            return contractCommunication;
        }

        public Tuple<int, ContractCommunicationType> GetCommunicationIdAndType(Guid gid)
        {
            var result = (from c in this.unitOfWork.DbContext.Set<ContractCommunicationXml>()
                          where c.Gid == gid
                          select new { Id = c.ContractCommunicationXmlId, c.Type }).Single();

            return Tuple.Create<int, ContractCommunicationType>(result.Id, result.Type);
        }

        public int GetContractId(int communicationId)
        {
            return (from ccx in this.unitOfWork.DbContext.Set<ContractCommunicationXml>()
                    where ccx.ContractCommunicationXmlId == communicationId
                    select ccx.ContractId).Single();
        }

        public int GetProjectId(int communicationId)
        {
            return (from v in this.unitOfWork.DbContext.Set<ContractCommunicationXml>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on v.ContractId equals c.ContractId
                    where v.ContractCommunicationXmlId == communicationId
                    select c.ProjectId).Single();
        }
    }
}
