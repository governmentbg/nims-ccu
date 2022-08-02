using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.OperationalMapAdminPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum OperationalMapAdminPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.OperationalMapAdminPermissions_CanAdministrate), ResourceType = typeof(DomainEnumTexts))]
        CanAdministrate,
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
                return Enum.Parse(typeof(OperationalMapAdminPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class OperationalMapAdminPermissionMap : EntityTypeConfiguration<OperationalMapAdminPermission>
    {
        public OperationalMapAdminPermissionMap()
        {
        }
    }
}
