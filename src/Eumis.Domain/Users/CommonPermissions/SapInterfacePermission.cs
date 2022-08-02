using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.SapInterfacePermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum SapInterfacePermissions
    {
        [Description(Description = nameof(DomainEnumTexts.SapInterfacePermissions_CanImport), ResourceType = typeof(DomainEnumTexts))]
        CanImport,
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
                return Enum.Parse(typeof(SapInterfacePermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class SapInterfacePermissionMap : EntityTypeConfiguration<SapInterfacePermission>
    {
        public SapInterfacePermissionMap()
        {
        }
    }
}
