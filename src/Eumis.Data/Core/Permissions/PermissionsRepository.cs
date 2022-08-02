using Eumis.Common.Db;
using Eumis.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Core.Permissions
{
    internal class PermissionsRepository : Repository, IPermissionsRepository
    {
        public PermissionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<UserPermission> GetAllUserPermissions(int userId)
        {
            return this.unitOfWork.DbContext.Set<UserPermission>().Where(up => up.UserId == userId).ToList();
        }

        public bool UserHasProgrammePermission<TEnum>(int userId, int programmeId, TEnum permission)
        {
            return this.unitOfWork.CreateUserHasProgrammePermissionQuery(userId, programmeId, permission).Any();
        }

        public bool UserHasPermission<TEnum>(int userId, TEnum permission)
        {
            return this.unitOfWork.CreateUserHasPermissionQuery(userId, permission).Any();
        }

        public bool UserIsMonitoringUser(int userId)
        {
            return this.unitOfWork.CreateUserIsMonitoringUserQuery(userId).Any();
        }

        public bool UserIsSuperUser(int userId)
        {
            return this.unitOfWork.CreateUserIsSuperUserQuery(userId).Any();
        }

        public int[] GetProgrammeIdsByPermission<TEnum>(int userId, TEnum permission)
        {
            return this.unitOfWork.CreateProgrammeIdsByPermissionQuery(userId, permission).ToArray();
        }
    }
}
