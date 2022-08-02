using Eumis.Domain.Users;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.UserTypes;
using System.Linq;

namespace Eumis.Data.Core.Permissions
{
    internal static class PermissionsRepositoryQueries
    {
        public static IQueryable<int> CreateUserHasProgrammePermissionQuery<TEnum>(this UnitOfWork unitOfWork, int userId, int programmeId, TEnum permission)
        {
            return unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(TEnum)))
                .OfType<ProgrammePermission>()
                .Where(up => up.UserId == userId &&
                    up.ProgrammeId == programmeId &&
                    up.PermissionString == permission.ToString())
                .Select(up => up.UserPermissionId);
        }

        public static IQueryable<int> CreateUserHasPermissionQuery<TEnum>(this UnitOfWork unitOfWork, int userId, TEnum permission)
        {
            return unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(TEnum)))
                .OfType<UserPermission>()
                .Where(up => up.UserId == userId &&
                    up.PermissionString == permission.ToString())
                .Select(up => up.UserPermissionId);
        }

        public static IQueryable<int> CreateProgrammeIdsByPermissionQuery<TEnum>(this UnitOfWork unitOfWork, int userId, TEnum permission)
        {
            return unitOfWork.DbContext.Set(UserPermission.GetPermissionEntityType(typeof(TEnum)))
                .OfType<ProgrammePermission>()
                .Where(up => up.UserId == userId &&
                             up.PermissionString == permission.ToString())
                .Select(up => up.ProgrammeId)
                .Distinct();
        }

        public static IQueryable<int> CreateUserIsSuperUserQuery(this UnitOfWork unitOfWork, int userId)
        {
            var userHasPermissionQuery = unitOfWork.CreateUserHasPermissionQuery(userId, UserAdminPermissions.CanAdministrate);

            return (from u in unitOfWork.DbContext.Set<User>()
                    join ut in unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
                    where u.UserId == userId && ut.IsSuperUser && !u.IsSystem && userHasPermissionQuery.Any()
                    select ut.UserTypeId)
                   .AsQueryable();
        }

        public static IQueryable<int> CreateUserIsMonitoringUserQuery(this UnitOfWork unitOfWork, int userId)
        {
            var userHasPermissionQuery = unitOfWork.CreateUserHasPermissionQuery(userId, UserAdminPermissions.CanControl);

            return (from u in unitOfWork.DbContext.Set<User>()
                    join ut in unitOfWork.DbContext.Set<UserType>() on u.UserTypeId equals ut.UserTypeId
                    where u.UserId == userId && ut.IsSuperUser && !u.IsSystem && userHasPermissionQuery.Any()
                    select ut.UserTypeId)
                   .AsQueryable();
        }
    }
}
