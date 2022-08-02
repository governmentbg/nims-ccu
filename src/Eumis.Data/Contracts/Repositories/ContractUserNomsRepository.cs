using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractUserNomsRepository : EntityNomsRepository<User, EntityNomVO>, IContractUserNomsRepository
    {
        private IContractsRepository contractsRepository;

        public ContractUserNomsRepository(IUnitOfWork unitOfWork, IContractsRepository contractsRepository)
            : base(
                unitOfWork,
                t => t.UserId,
                t => t.Fullname + t.Username,
                t => new EntityNomVO
                {
                    NomValueId = t.UserId,
                    Name = t.Fullname + "(" + t.Username + ")",
                })
        {
            this.contractsRepository = contractsRepository;
        }

        public IList<EntityNomVO> GetContractUserNoms(int contractId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<User>()
                .AndStringContains(this.nameSelector, term);

            var registeredUsers = this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.ContractId == contractId).Select(x => x.UserId);

            var users = from u in this.unitOfWork.DbContext.Set<User>()
                        where !registeredUsers.Contains(u.UserId)
                        select u;

            return users
                .Where(predicate)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }

        public IList<EntityNomVO> GetContractUser(int userId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<User>()
                .AndStringContains(this.nameSelector, term);

            var users = from u in this.unitOfWork.DbContext.Set<User>()
                        where u.UserId == userId
                        select u;

            return users
                .Where(predicate)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
