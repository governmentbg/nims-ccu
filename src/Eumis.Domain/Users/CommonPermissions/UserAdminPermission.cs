using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.UserAdminPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum UserAdminPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.UserAdminPermissions_CanAdministrate), ResourceType = typeof(DomainEnumTexts))]
        CanAdministrate,

        [Description(Description = nameof(DomainEnumTexts.UserAdminPermissions_CanControl), ResourceType = typeof(DomainEnumTexts))]
        CanControl,
    }

    internal class UserAdminPermission : CommonPermission
    {
        private UserAdminPermission()
        {
        }

        public UserAdminPermission(UserAdminPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(UserAdminPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(UserAdminPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class AdminPermissionMap : EntityTypeConfiguration<UserAdminPermission>
    {
        public AdminPermissionMap()
        {
        }
    }
}
