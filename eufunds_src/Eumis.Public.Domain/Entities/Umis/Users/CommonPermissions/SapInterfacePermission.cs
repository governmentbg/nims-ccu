using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Интерфейси към САП")]
    public enum SapInterfacePermissions
    {
        [Description("Импортиране")]
        CanImport
    }

    internal class SapInterfacePermission : CommonPermission
    {
        private SapInterfacePermission()
        {
        }

        public SapInterfacePermission(SapInterfacePermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(SapInterfacePermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(SapInterfacePermissions), base.PermissionString);
            }
        }
    }

    internal class SapInterfacePermissionMap : EntityTypeConfiguration<SapInterfacePermission>
    {
        public SapInterfacePermissionMap()
        {
        }
    }
}
