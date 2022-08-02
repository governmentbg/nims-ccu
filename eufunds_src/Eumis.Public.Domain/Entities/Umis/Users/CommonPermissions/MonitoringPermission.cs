using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Наблюдение")]
    public enum MonitoringPermissions
    {
        [Description("Четене")]
        CanRead
    }

    internal class MonitoringPermission : CommonPermission
    {
        private MonitoringPermission()
        {
        }

        public MonitoringPermission(MonitoringPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(MonitoringPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(MonitoringPermissions), base.PermissionString);
            }
        }
    }

    internal class MonitoringPermissionMap : EntityTypeConfiguration<MonitoringPermission>
    {
        public MonitoringPermissionMap()
        {
        }
    }
}
