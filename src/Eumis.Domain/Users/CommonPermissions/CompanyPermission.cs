using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.CompanyPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum CompanyPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.CompanyPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.CompanyPermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,
    }

    internal class CompanyPermission : CommonPermission
    {
        private CompanyPermission()
        {
        }

        public CompanyPermission(CompanyPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(CompanyPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(CompanyPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class CompanyPermissionMap : EntityTypeConfiguration<CompanyPermission>
    {
        public CompanyPermissionMap()
        {
        }
    }
}
