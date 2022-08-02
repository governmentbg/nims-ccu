using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Проверки на място")]
    public enum SpotCheckPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class SpotCheckPermission : ProgrammePermission
    {
        private SpotCheckPermission()
        {
        }

        public SpotCheckPermission(int programmeId, SpotCheckPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(SpotCheckPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(SpotCheckPermissions), base.PermissionString);
            }
        }
    }

    internal class SpotCheckPermissionMap : EntityTypeConfiguration<SpotCheckPermission>
    {
        public SpotCheckPermissionMap()
        {
        }
    }
}
