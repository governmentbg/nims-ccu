using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Профили за достъп към договор")]
    public enum ContractRegistrationPermissions
    {
        [Description("Четене")]
        CanRead
    }

    internal class ContractRegistrationPermission : CommonPermission
    {
        private ContractRegistrationPermission()
        {
        }

        public ContractRegistrationPermission(ContractRegistrationPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ContractRegistrationPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ContractRegistrationPermissions), base.PermissionString);
            }
        }
    }

    internal class ContractRegistrationPermissionMap : EntityTypeConfiguration<ContractRegistrationPermission>
    {
        public ContractRegistrationPermissionMap()
        {
        }
    }
}
