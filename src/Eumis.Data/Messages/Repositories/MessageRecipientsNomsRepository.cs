using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Users;

namespace Eumis.Data.Messages.Repositories
{
    internal class MessageRecipientsNomsRepository : Repository, IMessageRecipientsNomsRepository
    {
        private IAccessContext accessContext;

        public MessageRecipientsNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(unitOfWork)
        {
            this.accessContext = accessContext;
        }

        public IList<EntityNomVO> GetUsers(int[] ids, string term, int offset, int? limit = null)
        {
            var predicate = PredicateBuilder.True<User>()
                .And(u => u.UserId != this.accessContext.UserId);

            if (!string.IsNullOrWhiteSpace(term))
            {
                var predicateTerm = PredicateBuilder.False<User>()
                    .Or(u => u.Username.Contains(term))
                    .Or(u => u.Fullname.Contains(term));

                predicate = predicate.And(predicateTerm);
            }

            var results = from u in this.unitOfWork.DbContext.Set<User>().Where(predicate)
                          where u.IsActive && !u.IsDeleted && !u.IsLocked
                          orderby u.Fullname
                          select new
                          {
                              UserId = u.UserId,
                              Fullname = u.Fullname,
                              Username = u.Username,
                          };

            if (ids.Length != 0)
            {
                return results.Where(p => ids.Contains(p.UserId))
                    .ToList()
                    .Select(p => new EntityNomVO { NomValueId = p.UserId, Name = string.Format("{0}({1})", p.Fullname, p.Username) })
                    .ToList();
            }
            else
            {
                return results.WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(p => new EntityNomVO { NomValueId = p.UserId, Name = string.Format("{0}({1})", p.Fullname, p.Username) })
                    .ToList();
            }
        }
    }
}
