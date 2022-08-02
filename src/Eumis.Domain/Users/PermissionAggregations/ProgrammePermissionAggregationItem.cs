using System;

namespace Eumis.Domain.Users.PermissionAggregations
{
    public class ProgrammePermissionAggregationItem
    {
        public ProgrammePermissionAggregationItem(int programmeId, Type permissionType, object permission, bool isSet)
        {
            this.ProgrammeId = programmeId;
            this.PermissionType = permissionType;
            this.Permission = permission;
            this.IsSet = isSet;
        }

        public int ProgrammeId { get; private set; }

        public Type PermissionType { get; private set; }

        public object Permission { get; private set; }

        public bool IsSet { get; private set; }
    }
}
