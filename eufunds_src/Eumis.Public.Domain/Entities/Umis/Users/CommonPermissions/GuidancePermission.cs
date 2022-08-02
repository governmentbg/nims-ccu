using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Ръководства")]
    public enum GuidancePermissions
    {
        [Description("Създаване")]
        CanCreate
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
                return Enum.Parse(typeof(GuidancePermissions), base.PermissionString);
            }
        }
    }

    internal class GuidancePermissionMap : EntityTypeConfiguration<GuidancePermission>
    {
        public GuidancePermissionMap()
        {
        }
    }
}
