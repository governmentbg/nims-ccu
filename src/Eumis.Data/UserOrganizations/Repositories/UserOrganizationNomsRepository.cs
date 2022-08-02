using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.UserOrganizations;
using Eumis.Domain.Users;
using System.Linq;

namespace Eumis.Data.UserOrganizations.Repositories
{
    internal class UserOrganizationNomsRepository : EntityNomsRepository<UserOrganization, EntityNomVO>, IUserOrganizationNomsRepository
    {
        private IAccessContext accessContext;

        public UserOrganizationNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(
                unitOfWork,
                t => t.UserOrganizationId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.UserOrganizationId,
                    Name = t.Name,
                })
        {
            this.accessContext = accessContext;
        }

        protected override System.Linq.IQueryable<UserOrganization> GetQuery()
        {
            var uId = this.accessContext.UserId;

            var userIsSuperUserQuery = this.unitOfWork.CreateUserIsSuperUserQuery(uId);
            var usersSet = this.unitOfWork.DbContext.Set<User>().AsQueryable();

            return from uo in this.unitOfWork.DbContext.Set<UserOrganization>()
                   where userIsSuperUserQuery.Any() ||
                       (from u in usersSet
                        where u.UserId == uId &&
                            u.UserOrganizationId == uo.UserOrganizationId
                        select u)
                       .Any()
                   select uo;
        }
    }
}
