using System.Collections.Generic;

namespace Eumis.Domain.Users.PermissionAggregations
{
    public class ProgrammePermissionAggregationItemComparer : IEqualityComparer<ProgrammePermissionAggregationItem>
    {
        public bool Equals(ProgrammePermissionAggregationItem x, ProgrammePermissionAggregationItem y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else if (x.ProgrammeId == y.ProgrammeId && x.IsSet == y.IsSet && x.Permission.Equals(y.Permission) && x.PermissionType == y.PermissionType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(ProgrammePermissionAggregationItem obj)
        {
            return obj.ProgrammeId.GetHashCode() ^ obj.IsSet.GetHashCode() ^ obj.Permission.GetHashCode() ^ obj.PermissionType.GetHashCode();
        }
    }
}
