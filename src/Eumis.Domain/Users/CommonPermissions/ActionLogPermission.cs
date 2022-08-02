using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.ActionLogPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ActionLogPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ActionLogPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,
    }

    internal class ActionLogPermission : CommonPermission
    {
        private ActionLogPermission()
        {
        }

        public ActionLogPermission(ActionLogPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ActionLogPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ActionLogPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ActionLogPermissionMap : EntityTypeConfiguration<ActionLogPermission>
    {
        public ActionLogPermissionMap()
        {
        }
    }
}
