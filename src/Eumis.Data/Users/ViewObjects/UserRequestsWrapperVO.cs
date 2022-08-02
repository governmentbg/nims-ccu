using Eumis.Data.RequestPackages.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.Users.ViewObjects
{
    public class UserRequestsWrapperVO
    {
        public ICollection<RequestPackageUserVO> PermissionRequests { get; set; }

        public ICollection<RequestPackageUserVO> RegDataRequests { get; set; }
    }
}
