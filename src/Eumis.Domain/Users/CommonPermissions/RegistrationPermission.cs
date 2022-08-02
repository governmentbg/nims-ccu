using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.RegistrationPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum RegistrationPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.RegistrationPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,
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
                return Enum.Parse(typeof(RegistrationPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class RegistrationPermissionMap : EntityTypeConfiguration<RegistrationPermission>
    {
        public RegistrationPermissionMap()
        {
        }
    }
}
