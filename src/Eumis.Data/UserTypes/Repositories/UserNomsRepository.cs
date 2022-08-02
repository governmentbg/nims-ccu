using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.UserTypes.Repositories
{
    internal class UserNomsRepository : EntityNomsRepository<User, EntityNomVO>, IUserNomsRepository
    {
        private readonly IUsersRepository usersRepository;

        public UserNomsRepository(IUnitOfWork unitOfWork, IUsersRepository usersRepository)
            : base(
                  unitOfWork,
                  t => t.UserId,
                  t => t.Fullname,
                  t => new EntityNomVO
                  {
                      NomValueId = t.UserId,
                      Name = t.Fullname,
                      NameAlt = t.Fullname,
                  })
        {
            this.usersRepository = usersRepository;
        }

        public EntityNomVO GetExtendedUserNom(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentException("Filtering by the default value for userId is not allowed.");
            }

            var predicate =
                PredicateBuilder.True<User>()
                .AndPropertyEquals(this.keySelector, userId);

            return this.GetQuery()
                .Where(predicate)
                .Select(t => new EntityNomVO
                {
                    NomValueId = t.UserId,
                    Name = t.Fullname + "(" + t.Username + ")",
                    NameAlt = t.Fullname + "(" + t.Username + ")",
                }).SingleOrDefault();
        }
    }
}
