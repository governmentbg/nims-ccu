using Eumis.Domain.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.UserTypes.DataObjects
{
    public class UserTypeDO
    {
        public UserTypeDO()
        {
        }

        public UserTypeDO(UserType userType)
        {
            this.UserTypeId = userType.UserTypeId;
            this.Name = userType.Name;
            this.IsSuperUser = userType.IsSuperUser;
            this.PermissionTemplateId = userType.PermissionTemplateId;
            this.UserOrganizationId = userType.UserOrganizationId;

            this.Version = userType.Version;
        }

        public int UserTypeId { get; set; }

        public string Name { get; set; }

        public bool IsSuperUser { get; set; }

        public int? PermissionTemplateId { get; set; }

        public int? UserOrganizationId { get; set; }

        public byte[] Version { get; set; }
    }
}
