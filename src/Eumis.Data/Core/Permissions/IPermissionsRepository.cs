using System.Collections.Generic;
using Eumis.Domain.Users;

namespace Eumis.Data.Core.Permissions
{
    public interface IPermissionsRepository
    {
        bool UserHasProgrammePermission<TEnum>(int userId, int programmeId, TEnum permission);

        bool UserHasPermission<TEnum>(int userId, TEnum permission);

        bool UserIsSuperUser(int userId);

        int[] GetProgrammeIdsByPermission<TEnum>(int userId, TEnum permission);

        IList<UserPermission> GetAllUserPermissions(int userId);

        bool UserIsMonitoringUser(int userId);
    }
}
