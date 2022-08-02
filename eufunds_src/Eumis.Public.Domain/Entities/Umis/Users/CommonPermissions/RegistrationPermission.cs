using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Профили")]
    public enum RegistrationPermissions
    {
        [Description("Четене")]
        CanRead
    }

    internal class RegistrationPermission : CommonPermission
    {
        private RegistrationPermission()
        {
        }

        public RegistrationPermission(RegistrationPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(RegistrationPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(RegistrationPermissions), base.PermissionString);
            }
        }
    }

    internal class RegistrationPermissionMap : EntityTypeConfiguration<RegistrationPermission>
    {
        public RegistrationPermissionMap()
        {
        }
    }
}
