using System;
using Eumis.Domain.Users.PermissionAggregations;

namespace Eumis.Domain.PermissionTemplates
{
    public partial class PermissionTemplate
    {
        public void Update(string name, PermissionAggregation permissions)
        {
            this.Name = name;
            this.SetPermissions(permissions);

            this.ModifyDate = DateTime.Now;
        }
    }
}
