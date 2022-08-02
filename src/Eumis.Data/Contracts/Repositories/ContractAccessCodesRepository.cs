using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractAccessCodesRepository : AggregateRepository<ContractAccessCode>, IContractAccessCodesRepository
    {
        public ContractAccessCodesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractAccessCodeVO> GetContractAccessCodes(bool isSuperUser)
        {
            return (from crac in this.unitOfWork.DbContext.Set<ContractAccessCode>()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on crac.ContractId equals c.ContractId
                    select new ContractAccessCodeVO
                    {
                        ContractAccessCodeId = crac.ContractAccessCodeId,
                        ContractId = crac.ContractId,
                        ContractRegNumber = c.RegNumber,
                        Code = isSuperUser ? crac.Code : string.Empty,
                        FirstName = crac.FirstName,
                        LastName = crac.LastName,
                        Position = crac.Position,
                        Identifier = crac.Identifier,
                        Email = crac.Email,
                        IsActive = crac.IsActive,
                        CreateDate = crac.CreateDate,
                        ModifyDate = crac.ModifyDate,
                    }).ToList();
        }

        public IList<ContractAccessCodeVO> GetContractAccessCodes(int contractId, bool isSuperUser)
        {
            return (from crac in this.unitOfWork.DbContext.Set<ContractAccessCode>()
                    where crac.ContractId == contractId
                    select new ContractAccessCodeVO
                    {
                        ContractAccessCodeId = crac.ContractAccessCodeId,
                        ContractId = crac.ContractId,
                        Code = isSuperUser ? crac.Code : string.Empty,
                        FirstName = crac.FirstName,
                        LastName = crac.LastName,
                        Position = crac.Position,
                        Identifier = crac.Identifier,
                        Email = crac.Email,
                        IsActive = crac.IsActive,
                        CreateDate = crac.CreateDate,
                        ModifyDate = crac.ModifyDate,
                    }).ToList();
        }

        public PagePVO<ContractAccessCodePVO> GetContractAccessCodes(Guid contractGid, int offset = 0, int? limit = null)
        {
            var query = from crac in this.unitOfWork.DbContext.Set<ContractAccessCode>()
                        join c in this.unitOfWork.DbContext.Set<Contract>() on crac.ContractId equals c.ContractId
                        where c.Gid == contractGid
                        select crac;

            return new PagePVO<ContractAccessCodePVO>
            {
                Results = query
                    .OrderByDescending(t => t.CreateDate)
                    .WithOffsetAndLimit(offset, limit)
                    .Select(t => new ContractAccessCodePVO
                    {
                        Gid = t.Gid,
                        CreateDate = t.CreateDate,
                        Code = t.Code,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        Position = t.Position,
                        Identifier = t.Identifier,
                        Email = t.Email,
                        IsActive = t.IsActive,
                        Permissions = new AccessCodePermissionPVO
                        {
                            CanReadContracts = t.CanReadContracts,
                            CanReadProcurements = t.CanReadProcurements,
                            CanWriteProcurements = t.CanWriteProcurements,
                            CanReadTechnicalPlan = t.CanReadTechnicalPlan,
                            CanWriteTechnicalPlan = t.CanWriteTechnicalPlan,
                            CanReadFinancialPlan = t.CanReadFinancialPlan,
                            CanWriteFinancialPlan = t.CanWriteFinancialPlan,
                            CanReadMicrodata = t.CanReadMicrodata,
                            CanWriteMicrodata = t.CanReadMicrodata,
                            CanReadPaymentRequest = t.CanReadPaymentRequest,
                            CanWritePaymentRequest = t.CanWritePaymentRequest,
                            CanReadCommunication = t.CanReadCommunication,
                            CanReadSpendingPlan = t.CanReadSpendingPlan,
                            CanWriteSpendingPlan = t.CanWriteSpendingPlan,
                        },
                    })
                    .ToList(),
                Count = query.Count(),
            };
        }

        public ContractAccessCode Find(Guid gid)
        {
            return this.Set().Where(t => t.Gid == gid).Single();
        }

        public ContractAccessCode FindForUpdate(Guid gid, byte[] version)
        {
            var accessCode = this.Find(gid);

            this.CheckVersion(accessCode.Version, version);

            return accessCode;
        }

        public int GetContractId(int contractAccessCodeId)
        {
            return (from ac in this.unitOfWork.DbContext.Set<ContractAccessCode>()
                    where ac.ContractAccessCodeId == contractAccessCodeId
                    select ac.ContractId).First();
        }
    }
}
