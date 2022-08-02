using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Потребители")]
    public enum UserAdminPermissions
    {
        [Description("Администриране")]
        CanAdministrate,

        [Description("Контролиране")]
        CanControl
    }

    internal class UserAdminPermission : CommonPermission
    {
        private UserAdminPermission()
        {
        }

        public UserAdminPermission(UserAdminPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(UserAdminPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(UserAdminPermissions), base.PermissionString);
            }
        }
    }

    internal class AdminPermissionMap : EntityTypeConfiguration<UserAdminPermission>
    {
        public AdminPermissionMap()
        {
        }
    }
}
