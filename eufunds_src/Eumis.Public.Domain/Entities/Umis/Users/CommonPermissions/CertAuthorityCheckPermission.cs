using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Проверки на СО")]
    public enum CertAuthorityCheckPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class CertAuthorityCheckPermission : CommonPermission
    {
        private CertAuthorityCheckPermission()
        {
        }

        public CertAuthorityCheckPermission(CertAuthorityCheckPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(CertAuthorityCheckPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(CertAuthorityCheckPermissions), base.PermissionString);
            }
        }
    }

    internal class CertAuthorityCheckPermissionMap : EntityTypeConfiguration<CertAuthorityCheckPermissionMap>
    {
        public CertAuthorityCheckPermissionMap()
        {
        }
    }
}
