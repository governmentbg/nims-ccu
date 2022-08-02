using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Новини")]
    public enum NewsPermissions
    {
        [Description("Публикуване")]
        CanPublish
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
                return Enum.Parse(typeof(NewsPermissions), base.PermissionString);
            }
        }
    }

    internal class NewsPermissionMap : EntityTypeConfiguration<NewsPermission>
    {
        public NewsPermissionMap()
        {
        }
    }
}
