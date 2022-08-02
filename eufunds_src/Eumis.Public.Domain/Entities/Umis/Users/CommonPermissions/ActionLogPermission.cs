using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Лог на действията")]
    public enum ActionLogPermissions
    {
        [Description("Четене")]
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
                return Enum.Parse(typeof(ActionLogPermissions), base.PermissionString);
            }
        }
    }

    internal class ActionLogPermissionMap : EntityTypeConfiguration<ActionLogPermission>
    {
        public ActionLogPermissionMap()
        {
        }
    }
}
