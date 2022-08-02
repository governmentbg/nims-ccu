using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.NewsPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum NewsPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.NewsPermissions_CanPublish), ResourceType = typeof(DomainEnumTexts))]
        CanPublish,
    }

    internal class NewsPermission : CommonPermission
    {
        private NewsPermission()
        {
        }

        public NewsPermission(NewsPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(NewsPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(NewsPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class NewsPermissionMap : EntityTypeConfiguration<NewsPermission>
    {
        public NewsPermissionMap()
        {
        }
    }
}
