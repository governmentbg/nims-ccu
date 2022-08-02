using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractContractNomsRepository : Repository, IContractContractNomsRepository
    {
        public ContractContractNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from cc in this.unitOfWork.DbContext.Set<ContractContract>()
                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId
                    where cc.ContractContractId == nomValueId
                    select new
                    {
                        cc.ContractContractId,
                        cc.Number,
                        cc.SignDate,
                        cctor.Uin,
                        cctor.UinType,
                        cctor.Name,
                    })
                    .ToList()
                    .Select(t => new EntityNomVO
                    {
                        NomValueId = t.ContractContractId,
                        Name = string.Format("{0} {1: dd.MM.yyyy} ({2}: {3}) {4}", t.Number, t.SignDate, t.UinType.GetEnumDescription(), t.Uin, t.Name),
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetContractContracts(term: term, offset: offset, limit: limit);
        }

        public IEnumerable<EntityNomVO> GetContractContracts(int? contractId = null, string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<ContractContract>()
                .AndEquals(cc => cc.ContractId, contractId);

            return (from cc in this.unitOfWork.DbContext.Set<ContractContract>().Where(predicate)
                    join cctor in this.unitOfWork.DbContext.Set<ContractContractor>() on cc.ContractContractorId equals cctor.ContractContractorId
                    select new
                    {
                        cc.ContractContractId,
                        cc.Number,
                        cc.SignDate,
                        cctor.Uin,
                        cctor.UinType,
                        cctor.Name,
                    })
                    .ToList()
                    .Select(t => new EntityNomVO
                    {
                        NomValueId = t.ContractContractId,
                        Name = string.Format("{0} {1: dd.MM.yyyy} ({2}: {3}) {4}", t.Number, t.SignDate, t.UinType.GetEnumDescription(), t.Uin, t.Name),
                    })
                    .Where(t => string.IsNullOrEmpty(term) ? true : t.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
