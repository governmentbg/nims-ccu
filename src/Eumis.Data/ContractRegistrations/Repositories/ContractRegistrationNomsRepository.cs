using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ContractRegistrations.Repositories
{
    internal class ContractRegistrationNomsRepository : Repository, IContractRegistrationNomsRepository
    {
        public ContractRegistrationNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ContractRegistrationNomVO GetNom(int nomValueId)
        {
            return (from e in this.unitOfWork.DbContext.Set<ContractRegistration>()
                    where e.ContractRegistrationId == nomValueId
                    select new ContractRegistrationNomVO
                    {
                        NomValueId = e.ContractRegistrationId,
                        ContractRegistrationId = e.ContractRegistrationId,
                        Name = e.Email,
                        Email = e.Email,
                        UinType = e.UinType,
                        Uin = e.Uin,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Phone = e.Phone,
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<ContractRegistrationNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetNomsInternal(null, term, offset, limit);
        }

        public IEnumerable<ContractRegistrationNomVO> GetNotAttachedContractRegistrations(
            int contractId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            return this.GetNomsInternal(contractId, term, offset, limit);
        }

        private IEnumerable<ContractRegistrationNomVO> GetNomsInternal(int? contractId, string term, int offset, int? limit)
        {
            var predicate = PredicateBuilder.True<ContractRegistration>().AndStringContains(e => e.Email, term);

            var contractRegistrations =
                from e in this.unitOfWork.DbContext.Set<ContractRegistration>().Where(predicate)
                select e;

            if (contractId.HasValue)
            {
                contractRegistrations = from cr in contractRegistrations
                                        join ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>() on new { ContractId = contractId.Value, cr.ContractRegistrationId } equals new { ccr.ContractId, ccr.ContractRegistrationId } into g1
                                        from ccr in g1.DefaultIfEmpty()
                                        where ccr == null
                                        select cr;
            }

            return (from e in contractRegistrations
                    select new ContractRegistrationNomVO
                    {
                        NomValueId = e.ContractRegistrationId,
                        ContractRegistrationId = e.ContractRegistrationId,
                        Name = e.Email,
                        Email = e.Email,
                        UinType = e.UinType,
                        Uin = e.Uin,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Phone = e.Phone,
                    })
                    .OrderBy(e => e.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}