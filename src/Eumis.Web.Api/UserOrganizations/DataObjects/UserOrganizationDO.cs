using Eumis.Domain.UserOrganizations;
using Eumis.Domain.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.UserOrganizations.DataObjects
{
    public class UserOrganizationDO
    {
        public UserOrganizationDO()
        {
        }

        public UserOrganizationDO(UserOrganization userOrganization)
        {
            this.UserOrganizationId = userOrganization.UserOrganizationId;
            this.Name = userOrganization.Name;
            this.Version = userOrganization.Version;
        }

        public int UserOrganizationId { get; set; }

        public string Name { get; set; }

        public byte[] Version { get; set; }
    }
}
