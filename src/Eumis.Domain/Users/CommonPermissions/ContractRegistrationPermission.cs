using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.ContractRegistrationPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ContractRegistrationPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ContractRegistrationPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,
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
                return Enum.Parse(typeof(ContractRegistrationPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ContractRegistrationPermissionMap : EntityTypeConfiguration<ContractRegistrationPermission>
    {
        public ContractRegistrationPermissionMap()
        {
        }
    }
}
