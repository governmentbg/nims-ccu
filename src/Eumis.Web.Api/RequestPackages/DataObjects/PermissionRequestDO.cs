using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.RequestPackages;
using Eumis.Domain.Users.PermissionAggregations;
using Eumis.Domain.Users.PermissionTables;
using Eumis.Web.Api.PermissionTemplates.DataObjects;

namespace Eumis.Web.Api.RequestPackages.DataObjects
{
    public class PermissionRequestDO
    {
        public PermissionRequestDO()
        {
        }

        public PermissionRequestDO(int requestPackageId, int userId, Dictionary<int, string> programmes, PermissionTemplate permissionTemplate, PermissionAggregation permissions)
        {
            var programmeIds = programmes.Keys.ToArray();

            this.RequestPackageId = requestPackageId;
            this.UserId = userId;
            this.Status = RequestStatus.Draft;
            this.Permissions = new PermissionTable(programmes, permissions, permissionTemplate.GetPermissions(programmeIds), null);
            this.PermissionTemplate = new PermissionTemplateDO(permissionTemplate, programmes, null);
        }

        public PermissionRequestDO(PermissionRequest permissionRequest, Dictionary<int, string> programmes)
        {
            var programmeIds = programmes.Keys.ToArray();

            this.RequestPackageId = permissionRequest.RequestPackageId;
            this.UserId = permissionRequest.UserId;
            this.Permissions = new PermissionTable(programmes, permissionRequest.GetPermissions(programmeIds), permissionRequest.GetPermissionTemplate(programmeIds), null);
        }

        public int PermissionRequestId { get; set; }

        public int? RequestPackageId { get; set; }

        public int? UserId { get; set; }

        public RequestStatus Status { get; set; }

        public string RejectionMessage { get; set; }

        public PermissionTable Permissions { get; set; }

        public PermissionTemplateDO PermissionTemplate { get; set; }
    }
}
