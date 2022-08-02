using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.UserTypes.Repositories
{
    internal class UserTypeNomsRepository : EntityNomsRepository<UserType, EntityNomVO>, IUserTypeNomsRepository
    {
        public UserTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.UserTypeId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.UserTypeId,
                    Name = t.Name,
                })
        {
        }

        public override IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IList<EntityNomVO> GetUserTypeNoms(int userOrganizationId, string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<UserType>()
                .AndStringContains(this.nameSelector, term)
                .AndPropertyEquals(t => t.UserOrganizationId, userOrganizationId);

            return this.GetQuery()
                .Where(predicate)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
