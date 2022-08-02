using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.InterfacesPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum InterfacesPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.InterfacesPermissions_CanExport), ResourceType = typeof(DomainEnumTexts))]
        CanExport,
    }

    internal class InterfacesPermission : CommonPermission
    {
        private InterfacesPermission()
        {
        }

        public InterfacesPermission(InterfacesPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(InterfacesPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(InterfacesPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class InterfacesPermissionMap : EntityTypeConfiguration<InterfacesPermission>
    {
        public InterfacesPermissionMap()
        {
        }
    }
}
