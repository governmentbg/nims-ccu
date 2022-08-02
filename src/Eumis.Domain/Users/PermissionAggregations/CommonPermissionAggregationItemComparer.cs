using System.Collections.Generic;

namespace Eumis.Domain.Users.PermissionAggregations
{
    public class CommonPermissionAggregationItemComparer : IEqualityComparer<CommonPermissionAggregationItem>
    {
        public bool Equals(CommonPermissionAggregationItem x, CommonPermissionAggregationItem y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else if (x.IsSet == y.IsSet && x.Permission.Equals(y.Permission) && x.PermissionType == y.PermissionType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(CommonPermissionAggregationItem obj)
        {
            return obj.IsSet.GetHashCode() ^ obj.Permission.GetHashCode() ^ obj.PermissionType.GetHashCode();
        }
    }
}
