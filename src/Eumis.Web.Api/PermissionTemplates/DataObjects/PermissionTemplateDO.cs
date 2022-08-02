using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.Users.PermissionAggregations;
using Eumis.Domain.Users.PermissionTables;

namespace Eumis.Web.Api.PermissionTemplates.DataObjects
{
    public class PermissionTemplateDO
    {
        public PermissionTemplateDO()
        {
        }

        public PermissionTemplateDO(Dictionary<int, string> programmes)
        {
            this.UserPermissions = new PermissionTable(programmes);
        }

        public PermissionTemplateDO(PermissionAggregation permissionTemplate, Dictionary<int, string> programmes)
        {
            this.UserPermissions = new PermissionTable(programmes, permissionTemplate, null);
        }

        public PermissionTemplateDO(PermissionTemplate permissionTemplate, Dictionary<int, string> programmes, byte[] version)
        {
            this.PermissionTemplateId = permissionTemplate.PermissionTemplateId;
            this.Name = permissionTemplate.Name;

            var programmeIds = programmes.Keys.ToArray();
            this.UserPermissions = new PermissionTable(programmes, permissionTemplate.GetPermissions(programmeIds), version);

            this.Version = permissionTemplate.Version;
        }

        public int? PermissionTemplateId { get; set; }

        public string Name { get; set; }

        public PermissionTable UserPermissions { get; set; }

        public byte[] Version { get; set; }
    }
}
