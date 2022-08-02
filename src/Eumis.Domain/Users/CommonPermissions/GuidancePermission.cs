using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.GuidancePermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum GuidancePermissions
    {
        [Description(Description = nameof(DomainEnumTexts.GuidancePermissions_CanCreate), ResourceType = typeof(DomainEnumTexts))]
        CanCreate,
    }

    internal class GuidancePermission : CommonPermission
    {
        private GuidancePermission()
        {
        }

        public GuidancePermission(GuidancePermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(GuidancePermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(GuidancePermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class GuidancePermissionMap : EntityTypeConfiguration<GuidancePermission>
    {
        public GuidancePermissionMap()
        {
        }
    }
}
