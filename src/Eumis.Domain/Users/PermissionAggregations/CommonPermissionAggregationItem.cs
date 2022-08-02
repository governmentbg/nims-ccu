using System;

namespace Eumis.Domain.Users.PermissionAggregations
{
    public class CommonPermissionAggregationItem
    {
        public CommonPermissionAggregationItem(Type permissionType, object permission, bool isSet)
        {
            this.PermissionType = permissionType;
            this.Permission = permission;
            this.IsSet = isSet;
        }

        public Type PermissionType { get; private set; }

        public object Permission { get; private set; }

        public bool IsSet { get; private set; }
    }
}
