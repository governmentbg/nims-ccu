using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Оперативна карта")]
    public enum OperationalMapAdminPermissions
    {
        [Description("Администриране")]
        CanAdministrate
    }

    internal class OperationalMapAdminPermission : CommonPermission
    {
        private OperationalMapAdminPermission()
        {
        }

        public OperationalMapAdminPermission(OperationalMapAdminPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(OperationalMapAdminPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(OperationalMapAdminPermissions), base.PermissionString);
            }
        }
    }

    internal class OperationalMapAdminPermissionMap : EntityTypeConfiguration<OperationalMapAdminPermission>
    {
        public OperationalMapAdminPermissionMap()
        {
        }
    }
}
